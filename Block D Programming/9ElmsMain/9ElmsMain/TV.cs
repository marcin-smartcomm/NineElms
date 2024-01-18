using Crestron.SimplSharpPro.DM.Streaming;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _9ElmsMain
{
    public class TV
    {
        int _id;
        int _room;
        bool _muted;
        ControlSystem _comms;
     
        public TV(ControlSystem comms, int tvId, int roomID)
        {
            _comms = comms;
            _id = tvId;
            _room = roomID;
        }

        public void SourceSelectedChanged(string source)
        {
            ConsoleLogger.WriteLine("New source for TV" + _id + " = " + source);

            if (source.Equals("Sky"))
            {
                PowerOn();
                Task.Run(() =>
                {
                    HDMISelect(Delay(1, 1000));
                    HDMISelect(Delay(1, 5000));
                });
            }
            else if (source.Equals("Freeview"))
            {
                PowerOn();
                Task.Run(() =>
                {
                    SelectFreeview(Delay(1, 1000));
                    SelectFreeview(Delay(1, 5000));
                });
            }
            else if (source.Equals("Laptop"))
            {
                PowerOn();
                Task.Run(() =>
                {
                    HDMISelect(Delay(2, 1000));
                    HDMISelect(Delay(2, 5000));
                });
            }
            else
                PowerOff();
        }
        public void PowerOn()
        {
            _comms.SendMessage("Room" + _room + "TV" + _id + "PON");
        }
        public void PowerOff()
        {
            _comms.SendMessage("Room" + _room + "TV" + _id + "POFF");
        }
        public void SelectFreeview(int i)
        {
            _comms.SendMessage("Room" + _room + "TV" + _id + "Freeview");
        }
        public void HDMISelect(int hdmiInput)
        {
            switch (hdmiInput)
            {
                case 1:
                    _comms.SendMessage("Room" + _room + "TV" + _id + "HDMI1");
                    break;
                
                case 2:
                    _comms.SendMessage("Room" + _room + "TV" + _id + "HDMI2");
                    break;
            }
        }
        public void VolUp()
        {
            _comms.SendMessage("Room" + _room + "TV" + _id + "Vol+");
        }
        public void VolDown()
        {
            _comms.SendMessage("Room" + _room + "TV" + _id + "Vol-");
        }
        public void ToggleMute()
        {
            if (!_muted)
            {
                _comms.SendMessage("Room" + _room + "TV" + _id + "Mute");
                _muted = true;
            }
            else
            {
                    _comms.SendMessage("Room" + _room + "TV" + _id + "Unmute");
                    _muted = false;
            }
        }
        public void FreeviewBtnPress(int btnNum)
        {
            string key = string.Empty;
            byte[] message = new byte[6];
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
            _comms.SendMessage("Room" + _room + "TV" + _id + "KP:" + key);
        }

        public void Connect() => _comms.SendMessage("Room" + _room + "TV" + _id + "Connect");
        public void Disconnect() => _comms.SendMessage("Room" + _room + "TV" + _id + "Disconnect");

        static int Delay(int toReturnAfterDelay, int milisecondDelay)
        {
            Thread.Sleep(milisecondDelay);

            return toReturnAfterDelay;
        }
    }
}
