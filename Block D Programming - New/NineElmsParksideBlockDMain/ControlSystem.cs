using System;
using System.Collections.Generic;
using Crestron.SimplSharp;                              // For Basic SIMPL# Classes
using Crestron.SimplSharp.CrestronIO;
using Crestron.SimplSharpPro;                       	// For Basic SIMPL#Pro classes
using Crestron.SimplSharpPro.CrestronThread;            // For Threading
using Crestron.SimplSharpPro.EthernetCommunication;
using Crestron.SimplSharpPro.UI;
using Newtonsoft.Json;

namespace NineElmsParksideBlockDMain
{
    public class ControlSystem : CrestronControlSystem
    {
        public static bool fireAlarm = false;

        public static ThreeSeriesTcpIpEthernetIntersystemCommunications _SimplWindowsComms;
        CrestronOne iPad;

        static uint _skyHD1_IRPort = 1;
        static uint _skyHD2_IRPort = 2;

        public ControlSystem()
            : base()
        {
            try
            {
                Thread.MaxNumberOfUserThreads = 20;

                //Subscribe to the controller events (System, Program, and Ethernet)
                CrestronEnvironment.SystemEventHandler += new SystemEventHandler(_ControllerSystemEventHandler);
                CrestronEnvironment.ProgramStatusEventHandler += new ProgramStatusEventHandler(_ControllerProgramEventHandler);
                CrestronEnvironment.EthernetEventHandler += new EthernetEventHandler(_ControllerEthernetEventHandler);

                if (this.SupportsEthernet)
                {
                    ConsoleLogger cs = new ConsoleLogger();
                    cs.ConsoleLoggerStart(55555);

                    WebServer ws = new WebServer(this);

                    iPad = new CrestronOne(0x03, this);
                    iPad.ParameterProjectName.Value = "Parkside-NineElms-Block-D-iPad-GUI";
                    iPad.Register();
                }
                if (this.SupportsIROut)
                {
                    string SkyHDIRPath = string.Format("{0}/user/SkyHD.ir", Directory.GetDirectoryRoot(Directory.GetApplicationDirectory()));

                    ControllerIROutputSlot.Register();

                    try { IROutputPorts[_skyHD1_IRPort].LoadIRDriver(SkyHDIRPath); } catch (Exception ex) { ConsoleLogger.WriteLine($"Problem loading Sky HD 1 IR: {ex.Message}"); }
                    try { IROutputPorts[_skyHD2_IRPort].LoadIRDriver(SkyHDIRPath); } catch (Exception ex) { ConsoleLogger.WriteLine($"Problem loading Sky HD 2 IR: {ex.Message}"); }

                    ConsoleLogger.WriteLine("IR Drivers Loading Complete");
                }
                if(this.SupportsVersiport)
                {
                    this.VersiPorts[1].Register();
                    ConsoleLogger.WriteLine("Configuring versiport as Digital In");
                    this.VersiPorts[1].SetVersiportConfiguration(eVersiportConfiguration.DigitalInput);
                    this.VersiPorts[1].VersiportChange += ControlSystem_VersiportChange;
                }
            }
            catch (Exception e)
            {
                ErrorLog.Error("Error in the constructor: {0}", e.Message);
            }
        }

        List<(int, string)> sourcesBeforeFire;
        private void ControlSystem_VersiportChange(Versiport port, VersiportEventArgs args)
        {
            if(port.ID == 1)
            {
                if (!port.DigitalIn)
                {
                    fireAlarm = true;

                    sourcesBeforeFire = new List<(int, string)>();
                    foreach(var room in FileOperations.GetRoomDirectories())
                    {
                        string roomID = room.Split('/')[1].Replace("Room", "");
                        RoomCoreData rcd = JsonConvert.DeserializeObject<RoomCoreData>(FileOperations.loadRoomJson(int.Parse(roomID), "Core"));

                        (int, string) prevoiusSourcePair = (int.Parse(roomID), rcd.sourceSelected);
                        sourcesBeforeFire.Add(prevoiusSourcePair);

                        RoomControl.Shutdown(roomID);
                    }
                    ConsoleLogger.WriteLine("Fire Alarm Detected");
                }
                else
                {
                    fireAlarm = false;

                    foreach(var roomSourcePair in sourcesBeforeFire)
                        RoomControl.ChangeSourceSelected(roomSourcePair.Item1, roomSourcePair.Item2);

                    ConsoleLogger.WriteLine("Fire Alarm Cleared");
                }
            }
        }

