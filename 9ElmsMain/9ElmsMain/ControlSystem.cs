using System;
using Crestron.SimplSharp;                          	// For Basic SIMPL# Classes
using Crestron.SimplSharpPro.GeneralIO;
using Crestron.SimplSharp.CrestronIO;
using Crestron.SimplSharpPro;                       	// For Basic SIMPL#Pro classes
using Crestron.SimplSharpPro.CrestronThread;        	// For Threading
using System.Collections.Generic;
using System.Text;
using Crestron.SimplSharpPro.DM.Streaming;
using Crestron.SimplSharpPro.EthernetCommunication;
using Crestron.RAD.Common.Logging;
using System.Threading.Tasks;

namespace _9ElmsMain
{
    public class ControlSystem : CrestronControlSystem
    {
        public ProcessorSettings _processorSettings;
        public List<Room> rooms;
        public ThreeSeriesTcpIpEthernetIntersystemCommunications _SimplWindowsComms, _linkGFand10th, _linkGFand17th;
        Relay _fireplace;
        BGMController _AudioProcessor;
        SonosController[] _SonosController;

        HVACProcessor _HvacComms;
        LutronProcessor _lutronComms;

        //Sky
        Sky _skybox1, _skybox2;
        IROutputPort _sky1_IRPort, _sky2_IRPort;

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
                        ConsoleLogger.WriteLine("Supports Ethernet");
                        InitializeEquipment();
                        InitializeRooms();
                        InitializeTPs();
                    }
                    if (this.SupportsRelay)
                    {
                        ConsoleLogger.WriteLine("Supports Relay");
                        _fireplace = this.RelayPorts[1];
                        if (_fireplace.Register() != eDeviceRegistrationUnRegistrationResponse.Success)
                            ConsoleLogger.WriteLine("Error Registering fireplace Relay: " + _fireplace.DeviceRegistrationFailureReason);
                        _fireplace.StateChange += _fireplace_StateChange;
                    }
                    if (this.SupportsIROut)
                    {
                        ConsoleLogger.WriteLine("Supports IR Ports");
                        string IRPath = string.Format("{0}/nvram/SkyHD.ir", Directory.GetDirectoryRoot(Directory.GetApplicationDirectory()));
                        ConsoleLogger.WriteLine("getting IR file from: " + IRPath);

                        ConsoleLogger.WriteLine("Registering IR Devices...");
                        if (ControllerIROutputSlot.Register() != eDeviceRegistrationUnRegistrationResponse.Success)
                            ConsoleLogger.WriteLine("Problem Registering IR Devices: " + ControllerIROutputSlot.DeviceRegistrationFailureReason);
                        else
                        {
                            _sky1_IRPort = IROutputPorts[2];
                            ConsoleLogger.WriteLine("Sky1 IR Ports Registered successfully");
                            _sky1_IRPort.LoadIRDriver(IRPath);
                            ConsoleLogger.WriteLine("Sky1 IR Driver Loaded successfully");

                            _sky2_IRPort = IROutputPorts[1];
                            ConsoleLogger.WriteLine("Sky2 IR Ports Registered successfully");
                            _sky2_IRPort.LoadIRDriver(IRPath);
                            ConsoleLogger.WriteLine("Sky2 IR Driver Loaded successfully");

                            foreach (string s in _sky2_IRPort.AvailableIRCmds())
                                ConsoleLogger.WriteLine("Sky IR: {0}", s);
                        }
                    }
                    if(this.SupportsVersiport)
                    {
                        ConsoleLogger.WriteLine("Supports Versiport");
                        if (this.VersiPorts[1].Register() != eDeviceRegistrationUnRegistrationResponse.Success)
                        {
                            ConsoleLogger.WriteLine("Error Registering Versiport1: {0}", this.VersiPorts[1].DeviceRegistrationFailureReason);
                        }
                        else
                        {
                            if (this.VersiPorts[1].SupportsDigitalInput)
                            {
                                ConsoleLogger.WriteLine("Configuring versiport as Digital In");
                                this.VersiPorts[1].SetVersiportConfiguration(eVersiportConfiguration.DigitalInput);
                            }

                            this.VersiPorts[1].VersiportChange += FireAlarmRelay_VersiportChange;
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
        public void SetFirePlaceState(bool newState)
        {
            _fireplace.State = newState;
        }
        private void _fireplace_StateChange(Relay relay, RelayEventArgs args)
        {
            ConsoleLogger.WriteLine("Fireplace Relay changed state: " + args.State);
            if (ProcessorInfo.ID == 1)
                rooms[5].OnFireplaceStateChanged(args.State);
            if (ProcessorInfo.ID == 3)
                rooms[3].OnFireplaceStateChanged(args.State);
        }

        public bool GetFireplaceState()
        {
            return _fireplace.State;
        }

        private void FireAlarmRelay_VersiportChange(Versiport port, VersiportEventArgs args)
        {
            if(ProcessorInfo.ID == 1)
            {
                ConsoleLogger.WriteLine("Port" + port.DeviceName + "state changed to: " + args.Event + "Digital In State: " + port.DigitalIn);
                if (!port.DigitalIn)
                    ConsoleLogger.WriteLine("FireAlarm recorded at: " + DateTime.Now);

                SetFireAlarmState(port.DigitalIn);
                _linkGFand10th.BooleanInput[1].BoolValue = port.DigitalIn;
                _linkGFand17th.BooleanInput[1].BoolValue = port.DigitalIn;
            }
        }

        string[] previousSources;
        public bool fireAlarmState;
        public void SetFireAlarmState(bool state)
        {
            fireAlarmState = !state;
            try
            {
                if (state)
                {
                    ConsoleLogger.WriteLine("Fire Alarm Cleared, Reselecting Sources in All zones...");
                    for (int i = 0; i < _wallPanels.Length; i++)
                        _wallPanels[i].WriteLine("FireAlarm " + fireAlarmState);

                    Task.Run(() =>
                    {
                        if (previousSources.Length == rooms.Count)
                            for (int i = 0; i < rooms.Count; i++)
                            {
                                rooms[i].SetNewSource(previousSources[i]);
                                Thread.Sleep(250);
                            }
                    });
                }
                else
                {
                    ConsoleLogger.WriteLine("Fire Alarm, Switching Off All zones...");
                    for (int i = 0; i < _wallPanels.Length; i++)
                        _wallPanels[i].WriteLine("FireAlarm " + fireAlarmState);

                    previousSources = new string[rooms.Count];
                    for (int i = 0; i < rooms.Count; i++)
                        previousSources[i] = rooms[i].GetSourceSelected();

                    Task.Run(() =>
                    {
                        foreach (var room in rooms) 
                        {
                            room.SetNewSource("Off");
                            Thread.Sleep(250);
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                ConsoleLogger.WriteLine("Exception While Informing: " + ex);
            }
        }

        void InitializeEquipment()
        {
            try
            {
                _SimplWindowsComms = new ThreeSeriesTcpIpEthernetIntersystemCommunications(0xB0, "127.0.0.2", this);
                if(_SimplWindowsComms.Register() != eDeviceRegistrationUnRegistrationResponse.Success)
                    ConsoleLogger.WriteLine("Failed To Register Comms with Simpl Windows");
                else
                    _SimplWindowsComms.SigChange += _SimplWindowsComms_SigChange;

                if(ProcessorInfo.ID == 1)
                {
                    _linkGFand10th = new ThreeSeriesTcpIpEthernetIntersystemCommunications(0xB1, "172.16.98.102", this);
                    if (_linkGFand10th.Register() != eDeviceRegistrationUnRegistrationResponse.Success)
                        ConsoleLogger.WriteLine("Failed To Register link GF and 10th floor");
                    else
                        _linkGFand10th.SigChange += _linkGFand10th_SigChange;

                    _linkGFand17th = new ThreeSeriesTcpIpEthernetIntersystemCommunications(0xB2, "172.16.98.101", this);
                    if (_linkGFand17th.Register() != eDeviceRegistrationUnRegistrationResponse.Success)
                        ConsoleLogger.WriteLine("Failed To Register link GF and 10th floor");
                    else
                        _linkGFand17th.SigChange += _linkGFand17th_SigChange;
                }

                if (ProcessorInfo.ID == 2)
                {
                    _linkGFand10th = new ThreeSeriesTcpIpEthernetIntersystemCommunications(0xB1, "172.16.98.100", this);
                    if (_linkGFand10th.Register() != eDeviceRegistrationUnRegistrationResponse.Success)
                        ConsoleLogger.WriteLine("Failed To Register link GF and 10th floor");
                    else
                        _linkGFand10th.SigChange += _linkGFand10th_SigChange;
                }

                if (ProcessorInfo.ID == 3)
                {
                    _linkGFand17th = new ThreeSeriesTcpIpEthernetIntersystemCommunications(0xB2, "172.16.98.100", this);
                    if (_linkGFand17th.Register() != eDeviceRegistrationUnRegistrationResponse.Success)
                        ConsoleLogger.WriteLine("Failed To Register link GF and 10th floor");
                    else
                        _linkGFand17th.SigChange += _linkGFand17th_SigChange;
                }

                _AudioProcessor = new BGMController(this);
                _lutronComms = new LutronProcessor(this);
                _HvacComms = new HVACProcessor(this);
            }
            catch (Exception ex)
            {
                ConsoleLogger.WriteLine("Problem in ControlSystem InitializeEquipment: " + ex);
            }

            if(_processorSettings.sonosNames.Length > 0)
                for(int i = 0; i < _processorSettings.sonosNames.Length; i++)
                    _SonosController[i] = new SonosController(this, _processorSettings.sonosNames[i], (i + 1));

            _skybox1 = new Sky(2, this);
            _skybox2 = new Sky(1, this);
        }

        private void _linkGFand17th_SigChange(Crestron.SimplSharpPro.DeviceSupport.BasicTriList currentDevice, SigEventArgs args)
        {
            switch (args.Sig.Type)
            {
                case eSigType.Bool:
                    if (ProcessorInfo.ID == 3)
                        SetFireAlarmState(_linkGFand17th.BooleanOutput[1].BoolValue);
                break;
            }
        }

        private void _linkGFand10th_SigChange(Crestron.SimplSharpPro.DeviceSupport.BasicTriList currentDevice, SigEventArgs args)
        {
            switch (args.Sig.Type)
            {
                case eSigType.Bool:
                    if (ProcessorInfo.ID == 2)
                        SetFireAlarmState(_linkGFand10th.BooleanOutput[1].BoolValue);
                break;
            }
        }

        private void _SimplWindowsComms_SigChange(Crestron.SimplSharpPro.DeviceSupport.BasicTriList currentDevice, SigEventArgs args)
        {
            switch(args.Sig.Type)
            {
                case eSigType.String:
                    _SimplWindowsComms_MessageReceived(_SimplWindowsComms.StringOutput[1].StringValue);
                    break;
            }
        }

        public void SendMessage(string message)
        {
            _SimplWindowsComms.StringInput[1].StringValue = message;
        }

        void InitializeRooms()
        {
            for (int i = 0; i < _processorSettings.roomCount; i++)
            {
                if(ProcessorInfo.ID == 1)
                {
                    if (i == 5)  //if room is Resident's Lounge send Sky 2 object
                        rooms.Add(new Room(i + 1, _AudioProcessor, _skybox2, _lutronComms, _HvacComms, _fireplace, this));
                    else
                        rooms.Add(new Room(i + 1, _AudioProcessor, _skybox1, _lutronComms, _HvacComms, _fireplace, this));
                }
                if(ProcessorInfo.ID == 3)
                {
                    if (i == 2)  //if room is Bar Lounge send Sky 2 object
                        rooms.Add(new Room(i + 1, _AudioProcessor, _skybox2, _lutronComms, _HvacComms, _fireplace, this));
                    else
                        rooms.Add(new Room(i + 1, _AudioProcessor, _skybox1, _lutronComms, _HvacComms, _fireplace, this));
                }
            }

            AddSonosToRooms((short)ProcessorInfo.ID);
        }
        void AddSonosToRooms(short processorID)
        {
            if (processorID == 1)
            {
                //Play Space
                rooms[2].SetSonosController(_SonosController[0]);
                //Resident's Lounge
                //rooms[5].SetSonosController(_SonosController[1]);
                //Meeting Room 1
                rooms[6].SetSonosController(_SonosController[2]);
                //Meeting Room 2
                rooms[7].SetSonosController(_SonosController[3]);
                //Meeting Room 3
                rooms[8].SetSonosController(_SonosController[4]);
            }
            if (processorID == 2)
            {
                //Private Dining
                rooms[3].SetSonosController(_SonosController[0]);
            }
            if (processorID == 3)
            {
                //Bar Lounge
                rooms[1].SetSonosController(_SonosController[0]);
                //Indoor Lounge
                rooms[3].SetSonosController(_SonosController[1]);
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
                //Main Gym
                _wallPanels[0] = new Touchpannel(50000, rooms[0], this);
                //Play Space
                _wallPanels[1] = new Touchpannel(50001, rooms[2], this);
                //Leasing Center
                _wallPanels[2] = new Touchpannel(50002, rooms[3], this);
                //Resident's Lounge
                _wallPanels[3] = new Touchpannel(50003, rooms[5], this);
                //Meeting Room 1
                _wallPanels[4] = new Touchpannel(50004, rooms[6], this);
                //Meeting Room 2
                _wallPanels[5] = new Touchpannel(50005, rooms[7], this);
                //Meeting Room 3
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
                //Bar Lounge
                _wallPanels[0] = new Touchpannel(50000, rooms[1], this);
                //Demo Kitchen
                _wallPanels[1] = new Touchpannel(50001, rooms[2], this);
                //Indoor Lounge
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
                if (rooms[i].GetRoomName().Equals("Spare"))
                    continue;

                if(i == rooms.Count - 1)
                    toReturn += rooms[i].GetRoomName();
                else
                    toReturn += rooms[i].GetRoomName() + ":";
            }

            return toReturn;
        }

        private void _SimplWindowsComms_MessageReceived(string newMessage)
        {
            try
            {
                string fromSIMPLWindows = newMessage;
                ConsoleLogger.WriteLine("Message Received from SIMPL Windows: " + fromSIMPLWindows);

                if (fromSIMPLWindows.Contains("GetSonosNames"))
                {
                    for (int i = 0; i < _processorSettings.sonosNames.Length; i++)
                    {
                        Thread.Sleep(200);
                        _SimplWindowsComms.StringInput[1].StringValue = "Sonos" + (i + 1) + ":Name:" + _processorSettings.sonosNames[i];
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

        public void PushSky1Button(int btnNum)
        {
            try
            {
                switch (btnNum)
                {
                    case 1: _sky1_IRPort.PressAndRelease("TV_GUIDE", 25); break;
                    case 0: _sky1_IRPort.PressAndRelease("SKY", 25); break;
                    case 2: _sky1_IRPort.PressAndRelease("I", 25); break;
                    case 3: _sky1_IRPort.PressAndRelease("BOX_OFFICE", 25); break;
                    case 4: _sky1_IRPort.PressAndRelease("1", 25); break;
                    case 5: _sky1_IRPort.PressAndRelease("2", 25); break;
                    case 6: _sky1_IRPort.PressAndRelease("3", 25); break;
                    case 7: _sky1_IRPort.PressAndRelease("RED", 25); break;
                    case 8: _sky1_IRPort.PressAndRelease("4", 25); break;
                    case 9: _sky1_IRPort.PressAndRelease("5", 25); break;
                    case 10: _sky1_IRPort.PressAndRelease("6", 25); break;
                    case 11: _sky1_IRPort.PressAndRelease("GREEN", 25); break;
                    case 12: _sky1_IRPort.PressAndRelease("7", 25); break;
                    case 13: _sky1_IRPort.PressAndRelease("8", 25); break;
                    case 14: _sky1_IRPort.PressAndRelease("9", 25); break;
                    case 15: _sky1_IRPort.PressAndRelease("YELLOW", 25); break;
                    case 16: _sky1_IRPort.PressAndRelease("0", 25); break;
                    case 17: _sky1_IRPort.PressAndRelease("BLUE", 25); break;
                    case 18: _sky1_IRPort.PressAndRelease("UP", 25); break;
                    case 19: _sky1_IRPort.PressAndRelease("LEFT", 25); break;
                    case 20: _sky1_IRPort.PressAndRelease("SELECT", 25); break;
                    case 21: _sky1_IRPort.PressAndRelease("RIGHT", 25); break;
                    case 22: _sky1_IRPort.PressAndRelease("DOWN", 25); break;
                    case 23: _sky1_IRPort.PressAndRelease("CH+", 25); break;
                    case 24: _sky1_IRPort.PressAndRelease("CH-", 25); break;
                    case 25: _sky1_IRPort.PressAndRelease("REV", 25); break;
                    case 26: _sky1_IRPort.PressAndRelease("PLAY", 25); break;
                    case 27: _sky1_IRPort.PressAndRelease("STOP", 25); break;
                    case 28: _sky1_IRPort.PressAndRelease("RECORD", 25); break;
                    case 29: _sky1_IRPort.PressAndRelease("FFWD", 25); break;
                    case 30: _sky1_IRPort.PressAndRelease("BACK_UP", 25); break;
                    case 31: _sky1_IRPort.PressAndRelease("PAUSE", 25); break;
                }
            }
            catch (Exception ex)
            {
                ConsoleLogger.WriteLine("Problem in Sky: " + ex);
            }
        }
        public void PushSky2Button(int btnNum)
        {
            try
            {
                switch (btnNum)
                { 
                    case 1: _sky2_IRPort.PressAndRelease("TV_GUIDE", 25); break;
                    case 0: _sky2_IRPort.PressAndRelease("SKY", 25); break;
                    case 2: _sky2_IRPort.PressAndRelease("I", 25); break;
                    case 3: _sky2_IRPort.PressAndRelease("BOX_OFFICE", 25); break;
                    case 4: _sky2_IRPort.PressAndRelease("1", 25); break;
                    case 5: _sky2_IRPort.PressAndRelease("2", 25); break;
                    case 6: _sky2_IRPort.PressAndRelease("3", 25); break;
                    case 7: _sky2_IRPort.PressAndRelease("RED", 25); break;
                    case 8: _sky2_IRPort.PressAndRelease("4", 25); break;
                    case 9: _sky2_IRPort.PressAndRelease("5", 25); break;
                    case 10: _sky2_IRPort.PressAndRelease("6", 25); break;
                    case 11: _sky2_IRPort.PressAndRelease("GREEN", 25); break;
                    case 12: _sky2_IRPort.PressAndRelease("7", 25); break;
                    case 13: _sky2_IRPort.PressAndRelease("8", 25); break;
                    case 14: _sky2_IRPort.PressAndRelease("9", 25); break;
                    case 15: _sky2_IRPort.PressAndRelease("YELLOW", 25); break;
                    case 16: _sky2_IRPort.PressAndRelease("0", 25); break;
                    case 17: _sky2_IRPort.PressAndRelease("BLUE", 25); break;
                    case 18: _sky2_IRPort.PressAndRelease("UP", 25); break;
                    case 19: _sky2_IRPort.PressAndRelease("LEFT", 25); break;
                    case 20: _sky2_IRPort.PressAndRelease("SELECT", 25); break;
                    case 21: _sky2_IRPort.PressAndRelease("RIGHT", 25); break;
                    case 22: _sky2_IRPort.PressAndRelease("DOWN", 25); break;
                    case 23: _sky2_IRPort.PressAndRelease("CH+", 25); break;
                    case 24: _sky2_IRPort.PressAndRelease("CH-", 25); break;
                    case 25: _sky2_IRPort.PressAndRelease("REV", 25); break;
                    case 26: _sky2_IRPort.PressAndRelease("PLAY", 25); break;
                    case 27: _sky2_IRPort.PressAndRelease("STOP", 25); break;
                    case 28: _sky2_IRPort.PressAndRelease("RECORD", 25); break;
                    case 29: _sky2_IRPort.PressAndRelease("FFWD", 25); break;
                    case 30: _sky2_IRPort.PressAndRelease("BACK_UP", 25); break;
                    case 31: _sky2_IRPort.PressAndRelease("PAUSE", 25); break;
                }

                ConsoleLogger.WriteLine("Sent IR Code {0} to Sky", btnNum);
            }
            catch (Exception ex)
            {
                ConsoleLogger.WriteLine("Problem in Sky: " + ex);
            }
        }
    }
}