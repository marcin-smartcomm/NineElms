using System;
using Crestron.SimplSharp;                          	// For Basic SIMPL# Classes
using Crestron.SimplSharp.CrestronIO;
using Crestron.SimplSharpPro;                       	// For Basic SIMPL#Pro classes
using Crestron.SimplSharpPro.CrestronThread;        	// For Threading
using System.Collections.Generic;
using System.Text;
using Crestron.SimplSharpPro.DM.Streaming;

namespace _9ElmsMain
{
    public class ControlSystem : CrestronControlSystem
    {
        public ProcessorSettings _processorSettings;
        public List<Room> rooms;
        Relay _fireplace;
        AsyncTCPClient _SimplWindowsComms;
        BGMController _AudioProcessor;
        SonosController[] _SonosController;

        HVACProcessor _HvacComms;
        LutronProcessor _lutronComms;

        //Sky
        DmNvx360 _skyTransmitter;
        Sky _skybox;
        IROutputPort _skyIRPort;

        Touchpannel[] _wallPanels;
        Touchpannel _masterIpad;

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

                try
                {
                    ConsoleLogger.Start();
                    _processorSettings = FileOperations.loadProcessorSettings();
                    ProcessorInfo.ID = _processorSettings.processorId;
                    _SonosController = new SonosController[_processorSettings.sonosNames.Length];
                    rooms = new List<Room>();

                    if (this.SupportsEthernet)
                    {
                        InitializeEquipment();
                        InitializeRooms();
                        InitializeTPs();
                    }
                    if (this.SupportsRelay)
                    {
                        _fireplace = this.RelayPorts[1];
                        if (_fireplace.Register() != eDeviceRegistrationUnRegistrationResponse.Success)
                            ConsoleLogger.WriteLine("Error Registering fireplace Relay: " + _fireplace.DeviceRegistrationFailureReason);
                    }
                    if (this.SupportsIROut)
                    {
                        string IRPath = string.Format("{0}/SkyQ.ir", Directory.GetApplicationDirectory());

                        ConsoleLogger.WriteLine("Registering IR Devices...");
                        if (ControllerIROutputSlot.Register() != eDeviceRegistrationUnRegistrationResponse.Success)
                            ConsoleLogger.WriteLine("Problem Registering IR Devices: " + ControllerIROutputSlot.DeviceRegistrationFailureReason);
                        else
                        {
                            _skyIRPort = IROutputPorts[1];
                            ConsoleLogger.WriteLine("IR Ports Registered successfully");
                            _skyIRPort.LoadIRDriver(IRPath);
                        }
                    }
                }
                catch(Exception ex)
                {
                    ConsoleLogger.WriteLine("Problem in ControlSystem Constructor: " + ex);
                }
            }
            catch (Exception e)
            {
                ErrorLog.Error("Error in the constructor: {0}", e.Message);
            }
        }

        void InitializeEquipment()
        {
            try
            {
                _SimplWindowsComms = new AsyncTCPClient(_processorSettings.SIMPLControllerIP, _processorSettings.SIMPLControllerPort, 4000);
                _SimplWindowsComms.ConnectedEvent += _SimplWindowsComms_ConnectedEvent;
                _SimplWindowsComms.MessageReceived += _SimplWindowsComms_MessageReceived;
                _SimplWindowsComms.ConnectRequest(1);

                _AudioProcessor = new BGMController(_SimplWindowsComms);
                _lutronComms = new LutronProcessor(_SimplWindowsComms);
                _HvacComms = new HVACProcessor(_SimplWindowsComms);

                _skyTransmitter = new DmNvx360(_processorSettings.skyTransmitterIPID, this);
                _skyTransmitter.Description = "Sky Box in Rack";
                _skyTransmitter.OnlineStatusChange += _skyTransmitter_OnlineStatusChange;
                if (_skyTransmitter.Register() != eDeviceRegistrationUnRegistrationResponse.Success)
                    ConsoleLogger.WriteLine("Problem Registering Sky Box NVX: " + _skyTransmitter.RegistrationFailureReason);
            }
            catch (Exception ex)
            {
                ConsoleLogger.WriteLine("Problem in ControlSystem InitializeEquipment: " + ex);
            }

            if(_processorSettings.sonosNames.Length > 0)
            {
                for(int i = 0; i < _processorSettings.sonosNames.Length; i++)
                {
                    _SonosController[i] = new SonosController(_SimplWindowsComms, _processorSettings.sonosNames[i], (i + 1));
                }
            }

            _skybox = new Sky(_skyIRPort);
        }
        void InitializeRooms()
        {
            for (int i = 0; i < _processorSettings.roomCount; i++)
            {
                rooms.Add(new Room(i+1,_AudioProcessor, _skyTransmitter, _skybox, _lutronComms, _HvacComms, _fireplace, this));
            }

            AddSonosToRooms(_processorSettings.processorId);
        }
        void AddSonosToRooms(short processorID)
        {
            if (processorID == 1)
            {
                rooms[2].SetSonosController(_SonosController[0]);
                rooms[5].SetSonosController(_SonosController[1]);
                rooms[6].SetSonosController(_SonosController[2]);
                rooms[7].SetSonosController(_SonosController[3]);
                rooms[8].SetSonosController(_SonosController[4]);
            }
            if (processorID == 2)
            {
                //Private Dining
                rooms[3].SetSonosController(_SonosController[0]);
            }
            if (processorID == 3)
            {
                //No Sonos On this floor
            }
        }
        void InitializeTPs()
        {
            short TOUCHPANNEL_COUNT = _processorSettings.TPCount;

            _wallPanels = new Touchpannel[TOUCHPANNEL_COUNT];
            _masterIpad = new Touchpannel(50100, rooms[0], this);
            _masterIpad.Start();

            if (_processorSettings.processorId == 1)
            {
                _wallPanels[0] = new Touchpannel(50000, rooms[0], this);
                _wallPanels[1] = new Touchpannel(50001, rooms[2], this);
                _wallPanels[2] = new Touchpannel(50002, rooms[3], this);
                _wallPanels[3] = new Touchpannel(50003, rooms[5], this);
                _wallPanels[4] = new Touchpannel(50004, rooms[6], this);
                _wallPanels[5] = new Touchpannel(50005, rooms[7], this);
                _wallPanels[6] = new Touchpannel(50006, rooms[8], this);
            }
            if(_processorSettings.processorId == 2)
            {
                _wallPanels[0] = new Touchpannel(50000, rooms[1], this);
                _wallPanels[1] = new Touchpannel(50001, rooms[2], this);
                _wallPanels[2] = new Touchpannel(50002, rooms[3], this);
            }
            if(_processorSettings.processorId == 3)
            {
                _wallPanels[0] = new Touchpannel(50000, rooms[0], this);
                _wallPanels[1] = new Touchpannel(50001, rooms[2], this);
                _wallPanels[2] = new Touchpannel(50002, rooms[3], this);
            }

            for (int i = 0; i < TOUCHPANNEL_COUNT; i++)
                _wallPanels[i].Start();
        }

        public string GetRoomsNames()
        {
            string toReturn = "";

            for(int i = 0; i < rooms.Count; i++)
            {
                if(i == rooms.Count - 1)
                    toReturn += rooms[i].GetRoomName();
                else
                    toReturn += rooms[i].GetRoomName() + ":";
            }

            return toReturn;
        }

        private void _SimplWindowsComms_MessageReceived(object source, MessageReceivedEventArgs args)
        {
            try
            {
                string fromSIMPLWindows = Encoding.ASCII.GetString(args.message);
                ConsoleLogger.WriteLine("Message Received from SIMPL Windows: " + fromSIMPLWindows);

                if (fromSIMPLWindows.Contains("GetSonosNames"))
                {
                    for (int i = 0; i < _processorSettings.sonosNames.Length; i++)
                    {
                        Thread.Sleep(200);
                        _SimplWindowsComms.SendMessage("Sonos" + (i + 1) + ":Name:" + _processorSettings.sonosNames[i]);
                    }
                }

                else if (fromSIMPLWindows.Contains("Sonos"))
                    _SonosController[int.Parse(fromSIMPLWindows.Split(':')[0].Remove(0, 5)) - 1].EvaluateString(fromSIMPLWindows);

                else if (fromSIMPLWindows.Contains("BGM"))
                    _AudioProcessor.EvaluateString(fromSIMPLWindows);

                else if (fromSIMPLWindows.Contains("Lutron"))
                {
                    string procIDString = fromSIMPLWindows.Split(':')[1];
                    int processorID = int.Parse(procIDString.Remove(0, 4));

                    if (ProcessorInfo.ID == processorID)
                        _lutronComms.evaluateMessage(fromSIMPLWindows);
                }

                else if (fromSIMPLWindows.Contains("HVAC"))
                {
                    string procIDString = fromSIMPLWindows.Split(':')[1];
                    int processorID = int.Parse(procIDString.Remove(0, 4));

                    if (ProcessorInfo.ID == processorID)
                        _HvacComms.evaluateMessage(fromSIMPLWindows);
                }
            }
            catch(Exception ex)
            {
                ConsoleLogger.WriteLine("Problem in ControlSystem _SimplWindowsComms_MessageReceived: " + ex);
            }
        }
        private void _SimplWindowsComms_ConnectedEvent(bool connected)
        {
            if(connected)
                ConsoleLogger.WriteLine("Connected to SIMPL Windows");
            else
                ConsoleLogger.WriteLine("Lost Connection To SIMPL Windows");
        }

        private void _skyTransmitter_OnlineStatusChange(GenericBase currentDevice, OnlineOfflineEventArgs args)
        {
            if (args.DeviceOnLine)
            {
                _skyTransmitter.Control.EnableAutomaticInitiation();
            }
        }

        void _ControllerEthernetEventHandler(EthernetEventArgs ethernetEventArgs)
        {
            switch (ethernetEventArgs.EthernetEventType)
            {//Determine the event type Link Up or Link Down
                case (eEthernetEventType.LinkDown):
                    //Next need to determine which adapter the event is for. 
                    //LAN is the adapter is the port connected to external networks.
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
                    //The program has been paused.  Pause all user threads/timers as needed.
                    break;
                case (eProgramStatusEventType.Resumed):
                    //The program has been resumed. Resume all the user threads/timers as needed.
                    break;
                case (eProgramStatusEventType.Stopping):
                    //The program has been stopped.
                    //Close all threads. 
                    //Shutdown all Client/Servers in the system.
                    //General cleanup.
                    //Unsubscribe to all System Monitor events
                    break;
            }

        }
        void _ControllerSystemEventHandler(eSystemEventType systemEventType)
        {
            switch (systemEventType)
            {
                case (eSystemEventType.DiskInserted):
                    //Removable media was detected on the system
                    break;
                case (eSystemEventType.DiskRemoved):
                    //Removable media was detached from the system
                    break;
                case (eSystemEventType.Rebooting):
                    //The system is rebooting. 
                    //Very limited time to preform clean up and save any settings to disk.
                    break;
            }

        }
    }
}