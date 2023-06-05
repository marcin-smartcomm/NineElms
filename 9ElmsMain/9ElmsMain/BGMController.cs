using Crestron.SimplSharpPro.AudioDistribution;
using System;
using System.Text;

namespace _9ElmsMain
{
    public class BGMController
    {
        ControlSystem _comms;

        public event Action<short, int> volChanged;
        public event Action<short, int, int> individualVolChanged;
        public event Action<short, bool> muteChanged;
        public event Action<short, int, bool> individualMuteChanged;
        public event Action<short, int> sourcesChanged;

        public BGMController(string ip, int port)
        {

        }

        public BGMController(ControlSystem comms)
        {
            _comms = comms;
        }

        public void ChangeVolume(short roomID, int volLevel)
        {
            if (volLevel > -1 && volLevel < 101)
                if (roomID < 10)
                    _comms.SendMessage("BGM:Room0" + roomID + ":Volume:" + volLevel);
                else
                    _comms.SendMessage("BGM:Room" + roomID + ":Volume:" + volLevel);
        }
        public void ChangeIndividualVolume(short roomID, int zoneNum, int volLevel)
        {
            ConsoleLogger.WriteLine("Here1");
            if (volLevel > -1 && volLevel < 101)
                if (roomID < 10)
                    _comms.SendMessage("BGM:Room0" + roomID + ":Zone" + zoneNum + "Vol:" + volLevel);
                else
                    _comms.SendMessage("BGM:Room" + roomID + ":Zone" + zoneNum + "Vol:" + volLevel);
        }
        public void ChangeSource(short roomID, string newSource)
        {
            switch (newSource)
            {
                case "Music Server 1":
                    newSource = "1";
                    break;
                case "Music Server 2":
                    newSource = "2";
                    break;
                case "Music Server 3":
                    newSource = "3";
                    break;
                default:
                    break;
            }
            _comms.SendMessage("BGM:Room" + roomID + ":Source:" + newSource);
        }
        public void ToggleIndividualMute(int roomID, int zoneNum)
        {
            _comms.SendMessage("BGM:Room" + roomID + ":Zone" + zoneNum + "MuteToggle");
        }
        public void ToggleMute(short roomID)
        {
            _comms.SendMessage("BGM:Room" + roomID + ":MuteToggle");
        }

        public void EvaluateString(string message)
        {
            string[] newInfo = message.Split(':');

            short roomID = short.Parse(newInfo[1].Remove(0, 4));

            if (newInfo[2].Equals("Volume"))
            {
                int newVol = int.Parse(newInfo[3]);
                OnVolumeChanged(roomID, newVol);
            }
            if (newInfo[2].Equals("Zone1Volume"))
            {
                int newVol = int.Parse(newInfo[3]);
                OnVolumeChanged(roomID, newVol);
                OnIndividualVolumeChanged(roomID, 0, newVol);
            }
            if (newInfo[2].Equals("Zone2Volume"))
            {
                int newVol = int.Parse(newInfo[3]);
                OnIndividualVolumeChanged(roomID, 1, newVol);
            }
            if (newInfo[2].Equals("Zone3Volume"))
            {
                int newVol = int.Parse(newInfo[3]);
                OnIndividualVolumeChanged(roomID, 2, newVol);
            }
            if (newInfo[2].Equals("Zone4Volume"))
            {
                int newVol = int.Parse(newInfo[3]);
                OnIndividualVolumeChanged(roomID, 3, newVol);
            }
            if (newInfo[2].Contains("Source"))
            {
                int newSource = 0;

                if(newInfo[3].Equals("Music Stream 1"))
                    newSource = 1;
                if (newInfo[3].Equals("Music Stream 2"))
                    newSource = 2;
                if (newInfo[3].Equals("Music Stream 3"))
                    newSource = 3;

                OnSourceChanged(roomID, newSource);
            }

            if (newInfo[2].Equals("Muted"))
                OnMuteChanged(roomID, true);
            if (newInfo[2].Equals("UnMuted"))
                OnMuteChanged(roomID, false);

            if (newInfo[2].Equals("Zone1Muted"))
            {
                OnMuteChanged(roomID, true);
                OnIndividualMuteChanged(roomID, 0, true);
            }
            if (newInfo[2].Equals("Zone1UnMuted"))
            {
                OnMuteChanged(roomID, false);
                OnIndividualMuteChanged(roomID, 0, false);
            }
            if (newInfo[2].Equals("Zone2Muted"))
                OnIndividualMuteChanged(roomID, 1, true);
            if (newInfo[2].Equals("Zone2UnMuted"))
                OnIndividualMuteChanged(roomID, 1, false);

            if (newInfo[2].Equals("Zone3Muted"))
                OnIndividualMuteChanged(roomID, 2, true);
            if (newInfo[2].Equals("Zone3UnMuted"))
                OnIndividualMuteChanged(roomID, 2, false);

            if (newInfo[2].Equals("Zone4Muted"))
                OnIndividualMuteChanged(roomID, 3, true);
            if (newInfo[2].Equals("Zone4UnMuted"))
                OnIndividualMuteChanged(roomID, 3, false);
        }

        public void OnVolumeChanged(short roomID, int newVol)
        {
            try
            {
                if (volChanged != null)
                    volChanged(roomID, newVol);
            } catch (Exception ex)
            {
                ConsoleLogger.WriteLine("Problem in BGMController OnVolumeChanged: " + ex);
            }
        }
        public void OnIndividualVolumeChanged(short roomID, int zoneNum, int newVol)
        {
            try
            {
                if (individualVolChanged != null)
                    individualVolChanged(roomID, zoneNum, newVol);
            }
            catch (Exception ex)
            {
                ConsoleLogger.WriteLine("Problem in BGMController OnVolumeChanged: " + ex);
            }
        }
        public void OnSourceChanged(short roomID, int newSource)
        {
            if (sourcesChanged != null)
                sourcesChanged(roomID, newSource);
        }
        public void OnMuteChanged(short roomID, bool muteState)
        {
            if (muteChanged != null)
                muteChanged(roomID, muteState);
        }
        public void OnIndividualMuteChanged(short roomID, int zoneNum, bool newState)
        {
            if (individualMuteChanged != null)
                individualMuteChanged(roomID, zoneNum, newState);
        }

        #region DirectComms
        public void Connect() { }
        public void Disconnect() { }
        public void GetConnectionStatus() {}
        private void Comms_ConnectedEvent(bool obj)
        {
            ConsoleLogger.WriteLine("Connected to Audio Processor");
        }
        private void Comms_MessageReceived(object source, MessageReceivedEventArgs args)
        {
            string fromAudioServer = Encoding.ASCII.GetString(args.message);
            ConsoleLogger.WriteLine("Received from Audio Server: " + fromAudioServer);
        }
        #endregion
    }
}
