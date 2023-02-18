using System;
using Crestron.SimplSharpPro;
using Crestron.SimplSharpPro.AudioDistribution;
using Crestron.SimplSharpPro.DM.Streaming;

namespace _9ElmsMain
{
    public class Room
    {
        RoomSettings _settings;

        //Sonos Variables
        SonosController _sonosController;
        int _sonosVol;

        //Lights Variables
        LutronProcessor _lightsController;

        //HVAC Controller
        HVACProcessor _hvacController;
        float _actualTemp;

        //BGM Controller
        BGMController _bgmController;

        //Video
        TV[] _tv;
        Sky _skybox;

        //Fireplace
        ControlSystem _cs;

        //AV
        public event Action<int> RoomVolChanged;
        public event Action<int, int> RoomZoneVolChanged;
        public event Action<bool> RoomMuteStateChanged;
        public event Action<int, bool> IndividalMuteStateChanged;
        public event Action<string> SourceSelectedChanged;
        public event Action<bool> RoomTVConnectedChanged;

        //HVAC
        public event Action<float> ActualTempChanged;
        public event Action<float> DesiredTempChanged;

        //Lights
        public event Action<int> LightSceneChanged;

        //Fireplace
        public event Action<bool> FireplaceStateChanged;

        public Room(int roomID, BGMController BGMController, Sky skyBox,
            LutronProcessor lightsController, HVACProcessor hvacController, Relay fireplace, ControlSystem cs)
        {
            try
            {
                _settings = FileOperations.loadRoomSettings(roomID.ToString());

                _bgmController = BGMController;
                _lightsController = lightsController;
                _hvacController = hvacController;
                _cs = cs;
                _skybox = skyBox;

                SubscribeToEvents();

                if (_settings.hasTV)
                {
                    _tv = new TV[_settings.TVNames.Length];
                    for (int i = 0; i < _settings.TVNames.Length; i++)
                        _tv[i] = new TV(cs, i + 1, _settings.roomID);
                }

                ConsoleLogger.WriteLine(_settings.roomName + " registered " + _settings.TVNames.Length + " TVs");
            }
            catch (Exception ex)
            {
                ConsoleLogger.WriteLine("Problem in Room" + roomID + " Constructor " + ex);
            }
        }

        void SubscribeToEvents()
        {
            _lightsController.newSceneSelected += _lightsController_newSceneSelected;

            _hvacController.actualTempChanged += _hvacController_actualTempChanged;
            _hvacController.desiredTempChanged += _hvacController_desiredTempChanged;

            _bgmController.sourcesChanged += _bgmController_sourcesChanged;
            _bgmController.volChanged += _bgmController_volChanged;
            _bgmController.individualVolChanged += _bgmController_individualVolChanged;
            _bgmController.muteChanged += _bgmController_muteChanged;
            _bgmController.individualMuteChanged += _bgmController_individualMuteChanged;
        }

        public RoomSettings GetSettings() => _settings;
        public short GetRoomID() => _settings.roomID;
        public string GetRoomName() => _settings.roomName;

        public int GetCurrentScene() => _settings.lightSceneSelected;

        public string[] GetSources() => _settings.sources;
        public string GetSourceSelected() => _settings.sourceSelected;
        public int GetVolLevel()
        {
            string selectedSourceType = "Off";
            if (Array.IndexOf(_settings.sources, _settings.sourceSelected) > -1)
                selectedSourceType = _settings.sourceType[Array.IndexOf(_settings.sources, _settings.sourceSelected)];

            if (selectedSourceType.Equals("Off"))
                return -1;
            else if (selectedSourceType.Equals("BGM"))
                return _settings.BGvolume;
            else if (selectedSourceType.Equals("TV"))

                if (_sonosController != null)
                    return _settings.SonosVolume;
                else if (_settings.hasBGMusic)
                    return -1;
                else
                    return -1;

            else return -1;
        }
        public int GetZoneVol(int zoneNum) => _settings.IndividualBGVolume[zoneNum];
        public bool GetMuteState()
        {
            string selectedSourceType = "Off";
            if (Array.IndexOf(_settings.sources, _settings.sourceSelected) > -1)
                selectedSourceType = _settings.sourceType[Array.IndexOf(_settings.sources, _settings.sourceSelected)];

            if (selectedSourceType.Equals("Off"))
                return true;
            else if (selectedSourceType.Equals("BGM"))
                return _settings.BGMMuteState;
            else if (selectedSourceType.Equals("TV"))

                if (_sonosController != null)
                    return _settings.SonosMuteState;
                else if (_settings.hasBGMusic)
                    return _settings.BGMMuteState;
                else
                    return true;

            else return true;
        }
        public bool GetZoneMuteState(int zoneNum) => _settings.IndividualMuteState[zoneNum];

