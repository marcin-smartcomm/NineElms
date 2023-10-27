using Crestron.SimplSharp;
using System;
using System.Collections.Generic;
using System.Timers;
using WebsocketServer;

namespace _9ElmsMain
{

    public class Touchpannel
    {
        int tpID;
        Room currentRoom;

        public ControlSystem controlSystem;

        private static Timer aTimer;

        private WebsocketSrvr CommsServer;
        private bool _clientConnected;

        private List<string> _backlog;
        bool isPinging = false;

        public Touchpannel(int port, Room currentRoom, ControlSystem cs)
        {
            try
            {
                controlSystem = cs;

                tpID = port - 50000;
                this.currentRoom = currentRoom;
                SubscribeToRoomEvents();

                CommsServer = new WebsocketSrvr();
                CommsServer.Initialize(port);
                CommsServer.OnClientConnectedChange += OnClientConnected;
                CommsServer.OnStringSignalChange += OnReceivingMessage;

                _backlog = new List<string>();

                _clientConnected = false;

                aTimer = new Timer();
                aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                aTimer.Interval = 59000;
                aTimer.Enabled = true;
            }
            catch (Exception e)
            {
                ConsoleLogger.WriteLine("TP Constructor issue: \n" + e.ToString());
            }
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            if (!isPinging)
            {
                Stop();
                Start();
            }
            isPinging = false;
        }

        void SubscribeToRoomEvents()
        {
            currentRoom.ActualTempChanged += CurrentRoom_ActualTempChanged;
            currentRoom.DesiredTempChanged += CurrentRoom_DesiredTempChanged;
            currentRoom.FireplaceStateChanged += CurrentRoom_FireplaceStateChanged;
            currentRoom.LightSceneChanged += CurrentRoom_LightSceneChanged;
            currentRoom.RoomMuteStateChanged += CurrentRoom_RoomMuteStateChanged;
            currentRoom.IndividalMuteStateChanged += CurrentRoom_IndividalMuteStateChanged;
            currentRoom.RoomVolChanged += CurrentRoom_RoomVolChanged;
            currentRoom.RoomZoneVolChanged += CurrentRoom_RoomZoneVolChanged;
            currentRoom.SourceSelectedChanged += CurrentRoom_SourceSelectedChanged;
        }
        void UnsubscribeFromRoomEvents()
        {
            currentRoom.ActualTempChanged -= CurrentRoom_ActualTempChanged;
            currentRoom.DesiredTempChanged -= CurrentRoom_DesiredTempChanged;
            currentRoom.FireplaceStateChanged -= CurrentRoom_FireplaceStateChanged;
            currentRoom.LightSceneChanged -= CurrentRoom_LightSceneChanged;
            currentRoom.RoomMuteStateChanged -= CurrentRoom_RoomMuteStateChanged;
            currentRoom.IndividalMuteStateChanged -= CurrentRoom_IndividalMuteStateChanged;
            currentRoom.RoomVolChanged -= CurrentRoom_RoomVolChanged;
            currentRoom.RoomZoneVolChanged -= CurrentRoom_RoomZoneVolChanged;
            currentRoom.SourceSelectedChanged -= CurrentRoom_SourceSelectedChanged;
        }

        private void CurrentRoom_RoomVolChanged(int newVol) => SendVolume(newVol);
        private void CurrentRoom_RoomZoneVolChanged(int zoneNum, int newVol) => SendIndividualVolume(zoneNum+1, newVol);
        private void CurrentRoom_RoomMuteStateChanged(bool newState) => SendMuteState(newState);
        private void CurrentRoom_IndividalMuteStateChanged(int zoneNum, bool newState) => SendIndividualMuteState(zoneNum+1, newState);
        private void CurrentRoom_SourceSelectedChanged(string newSource)
        {
            SendSourceSelected();
            SendVolume(currentRoom.GetVolLevel());
            SendMuteState(currentRoom.GetMuteState());
        }

        private void CurrentRoom_LightSceneChanged(int newScene) => SendLightScene();

