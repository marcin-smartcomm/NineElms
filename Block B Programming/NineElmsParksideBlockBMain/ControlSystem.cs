using System;
using Crestron.SimplSharp.CrestronIO;
using Crestron.SimplSharp;                          	// For Basic SIMPL# Classes
using Crestron.SimplSharpPro;                       	// For Basic SIMPL#Pro classes
using Crestron.SimplSharpPro.CrestronThread;            // For Threading
using Crestron.SimplSharpPro.Gateways;
using Crestron.SimplSharpPro.Remotes;
using Crestron.SimplSharpPro.EthernetCommunication;
using Crestron.SimplSharpPro.UI;
using Newtonsoft.Json;

namespace NineElmsParksideBlockBMain
{
    public class ControlSystem : CrestronControlSystem
    {
        public SSE_Server sse;
        public static ThreeSeriesTcpIpEthernetIntersystemCommunications _SimplWindowsComms;
        CrestronOne iPad;
        CenGwExEr cinemaGW;
        Hr310 cinemaRemote;

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

                    sse = new SSE_Server();
                    WebServer ws = new WebServer(sse, this);

                    iPad = new CrestronOne(0x03, this);
                    iPad.ParameterProjectName.Value = "Parkside-NineElms-Block-B-iPad-GUI";
                    iPad.Register();

                    cinemaGW = new CenGwExEr(0x30, this);
                    cinemaGW.Register();

                    cinemaRemote = new Hr310(0x03, cinemaGW);
                    cinemaRemote.Register();

                    cinemaRemote.OnlineStatusChange += CinemaRemote_OnlineStatusChange;
                    cinemaRemote.ButtonStateChange += CinemaRemote_ButtonStateChange;
                }