        public float GetActualTemp() => _actualTemp;
        public float GetDesiredTemp() => _settings.desiredTemp;

        public bool GetFireplaceState() => true;

        public bool SonosExists()
        {
            if (_sonosController != null)
                return true;
            else
                return false;
        }
        public void SetNewVolumeLevel(int newVol)
        {
            string selectedSourceType = "Off";
            if (Array.IndexOf(_settings.sources, _settings.sourceSelected) > -1)
                selectedSourceType = _settings.sourceType[Array.IndexOf(_settings.sources, _settings.sourceSelected)];

            if (selectedSourceType.Equals("Off"))
                return;

            if (selectedSourceType.Equals("BGM"))
                _bgmController.ChangeVolume(_settings.roomID, newVol);
            else if (selectedSourceType.Equals("TV"))
                _sonosController.SetNewVolumeLevel(newVol);
        }
        public void SetIndividualVolumeLevel(int zoneNum, int newVol)
        {
            string selectedSourceType = "Off";
            if (Array.IndexOf(_settings.sources, _settings.sourceSelected) > -1)
                selectedSourceType = _settings.sourceType[Array.IndexOf(_settings.sources, _settings.sourceSelected)];

            if (selectedSourceType.Equals("Off"))
                return;
            if (selectedSourceType.Equals("BGM"))
                _bgmController.ChangeIndividualVolume(_settings.roomID, zoneNum,  newVol);
        }
        public void SetNewSource(string newSource)
        {
            _settings.sourceSelected = newSource;
            FileOperations.UpdateSettings(_settings.roomID.ToString(), _settings);

            string selectedSourceType = "Off";
            int selectedSourceIndex = Array.IndexOf(_settings.sources, _settings.sourceSelected);
            if (selectedSourceIndex > -1)
            {
                selectedSourceType = _settings.sourceType[selectedSourceIndex];
                OnSourceSelected(Array.IndexOf(_settings.sources, _settings.sourceSelected));
            }

            for (int i = 0; i < _settings.TVNames.Length; i++)
                _tv[i].SourceSelectedChanged(newSource);

            if (!_settings.hasBGMusic && _sonosController == null)
                return;

            if (selectedSourceType == "TV")
            {
                if (!_settings.BGMMuteState)
                    _bgmController.ToggleMute(_settings.roomID);
            }
            else if (selectedSourceType == "BGM")
            {
                _bgmController.ChangeSource(_settings.roomID, newSource);

                if (_settings.BGMMuteState)
                    _bgmController.ToggleMute(_settings.roomID);

                if (_sonosController != null)
                    _sonosController.Pause();
            }
            else if (selectedSourceType == "Off")
            {
                if (!_settings.BGMMuteState)
                    _bgmController.ToggleMute(_settings.roomID);
                    
                if (_sonosController != null)
                    _sonosController.Pause();
            }
        }
        public void SetSonosController(SonosController sonos)
        {
            try
            {
                _sonosController = sonos;
                _sonosController.volumeChanged += _sonosController_volumeChanged;
                _sonosController.muteStateChanged += _sonosController_muteStateChanged;
            }
            catch (Exception ex)
            {
                ConsoleLogger.WriteLine("Problem in Room SetSonosController: " + ex);
            }
        }
        public void SetFirePlaceState(bool newState) => _cs.SetFirePlaceState(newState);
        public void SetIndividualTVSource(string tvName, string newSource)
        {
            ConsoleLogger.WriteLine(tvName + " is here, in array at position: " + _tv[Array.IndexOf(_settings.TVNames, tvName)]);
            _tv[Array.IndexOf(_settings.TVNames, tvName)].SourceSelectedChanged(newSource);
        }
        public void SourceBtnPressed(int btnNum, string source)
        {
            ConsoleLogger.WriteLine("SourceBtnPressed.btnNum: " + btnNum);
            if (source.Equals("Sky")) _skybox.PushButton(btnNum);
            else if (source.Equals("Freeview"))
                for (int i = 0; i < _settings.TVNames.Length; i++)
                    _tv[i].FreeviewBtnPress(btnNum);
        }
        public void VolUp()
        {
            for (int i = 0; i < _settings.TVNames.Length; i++)
            {
                _tv[i].VolUp();
            }
        }
        public void VolDown()
        {
            for (int i = 0; i < _settings.TVNames.Length; i++)
                _tv[i].VolDown();
        }
        public void Mute()
        {
            string selectedSourceType = "Off";
            if (Array.IndexOf(_settings.sources, _settings.sourceSelected) > -1)
                selectedSourceType = _settings.sourceType[Array.IndexOf(_settings.sources, _settings.sourceSelected)];

            if (selectedSourceType.Equals("BGM"))
                _bgmController.ToggleMute(_settings.roomID);
            else if (selectedSourceType.Equals("TV"))
            {
                if (_sonosController != null)
                    _sonosController.ToggleMuteState();
                else
                {
                    for (int i = 0; i < _settings.TVNames.Length; i++)
                        _tv[i].ToggleMute();
                }
            }
            else
                return;
        }
        public void SetIndividualMute(int zoneNum)
        {
            string selectedSourceType = "Off";
            if (Array.IndexOf(_settings.sources, _settings.sourceSelected) > -1)
                selectedSourceType = _settings.sourceType[Array.IndexOf(_settings.sources, _settings.sourceSelected)];

            if (selectedSourceType.Equals("BGM"))
                _bgmController.ToggleIndividualMute(_settings.roomID, zoneNum);
            if (selectedSourceType.Equals("Off"))
                return;
        }
        public void TVOff()
        {
            for (int i = 0; i < _settings.TVNames.Length; i++)
                _tv[i].PowerOff();
        }