        private void CurrentRoom_FireplaceStateChanged(bool newState) => SendFireplaceState(newState);
        
        private void CurrentRoom_DesiredTempChanged(float newTemp) => SendDesiredTemp();
        private void CurrentRoom_ActualTempChanged(float newTemp) => SendActualTemp();

        public void Start()
        {
            CommsServer.StartServer();
        }
        public void Stop()
        {
            CommsServer.StopServer();
            currentRoom.DisconnectRoomEquipment(tpID);
        }

        public void WriteLine(string msg, params object[] args)
        {
            var text = String.Format(msg, args) + "\n";

            if (_clientConnected)
            {
                CommsServer.SetIndirectTextSignal(1, text);
            }
            else
            {
                _backlog.Add(text);
            }
        }
        private void OnClientConnected(ushort state)
        {
            if (state == 0)
            {
                // Disconnected
                _clientConnected = false;
            }
            else
            {
                // Connected
                _clientConnected = true;
                CommsServer.SetIndirectTextSignal(1, "\n-- CONNECTED --\n");

                if (_backlog.Count > 0)
                {
                    foreach (var msg in _backlog)
                    {
                        CommsServer.SetIndirectTextSignal(1, msg);
                    }
                }

                _backlog.Clear();
            }
        }
        private void OnReceivingMessage(ushort state, SimplSharpString value)
        {
            ConsoleLogger.WriteLine(value.ToString());
            if (value.ToString() == "__ping__")
            {
                isPinging = true;
                CommsServer.SetIndirectTextSignal(1, "__pong__");
            }
            else
                evaluateString(value.ToString());
        }

        void SendSources()
        {
            string[] roomSources = currentRoom.GetSources();
            if (roomSources != null)
            {
                string toReturn = "Sources ";
                foreach (string source in roomSources)
                {
                    if (source == roomSources[roomSources.Length - 1])
                    {
                        toReturn += source;
                    }
                    else
                    {
                        toReturn += source + ":";
                    }
                }
                CommsServer.SetIndirectTextSignal(1, toReturn);
            }
        }
        void SendRoomName() => CommsServer.SetIndirectTextSignal(1, "RoomName " + currentRoom.GetRoomName());
        void SendSourceSelected() => CommsServer.SetIndirectTextSignal(1, "SourceSelected " + currentRoom.GetSourceSelected());
        void SendVolume(int newVol)
        {
            if (newVol > -1 && newVol < 101)
                CommsServer.SetIndirectTextSignal(1, "Volume " + newVol);
        }
        void SendIndividualVolume(int zoneNum, int newVol) => CommsServer.SetIndirectTextSignal(1, "ZoneVolume " + zoneNum+":"+newVol);
        void SendMuteState(bool newState) => CommsServer.SetIndirectTextSignal(1, "MuteState " + newState);
        void SendIndividualMuteState(int zoneNum, bool newState) => CommsServer.SetIndirectTextSignal(1, "ZoneMuteState " + zoneNum+":"+newState);
        void SendLightScene() => CommsServer.SetIndirectTextSignal(1, "LightScene " + currentRoom.GetSettings().lightSceneSelected);
        void SendActualTemp() => CommsServer.SetIndirectTextSignal(1, "ActualTemp " + currentRoom.GetActualTemp());
        void SendDesiredTemp() => CommsServer.SetIndirectTextSignal(1, "DesiredTemp " + currentRoom.GetDesiredTemp());
        void SendFireplaceState(bool newState)
        {
            ConsoleLogger.WriteLine("Fireplace state changing");
            CommsServer.SetIndirectTextSignal(1, "FireplaceState " + newState);
        }
        void SendMasteriPadState()
        {
            if(tpID == 100)
                CommsServer.SetIndirectTextSignal(1, "MasteriPad True");
        }
        void SendRoomsList() => CommsServer.SetIndirectTextSignal(1, "RoomsList " + controlSystem.GetRoomsNames());

        void SendFireAlarmState() => CommsServer.SetIndirectTextSignal(1, "FireAlarm " + controlSystem.fireAlarmState);