                if(this.SupportsIROut)
                {
                    string SkyHDIRPath = string.Format("{0}/user/SkyHD.ir", Directory.GetDirectoryRoot(Directory.GetApplicationDirectory()));

                    string SkyQIRPath = string.Format("{0}/user/SkyQ.ir", Directory.GetDirectoryRoot(Directory.GetApplicationDirectory()));

                    ControllerIROutputSlot.Register();

                    IROutputPorts[1].LoadIRDriver(SkyHDIRPath);
                    IROutputPorts[2].LoadIRDriver(SkyQIRPath);

                    ConsoleLogger.WriteLine("IR Drivers Loaded");
                }
            }
            catch (Exception e)
            {
                ErrorLog.Error("Error in the constructor: {0}", e.Message);
            }
        }

        private void CinemaRemote_ButtonStateChange(GenericBase device, Crestron.SimplSharpPro.DeviceSupport.ButtonEventArgs args)
        {
            switch (args.Button.State)
            {
                case Crestron.SimplSharpPro.DeviceSupport.eButtonState.Pressed:
                    if(args.Button.Name == Crestron.SimplSharpPro.DeviceSupport.eButtonName.Custom1)            RoomControl.ChangeCourceSelected(6, 0);
                    if (args.Button.Name == Crestron.SimplSharpPro.DeviceSupport.eButtonName.Custom2) RoomControl.ChangeCourceSelected(6, 1);
                    if (args.Button.Name == Crestron.SimplSharpPro.DeviceSupport.eButtonName.Custom3) RoomControl.ChangeCourceSelected(6, 2);
                    if (args.Button.Name == Crestron.SimplSharpPro.DeviceSupport.eButtonName.Custom4) RoomControl.ChangeCourceSelected(6, 3);

                    if (args.Button.Name == Crestron.SimplSharpPro.DeviceSupport.eButtonName.VolumeUp)
                        RoomControl.VolUp("6");
                    if (args.Button.Name == Crestron.SimplSharpPro.DeviceSupport.eButtonName.VolumeDown)
                        RoomControl.VolDown("6");
                    if (args.Button.Name == Crestron.SimplSharpPro.DeviceSupport.eButtonName.Mute)
                        RoomControl.Mute("6");

                    if (args.Button.Name == Crestron.SimplSharpPro.DeviceSupport.eButtonName.Power)
                        RoomControl.Shutdown("6");
                    break;
            }
        }

        private void CinemaRemote_OnlineStatusChange(GenericBase currentDevice, OnlineOfflineEventArgs args)
        {
            ConsoleLogger.WriteLine("Cinema Remote Online Status changed to: " + args.DeviceOnLine);
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

                    if (fromSIMPLWindows.Contains("Volume"))
                    {
                        rcd.volLevel = int.Parse(fromSIMPLWindows.Split(':')[3]);
                        sse.UpdateAllConnected(roomID, $"Slider:{fromSIMPLWindows.Split(':')[3]}");
                    }
                    else if (fromSIMPLWindows.Contains("UnMuted"))
                    {
                        rcd.volMute = false;
                        sse.UpdateAllConnected(roomID, "Mute:false");
                    }
                    else if (fromSIMPLWindows.Contains("Muted"))
                    {
                        rcd.volMute = true;
                        sse.UpdateAllConnected(roomID, "Mute:true");
                    }

                    FileOperations.saveRoomJson(roomID.ToString(), "Core", JsonConvert.SerializeObject(rcd));
                }
            }
            catch (Exception ex)
            {
                ConsoleLogger.WriteLine("Problem in ControlSystem _SimplWindowsComms_MessageReceived: " + ex);
            }
        }

        public static void SendMessageToSIMPL(string message)
        {
            _SimplWindowsComms.StringInput[1].StringValue = message;
        }

        public static void FreeviewBtnPress(string roomID, int btnNum)
        {
            string key = string.Empty;
            switch (btnNum)
            {
                //SOURCE
                case 0: key = "InputSelect"; break;
                //POWER
                case 1: key = "PowerToggle"; break;
                //1
                case 2: key = "1"; break;
                //2
                case 3: key = "2"; break;
                //3
                case 4: key = "3"; break;
                //VOL_UP
                case 5: key = "Vol+"; break;
                //4
                case 6: key = "4"; break;
                //5
                case 7: key = "5"; break;
                //6
                case 8: key = "6"; break;
                //VOL_DOWN
                case 9: key = "Vol-"; break;
                //7
                case 10: key = "7"; break;
                //8
                case 11: key = "8"; break;
                //9
                case 12: key = "9"; break;
                //MUTE
                case 13: key = "MuteToggle"; break;
                //CH_DOWN
                case 14: key = "Ch-"; break;
                //0
                case 15: key = "0"; break;
                //CH_UP
                case 16: key = "Ch+"; break;
                //GREEN
                case 17: key = "Green"; break;
                //YELLOW
                case 18: key = "Yellow"; break;
                //CYAN(BLUE)
                case 19: key = "Blue"; break;
                //MENU
                case 20: key = "Menu"; break;
                //DISPLAY
                case 21: key = "Display"; break;
                //DIGIT
                case 22: key = "Digit"; break;
                //PIP_TV_VIDEO
                case 23: key = "PIP"; break;
                //EXIT
                case 24: key = "Exit"; break;
                //REWND
                case 25: key = "Rewind"; break;
                //STOP
                case 26: key = "Stop"; break;
                //PLAY
                case 27: key = "Play"; break;
                //FAST_FORWARD
                case 28: key = "FastForward"; break;
                //PAUSE
                case 29: key = "Pause"; break;
                //TOOLS
                case 30: key = "Tools"; break;
                //RETURN
                case 31: key = "Return"; break;
                //MAGNIFICO_LITE
                case 32: key = "Magnifico"; break;
                //UP
                case 33: key = "Up"; break;
                //DOWN
                case 34: key = "Down"; break;
                //RIGHT
                case 35: key = "Right"; break;
                //LEFT
                case 36: key = "Left"; break;
                //ENTER
                case 37: key = "Enter"; break;
                //RED
                case 38: key = "Red"; break;
                //LOCK
                case 39: key = "Lock"; break;
                //CONTENT
                case 40: key = "Guide"; break;
                //DISCRETE_POWER_OFF
                case 41: key = "POFF"; break;
                //3D
                case 42: key = "3D"; break;
            }
            SendMessageToSIMPL("Room" + roomID + "TVKP:" + key);
        }

        public void SkyQBtnPress(int roomID, string btnName)
        {
            RoomCoreData rcd = JsonConvert.DeserializeObject<RoomCoreData>(FileOperations.loadRoomJson(roomID, "Core"));
            RoomMenuItem rmi = new RoomMenuItem();

            foreach (var menuItem in rcd.menuItems)
                if (menuItem.menuItemName == "Sky Q")
                    rmi = menuItem;

            try
            {
                IROutputPorts[rmi.skyIRPort].PressAndRelease(btnName, 25);
            }
            catch (Exception ex) { ConsoleLogger.WriteLine("Problem in SkyQBtnPress: " + ex); }
        }

        public void SkyBtnPress(int roomID, int btnNum)
        {
            RoomCoreData rcd = JsonConvert.DeserializeObject<RoomCoreData>(FileOperations.loadRoomJson(roomID, "Core"));
            RoomMenuItem rmi = new RoomMenuItem();

            foreach(var menuItem in rcd.menuItems)
                if(menuItem.menuItemName == "Sky")
                    rmi = menuItem;

            try
            {
                switch (btnNum)
                {
                    case 1: IROutputPorts[rmi.skyIRPort].PressAndRelease("TV_GUIDE", 25); break;
                    case 0: IROutputPorts[rmi.skyIRPort].PressAndRelease("SKY", 25); break;
                    case 2: IROutputPorts[rmi.skyIRPort].PressAndRelease("I", 25); break;
                    case 3: IROutputPorts[rmi.skyIRPort].PressAndRelease("BOX_OFFICE", 25); break;
                    case 4: IROutputPorts[rmi.skyIRPort].PressAndRelease("1", 25); break;
                    case 5: IROutputPorts[rmi.skyIRPort].PressAndRelease("2", 25); break;
                    case 6: IROutputPorts[rmi.skyIRPort].PressAndRelease("3", 25); break;
                    case 7: IROutputPorts[rmi.skyIRPort].PressAndRelease("RED", 25); break;
                    case 8: IROutputPorts[rmi.skyIRPort].PressAndRelease("4", 25); break;
                    case 9: IROutputPorts[rmi.skyIRPort].PressAndRelease("5", 25); break;
                    case 10: IROutputPorts[rmi.skyIRPort].PressAndRelease("6", 25); break;
                    case 11: IROutputPorts[rmi.skyIRPort].PressAndRelease("GREEN", 25); break;
                    case 12: IROutputPorts[rmi.skyIRPort].PressAndRelease("7", 25); break;
                    case 13: IROutputPorts[rmi.skyIRPort].PressAndRelease("8", 25); break;
                    case 14: IROutputPorts[rmi.skyIRPort].PressAndRelease("9", 25); break;
                    case 15: IROutputPorts[rmi.skyIRPort].PressAndRelease("YELLOW", 25); break;
                    case 16: IROutputPorts[rmi.skyIRPort].PressAndRelease("0", 25); break;
                    case 17: IROutputPorts[rmi.skyIRPort].PressAndRelease("BLUE", 25); break;
                    case 18: IROutputPorts[rmi.skyIRPort].PressAndRelease("UP", 25); break;
                    case 19: IROutputPorts[rmi.skyIRPort].PressAndRelease("LEFT", 25); break;
                    case 20: IROutputPorts[rmi.skyIRPort].PressAndRelease("SELECT", 25); break;
                    case 21: IROutputPorts[rmi.skyIRPort].PressAndRelease("RIGHT", 25); break;
                    case 22: IROutputPorts[rmi.skyIRPort].PressAndRelease("DOWN", 25); break;
                    case 23: IROutputPorts[rmi.skyIRPort].PressAndRelease("CH+", 25); break;
                    case 24: IROutputPorts[rmi.skyIRPort].PressAndRelease("CH-", 25); break;
                    case 25: IROutputPorts[rmi.skyIRPort].PressAndRelease("REV", 25); break;
                    case 26: IROutputPorts[rmi.skyIRPort].PressAndRelease("PLAY", 25); break;
                    case 27: IROutputPorts[rmi.skyIRPort].PressAndRelease("STOP", 25); break;
                    case 28: IROutputPorts[rmi.skyIRPort].PressAndRelease("RECORD", 25); break;
                    case 29: IROutputPorts[rmi.skyIRPort].PressAndRelease("FFWD", 25); break;
                    case 30: IROutputPorts[rmi.skyIRPort].PressAndRelease("BACK_UP", 25); break;
                    case 31: IROutputPorts[rmi.skyIRPort].PressAndRelease("PAUSE", 25); break;
                }
            }
            catch (Exception ex)
            {
                ConsoleLogger.WriteLine("Problem in Sky: " + ex);
            }
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