        public void SetLightScene(int scene) => _lightsController.SetScene(_settings.roomID, scene);

        public void TempUp() => _hvacController.TempUp(ProcessorInfo.ID, _settings.roomID);
        public void TempDown() => _hvacController.TempDown(ProcessorInfo.ID, _settings.roomID);

        public void OnLightSceneChanged()
        {
            if(this.LightSceneChanged != null)
            {
                this.LightSceneChanged(_settings.lightSceneSelected);
            }
        }

        public void OnActualTempChanged()
        {
            if(ActualTempChanged != null)
                ActualTempChanged(_actualTemp);
        }
        public void OnDesiredTempChanged()
        {
            if (DesiredTempChanged != null)
                DesiredTempChanged(_settings.desiredTemp);
        }

        public void OnSourceSelected(int newSourceIndex)
        {
            _settings.sourceSelected = _settings.sources[newSourceIndex];
            FileOperations.UpdateSettings(_settings.roomID.ToString(), _settings);
            ConsoleLogger.WriteLine("new Source in " + _settings.roomName + " is " + _settings.sources[newSourceIndex]);

            if (SourceSelectedChanged != null)
            {
                SourceSelectedChanged(_settings.sources[newSourceIndex]);
            }
        }
        public void OnMuteStateChanged(string sourceType, bool muteState)
        {
            if (_settings.sourceSelected.Equals("Off"))
                return;

            string selectedSourceType = _settings.sourceType[Array.IndexOf(_settings.sources, _settings.sourceSelected)];

            if (sourceType == selectedSourceType)
                if (RoomMuteStateChanged != null)
                    RoomMuteStateChanged(muteState);
        }
        public void OnVolumeChanged(string sourceType, int newVol)
        {
            try
            {
                string selectedSourceType = "";
                if (Array.IndexOf(_settings.sources, _settings.sourceSelected) > -1)
                selectedSourceType = _settings.sourceType[Array.IndexOf(_settings.sources, _settings.sourceSelected)];

                if (sourceType == selectedSourceType)
                    if (RoomVolChanged != null)
                        RoomVolChanged(newVol);
            }
            catch(Exception ex)
            {
                ConsoleLogger.WriteLine("Problem in Room.OnVolumeChanged: " + ex);
            }
        }
        public void OnFireplaceStateChanged(bool _fireplaceState)
        {
            if (FireplaceStateChanged != null)
                FireplaceStateChanged(_fireplaceState);
        }