        public override void InitializeSystem()
        {

            try
            {
                _SimplWindowsComms = new ThreeSeriesTcpIpEthernetIntersystemCommunications(0xB0, "127.0.0.2", this);
                _SimplWindowsComms.Register();
                _SimplWindowsComms.SigChange += _SimplWindowsComms_SigChange;
            }
            catch (Exception e)
            {
                ErrorLog.Error("Error in InitializeSystem: {0}", e.Message);
            }
        }

        private void _SimplWindowsComms_SigChange(Crestron.SimplSharpPro.DeviceSupport.BasicTriList currentDevice, SigEventArgs args)
        {
            switch (args.Sig.Type)
            {
                case eSigType.String:
                    _SimplWindowsComms_MessageReceived(_SimplWindowsComms.StringOutput[1].StringValue);
                    break;
            }
        }
        private void _SimplWindowsComms_MessageReceived(string newMessage)
        {
            try
            {
                string fromSIMPLWindows = newMessage;
                ConsoleLogger.WriteLine("Message Received from SIMPL Windows: " + fromSIMPLWindows);

                if (fromSIMPLWindows.Contains("BGM"))
                {
                    string roomIDraw = fromSIMPLWindows.Split(':')[1].Replace("Room", "");
                    int roomID = int.Parse(roomIDraw);

                    RoomCoreData rcd = JsonConvert.DeserializeObject<RoomCoreData>(FileOperations.loadRoomJson(roomID, "Core"));

                    if (fromSIMPLWindows.Contains("Volume")) rcd.volLevel = int.Parse(fromSIMPLWindows.Split(':')[3]);
                    else if (fromSIMPLWindows.Contains("UnMuted")) rcd.volMute = false;
                    else if (fromSIMPLWindows.Contains("Muted")) rcd.volMute = true;

                    FileOperations.saveRoomJson(roomID.ToString(), "Core", JsonConvert.SerializeObject(rcd));
                }
            }
            catch (Exception ex)
            {
                ConsoleLogger.WriteLine("Problem in ControlSystem _SimplWindowsComms_MessageReceived: " + ex);
            }
        }

        public static void SendMessageToSIMPL(string message) => _SimplWindowsComms.StringInput[1].StringValue = message;
        public static void FreeviewBtnPress(string roomID, string btnPressed) => SendMessageToSIMPL("Room" + roomID + "TV1KP:" + btnPressed);

        public void SkyBtnPress(int roomID, string btnName)
        {
            RoomCoreData rcd = JsonConvert.DeserializeObject<RoomCoreData>(FileOperations.loadRoomJson(roomID, "Core"));
            if (rcd.skyIRPort == 0) { ConsoleLogger.WriteLine("No IR Port Assigned to Sky"); return; }

            try { IROutputPorts[rcd.skyIRPort].PressAndRelease(btnName, 25); }
            catch (Exception ex) { ConsoleLogger.WriteLine("Problem in SkyHDBtnPress: " + ex); }
        }

        void _ControllerEthernetEventHandler(EthernetEventArgs ethernetEventArgs)
        {
            switch (ethernetEventArgs.EthernetEventType)
            {//Determine the event type Link Up or Link Down
                case (eEthernetEventType.LinkDown):
                    if (ethernetEventArgs.EthernetAdapter == EthernetAdapterType.EthernetLANAdapter)
                    {
                        //
                    }
                    break;
                case (eEthernetEventType.LinkUp):
                    if (ethernetEventArgs.EthernetAdapter == EthernetAdapterType.EthernetLANAdapter)
                    {

                    }
                    break;
            }
        }
        void _ControllerProgramEventHandler(eProgramStatusEventType programStatusEventType)
        {
            switch (programStatusEventType)
            {
                case (eProgramStatusEventType.Paused):
                    break;
                case (eProgramStatusEventType.Resumed):
                    break;
                case (eProgramStatusEventType.Stopping):
                    break;
            }

        }
        void _ControllerSystemEventHandler(eSystemEventType systemEventType)
        {
            switch (systemEventType)
            {
                case (eSystemEventType.DiskInserted):
                    break;
                case (eSystemEventType.DiskRemoved):
                    break;
                case (eSystemEventType.Rebooting):
                    break;
            }

        }
    }
}