        void evaluateString(string incomingRequest)
        {
            try
            {
                if (incomingRequest.Contains("GetRoomName")) { SendRoomName(); SendMasteriPadState(); }

                else if (incomingRequest.Contains("hasSonos")) CommsServer.SetIndirectTextSignal(1, "Sonos " + currentRoom.SonosExists());
                else if (incomingRequest.Contains("hasBGM")) CommsServer.SetIndirectTextSignal(1, "BGM " + currentRoom.GetSettings().hasBGMusic);
                else if (incomingRequest.Contains("hasLights"))
                {
                    CommsServer.SetIndirectTextSignal(1, "Lights " + currentRoom.GetSettings().hasLights);
                    SendLightScene();
                }
                else if (incomingRequest.Contains("hasHVAC"))
                {
                    CommsServer.SetIndirectTextSignal(1, "HVAC " + currentRoom.GetSettings().hasHVAC);
                    SendActualTemp();
                    SendDesiredTemp();
                    ConsoleLogger.WriteLine(currentRoom.GetDesiredTemp().ToString());
                }
                else if (incomingRequest.Contains("hasFireplace"))
                {
                    CommsServer.SetIndirectTextSignal(1, "Fireplace " + currentRoom.GetSettings().hasFirePlace);
                    if (currentRoom.GetSettings().hasFirePlace)
                        SendFireplaceState(controlSystem.GetFireplaceState());
                }
                else if (incomingRequest.Contains("hasTV"))
                {
                    string toReturn = "TVs ";

                    if (currentRoom.GetSettings().TVNames.Length == 0)
                    {
                        toReturn += "null";
                        CommsServer.SetIndirectTextSignal(1, toReturn);
                        return;
                    }

                    for (int i = 0; i < currentRoom.GetSettings().TVNames.Length; i++)
                    {
                        if (i == currentRoom.GetSettings().TVNames.Length - 1)
                            toReturn += currentRoom.GetSettings().TVNames[i];
                        else
                            toReturn += currentRoom.GetSettings().TVNames[i] + ":";
                    }

                    CommsServer.SetIndirectTextSignal(1, toReturn);
                }

                else if (incomingRequest.Contains("GetSources")) SendSources();
                else if (incomingRequest.Contains("GetSourceSelected")) SendSourceSelected();
                else if (incomingRequest.Contains("GetVolumeLevel")) SendVolume(currentRoom.GetVolLevel());
                else if (incomingRequest.Contains("GetIndividualVolumes"))
                {
                    if (currentRoom.GetRoomName().Contains("External Terrace"))
                    {
                        SendIndividualVolume(1, currentRoom.GetZoneVol(0));
                        SendIndividualVolume(2, currentRoom.GetZoneVol(1));
                        SendIndividualVolume(3, currentRoom.GetZoneVol(2));
                        SendIndividualVolume(4, currentRoom.GetZoneVol(3));
                    }
                    else
                    {
                        SendIndividualVolume(1, currentRoom.GetZoneVol(0));
                        SendIndividualVolume(2, currentRoom.GetZoneVol(1));
                    }
                }
                else if (incomingRequest.Contains("GetMuteState")) SendMuteState(currentRoom.GetMuteState());
                else if (incomingRequest.Contains("GetIndividualMutes"))
                {
                    if (currentRoom.GetRoomName().Contains("External Terrace"))
                    {
                        SendIndividualMuteState(1, currentRoom.GetZoneMuteState(0));
                        SendIndividualMuteState(2, currentRoom.GetZoneMuteState(1));
                        SendIndividualMuteState(3, currentRoom.GetZoneMuteState(2));
                        SendIndividualMuteState(4, currentRoom.GetZoneMuteState(3));
                    }
                    else
                    {
                        SendIndividualMuteState(1, currentRoom.GetZoneMuteState(0));
                        SendIndividualMuteState(2, currentRoom.GetZoneMuteState(1));
                    }
                }

                else if (incomingRequest.Contains("RoomChange"))
                {
                    UnsubscribeFromRoomEvents();
                    currentRoom =
                        controlSystem.rooms[
                            controlSystem.rooms.IndexOf(
                                controlSystem.rooms.Find(x => x.GetRoomName() == incomingRequest.Split(':')[1]))
                            ];
                    SubscribeToRoomEvents();

                    CommsServer.SetIndirectTextSignal(1, "RoomChanged");
                }
                else if (incomingRequest.Contains("GetRoomsList")) SendRoomsList();
                else if (incomingRequest.Contains("GetProcessorID")) CommsServer.SetIndirectTextSignal(1, "ProcessorID " + ProcessorInfo.ID);
                else if (incomingRequest.Contains("ConnectEquipment")) currentRoom.ConnectRoomEquipment(tpID);
                else if (incomingRequest.Contains("DisconnectEquipment")) currentRoom.DisconnectRoomEquipment(tpID);

                else if (incomingRequest.Contains("VolumeUp")) currentRoom.VolUp();
                else if (incomingRequest.Contains("VolumeDown")) currentRoom.VolDown();
                else if (incomingRequest.Contains("IndividualVolume")) currentRoom.SetIndividualVolumeLevel(int.Parse(incomingRequest.Split(':')[1]), int.Parse(incomingRequest.Split(':')[2]));
                else if (incomingRequest.Contains("Volume")) currentRoom.SetNewVolumeLevel(int.Parse(incomingRequest.Split(':')[1]));
                else if (incomingRequest.Contains("IndividualMute")) currentRoom.SetIndividualMute(int.Parse(incomingRequest.Split(':')[1]));
                else if (incomingRequest.Contains("Mute")) currentRoom.Mute();
                else if (incomingRequest.Contains("SetSourceSelected")) currentRoom.SetNewSource(incomingRequest.Split(':')[1]);
                else if (incomingRequest.Contains("srcBtn"))
                {
                    int btnNum = int.Parse(incomingRequest.Split(':')[1]);
                    string source = incomingRequest.Split(':')[2];
                    ConsoleLogger.WriteLine("Touchpanel.EvaluateString.btnNum: " + btnNum);

                    currentRoom.SourceBtnPressed(btnNum, source);
                }
                else if (incomingRequest.Contains("RoomOff"))
                {
                    currentRoom.SetNewSource("Off");
                    CommsServer.SetIndirectTextSignal(1, "SourceSelected " + currentRoom.GetSourceSelected());
                }
                else if (incomingRequest.Contains("TVOff")) currentRoom.TVOff();
                else if (incomingRequest.Contains("TV"))
                {
                    currentRoom.SetIndividualTVSource(incomingRequest.Split(':')[0], incomingRequest.Split(':')[1]);
                    if (ProcessorInfo.ID == 2 && currentRoom.GetRoomName().Contains("Games Room") && currentRoom.GetSourceSelected() == "Sky")
                    {
                        SendVolume(currentRoom.GetVolLevel());
                        SendMuteState(currentRoom.GetMuteState());
                    }
                }

                else if (incomingRequest.Contains("SetLightScene")) currentRoom.SetLightScene(int.Parse(incomingRequest.Split(':')[1]));
                else if (incomingRequest.Contains("SetDim")) currentRoom.SetDimState(incomingRequest.Split(':')[1], incomingRequest.Split(':')[2]);

                else if (incomingRequest.Contains("TempUp")) currentRoom.TempUp();
                else if (incomingRequest.Contains("TempDown")) currentRoom.TempDown();

                else if (incomingRequest.Contains("SetFireplace"))
                {
                    bool newState = bool.Parse(incomingRequest.Split(':')[1]);
                    controlSystem.SetFirePlaceState(newState);
                }

                else if (incomingRequest.Contains("GetFireAlarmState")) SendFireAlarmState();
            }
            catch (Exception ex)
            {
                ConsoleLogger.WriteLine("Problem in Touchpannel.evaluateString: " + ex);
            }
        }
    }
}