        private void _sonosController_volumeChanged(int newVol)
        {
            _sonosVol = newVol;
            OnVolumeChanged("TV", _sonosVol);

            _settings.SonosVolume = newVol;
            FileOperations.UpdateSettings(_settings.roomID.ToString(), _settings);
        }
        private void _sonosController_muteStateChanged(bool muteState)
        {
            _settings.SonosMuteState = muteState;
            OnMuteStateChanged("TV", _settings.SonosMuteState);
            FileOperations.UpdateSettings(_settings.roomID.ToString(), _settings);
        }

        private void _bgmController_muteChanged(short roomID, bool muteState)
        {
            if (_settings.roomID == roomID)
            {
                _settings.BGMMuteState = muteState;
                FileOperations.UpdateSettings(_settings.roomID.ToString(), _settings);
                OnMuteStateChanged("BGM", _settings.BGMMuteState);
            }
        }
        private void _bgmController_individualMuteChanged(short roomID, int zoneNum, bool newState)
        {
            if (_settings.roomID == roomID)
            {
                _settings.IndividualMuteState[zoneNum] = newState;
                FileOperations.UpdateSettings(_settings.roomID.ToString(), _settings);
                if(IndividalMuteStateChanged != null && _settings.sourceSelected != "Off")
                    IndividalMuteStateChanged(zoneNum, newState);
            }
        }
        private void _bgmController_volChanged(short roomID, int newVol)
        {
            if (_settings.roomID == roomID)
            {
                _settings.BGvolume = newVol;
                FileOperations.UpdateSettings(_settings.roomID.ToString(), _settings);
                OnVolumeChanged("BGM", _settings.BGvolume);
            }
        }
        private void _bgmController_individualVolChanged(short roomID, int zoneNum, int newVol)
        {
            if (_settings.roomID == roomID)
            {
                _settings.IndividualBGVolume[zoneNum] = newVol;
                FileOperations.UpdateSettings(_settings.roomID.ToString(), _settings);
                if (RoomZoneVolChanged != null && _settings.sourceSelected != "Off")
                    RoomZoneVolChanged(zoneNum, _settings.IndividualBGVolume[zoneNum]);
            }
        }
        private void _bgmController_sourcesChanged(short roomID, int newSource)
        {
            if (_settings.roomID == roomID)
                OnSourceSelected(newSource - 1);
        }

        private void _lightsController_newSceneSelected(int procID, int roomID, int newScene)
        {
            if (ProcessorInfo.ID == procID)
            {
                if (_settings.roomID == (short)roomID)
                {
                    _settings.lightSceneSelected = newScene;
                    FileOperations.UpdateSettings(_settings.roomID.ToString(), _settings);
                    OnLightSceneChanged();
                }
            }
        }

        private void _hvacController_desiredTempChanged(short roomID, float newSetPoint)
        {
            if (roomID == _settings.roomID)
            {
                _settings.desiredTemp = newSetPoint;
                FileOperations.UpdateSettings(_settings.roomID.ToString(), _settings);
                OnDesiredTempChanged();
            }
        }
        private void _hvacController_actualTempChanged(short roomID, float newActualTemp)
        {
            if (roomID == _settings.roomID)
            {
                _actualTemp = newActualTemp;
                OnActualTempChanged();
            }
        }

        public void ConnectRoomEquipment(int tpID)
        {
            for (int i = 0; i < _settings.TVNames.Length; i++)
                _tv[i].Connect();
        }
        public void DisconnectRoomEquipment(int tpID)
        {
            for (int i = 0; i < _settings.TVNames.Length; i++)
                _tv[i].Disconnect();
        }
    }
}
