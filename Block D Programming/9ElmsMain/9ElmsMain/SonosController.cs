using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _9ElmsMain
{
    public class SonosController
    {
        ControlSystem _comms;
        string _name;
        int _id;

        int _currentVolume;

        public event Action<int> volumeChanged;
        public event Action<bool> muteStateChanged;

        public SonosController(ControlSystem comms, string name, int id)
        {
            this._comms = comms;
            this._name = name;
            this._id = id;
        }

        public void SetNewVolumeLevel(int newVolume)
        {
            _comms.SendMessage("Sonos"+_id+":Volume:"+newVolume);
        }
        public void ToggleMuteState()
        {
            _comms.SendMessage("Sonos"+_id+":MuteToggle");
        }
        public void Pause()
        {
            _comms.SendMessage("Sonos"+_id+":Pause");
        }

        public void EvaluateString(string message)
        {
            string[] newInfo = message.Split(':');

            ConsoleLogger.WriteLine(message + newInfo[0] + newInfo[1]);
            if (newInfo[1].Contains("Volume"))
                ChangeVol(newInfo);
            if (newInfo[1].Contains("Muted"))
                OnMuteChanged(true);
            if (newInfo[1].Contains("UnMuted"))
                OnMuteChanged(false);
        }

        void ChangeVol(string[] newInfo)
        {
            int newVol = int.Parse(newInfo[2]);
            _currentVolume = newVol;
            OnVolChanged(newVol);
        }

        void OnVolChanged(int newVol)
        {
            if(this.volumeChanged != null)
                this.volumeChanged(newVol);
        }

        void OnMuteChanged(bool newState)
        {
            if (this.muteStateChanged != null)
                this.muteStateChanged(newState);
        }
    }
}
