using Crestron.SimplSharpPro.DM.Streaming;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _9ElmsMain
{
    public class TV
    {
        bool _muted;
        string _currentSource;
        AsyncTCPClient _comms;
        DmNvx360 _tvReceiver;

        public TV(string ip, int port, DmNvx360 tvReceiver)
        {
            _comms = new AsyncTCPClient(ip, port, 4000);
            _comms.MessageReceived += Comms_MessageReceived;
            _comms.ConnectedEvent += Comms_ConnectedEvent;
            _tvReceiver = tvReceiver;
        }
        public TV(string ip, int port)
        {
            _comms = new AsyncTCPClient(ip, port, 4000);
            _comms.MessageReceived += Comms_MessageReceived;
            _comms.ConnectedEvent += Comms_ConnectedEvent;
        }

        public void Connect(int tpID)
        {
            _comms.ConnectRequest(tpID);
        }
        public void Disconnect(int tpID)
        {
            _comms.Disconnect(tpID);
        }
        public bool GetConnectionStatus() => _comms.GetConnectionStatus();

        private void Comms_ConnectedEvent(bool obj)
        {

        }
        private void Comms_MessageReceived(object source, MessageReceivedEventArgs args)
        {
            string message = Encoding.ASCII.GetString(args.message);
            ConsoleLogger.WriteLine("Received bytes from TV: " + args.message);
            ConsoleLogger.WriteLine("Received string from TV: " + message);
        }

        public void SourceSelectedChanged(string source)
        {
            if (source.Equals("Off"))
                PowerOff();
            else
                PowerOn();

            if (source.Equals("Sky"))
            {
                _currentSource = source;
                HDMISelect(1);
                Task.Run(() =>
                {
                    HDMISelect(Delay(1, 3000));
                });
            }
            else if (source.Equals("Freeview"))
            {
                _currentSource = source;
                SelectFreeview(1);
                Task.Run(() =>
                {
                    SelectFreeview(Delay(1, 3000));
                });
            }
            else if (source.Equals("Laptop"))
            {
                _currentSource = source;
                HDMISelect(1);
                Task.Run(() =>
                {
                    HDMISelect(Delay(1, 3000));
                });
            }
        }
        public void PowerOn()
        {
            if (_tvReceiver != null)
                _tvReceiver.HdmiOut.StreamCec.Send.StringValue = "\x40\x44\x6D";
            else
            {
                byte[] message = new byte[6];
                message[0] = 0xAA; message[1] = 0x11; message[2] = 0xFE; message[3] = 0x01; message[4] = 0x01; message[5] = 0x11;
                _comms.SendByteMessage(message);
            }
        }
        public void PowerOff()
        {
            if (_tvReceiver != null)
                _tvReceiver.HdmiOut.StreamCec.Send.StringValue = "\x40\x44\x6C";
            else
            {
                byte[] message = new byte[6];
                message[0] = 0xAA; message[1] = 0x11; message[2] = 0xFE; message[3] = 0x01; message[4] = 0x00; message[5] = 0x10;
                _comms.SendByteMessage(message);
            }
        }
        public void SelectFreeview(int i)
        {
            if (_tvReceiver != null)
                _tvReceiver.HdmiOut.StreamCec.Send.StringValue = "\x3F\x82\x00\x00";
            else
            {
                byte[] message = new byte[6];
                message[0] = 0xAA; message[1] = 0x14; message[2] = 0x01; message[3] = 0x01; message[4] = 0x18; message[5] = 0x2E;
                _comms.SendByteMessage(message);
            }
        }
        public void HDMISelect(int hdmiInput)
        {
            switch (hdmiInput)
            {
                case 1:
                    if (_tvReceiver != null)
                        _tvReceiver.HdmiOut.StreamCec.Send.StringValue = "\x1F\x82\x00\x00";
                    else
                    {
                        byte[] message = new byte[6];
                        message[0] = 0xAA; message[1] = 0x14; message[2] = 0xFE; message[3] = 0x01; message[4] = 0x21; message[5] = 0x34;
                        _comms.SendByteMessage(message);
                    }
                    break;
                
                case 2:
                    if (_tvReceiver != null)
                        _tvReceiver.HdmiOut.StreamCec.Send.StringValue = "\x2F\x82\x00\x00";
                    else
                    {
                        byte[] message = new byte[6];
                        message[0] = 0xAA; message[1] = 0x14; message[2] = 0xFE; message[3] = 0x01; message[4] = 0x23; message[5] = 0x36;
                        _comms.SendByteMessage(message);
                    }
                    _comms.SendMessage("\xAA\x14\xFE\x01\x23\x36");
                    break;
            }
        }
        public void VolUp()
        {
            if (_tvReceiver != null)
                _tvReceiver.HdmiOut.StreamCec.Send.StringValue = "\x40\x44\x41";
            else
            {
                byte[] message = new byte[6];
                message[0] = 0xAA; message[1] = 0x62; message[2] = 0xFE; message[3] = 0x01; message[4] = 0x00; message[5] = 0x61;
                _comms.SendByteMessage(message);
            }
        }
        public void VolDown()
        {
            if (_tvReceiver != null)
                _tvReceiver.HdmiOut.StreamCec.Send.StringValue = "\x40\x44\x42";
            else
            {
                byte[] message = new byte[6];
                message[0] = 0xAA; message[1] = 0x62; message[2] = 0xFE; message[3] = 0x01; message[4] = 0x01; message[5] = 0x62;
                _comms.SendByteMessage(message);
            }
        }
        public void ToggleMute()
        {
            if (!_muted)
            {
                if (_tvReceiver != null)
                { 
                    _tvReceiver.HdmiOut.StreamCec.Send.StringValue = "\x40\x44\x43";
                    _muted = true; 
                }
                else
                {
                    byte[] message = new byte[6];
                    message[0] = 0xAA; message[1] = 0x13; message[2] = 0xFE; message[3] = 0x01; message[4] = 0x01; message[5] = 0x13;
                    _comms.SendByteMessage(message);
                    _muted = true;
                }
            }
            else
            {
                if (_tvReceiver != null)
                {
                    _tvReceiver.HdmiOut.StreamCec.Send.StringValue = "\x40\x44\x65";
                    _muted = false;
                }
                else
                {
                    byte[] message = new byte[6];
                    message[0] = 0xAA; message[1] = 0x13; message[2] = 0xFE; message[3] = 0x01; message[4] = 0x00; message[5] = 0x12;
                    _comms.SendByteMessage(message);
                    _muted = false;
                }
            }
        }
        public void FreeviewBtnPress(int btnNum)
        {
            if (_currentSource == null)
                return;

            if(_currentSource.Equals("Freeview"))
            {
                byte[] message = new byte[6];
                switch (btnNum)
                {
                    //SOURCE
                    case 0: message[0] = 0xAA; message[1] = 0xB0; message[2] = 0xFE; message[3] = 0x01; message[4] = 0x01; message[5] = 0xB0; break;
                    //POWER
                    case 1: message[0] = 0xAA; message[1] = 0xB0; message[2] = 0xFE; message[3] = 0x01; message[4] = 0x02; message[5] = 0xB1; break;
                    //1
                    case 2: message[0] = 0xAA; message[1] = 0xB0; message[2] = 0xFE; message[3] = 0x01; message[4] = 0x04; message[5] = 0xB3; break;
                    //2
                    case 3: message[0] = 0xAA; message[1] = 0xB0; message[2] = 0xFE; message[3] = 0x01; message[4] = 0x05; message[5] = 0xB4; break;
                    //3
                    case 4: message[0] = 0xAA; message[1] = 0xB0; message[2] = 0xFE; message[3] = 0x01; message[4] = 0x06; message[5] = 0xB5; break;
                    //VOL_UP
                    case 5: message[0] = 0xAA; message[1] = 0xB0; message[2] = 0xFE; message[3] = 0x01; message[4] = 0x07; message[5] = 0xB6; break;
                    //4
                    case 6: message[0] = 0xAA; message[1] = 0xB0; message[2] = 0xFE; message[3] = 0x01; message[4] = 0x08; message[5] = 0xB7; break;
                    //5
                    case 7: message[0] = 0xAA; message[1] = 0xB0; message[2] = 0xFE; message[3] = 0x01; message[4] = 0x09; message[5] = 0xB8; break;
                    //6
                    case 8: message[0] = 0xAA; message[1] = 0xB0; message[2] = 0xFE; message[3] = 0x01; message[4] = 0x0A; message[5] = 0xB9; break;
                    //VOL_DOWN
                    case 9: message[0] = 0xAA; message[1] = 0xB0; message[2] = 0xFE; message[3] = 0x01; message[4] = 0x0B; message[5] = 0xBA; break;
                    //7
                    case 10: message[0] = 0xAA; message[1] = 0xB0; message[2] = 0xFE; message[3] = 0x01; message[4] = 0x0C; message[5] = 0xBB; break;
                    //8
                    case 11: message[0] = 0xAA; message[1] = 0xB0; message[2] = 0xFE; message[3] = 0x01; message[4] = 0x0D; message[5] = 0xBC; break;
                    //9
                    case 12: message[0] = 0xAA; message[1] = 0xB0; message[2] = 0xFE; message[3] = 0x01; message[4] = 0x0E; message[5] = 0xBD; break;
                    //MUTE
                    case 13: message[0] = 0xAA; message[1] = 0xB0; message[2] = 0xFE; message[3] = 0x01; message[4] = 0x0F; message[5] = 0xBE; break;
                    //CH_DOWN
                    case 14: message[0] = 0xAA; message[1] = 0xB0; message[2] = 0xFE; message[3] = 0x01; message[4] = 0x10; message[5] = 0xBF; break;
                    //0
                    case 15: message[0] = 0xAA; message[1] = 0xB0; message[2] = 0xFE; message[3] = 0x01; message[4] = 0x11; message[5] = 0xC0; break;
                    //CH_UP
                    case 16: message[0] = 0xAA; message[1] = 0xB0; message[2] = 0xFE; message[3] = 0x01; message[4] = 0x12; message[5] = 0xC1; break;
                    //GREEN
                    case 17: message[0] = 0xAA; message[1] = 0xB0; message[2] = 0xFE; message[3] = 0x01; message[4] = 0x14; message[5] = 0xC3; break;
                    //YELLOW
                    case 18: message[0] = 0xAA; message[1] = 0xB0; message[2] = 0xFE; message[3] = 0x01; message[4] = 0x15; message[5] = 0xC4; break;
                    //CYAN(BLUE)
                    case 19: message[0] = 0xAA; message[1] = 0xB0; message[2] = 0xFE; message[3] = 0x01; message[4] = 0x16; message[5] = 0xC5; break;
                    //MENU
                    case 20: message[0] = 0xAA; message[1] = 0xB0; message[2] = 0xFE; message[3] = 0x01; message[4] = 0x1A; message[5] = 0xC9; break;
                    //DISPLAY
                    case 21: message[0] = 0xAA; message[1] = 0xB0; message[2] = 0xFE; message[3] = 0x01; message[4] = 0x1F; message[5] = 0xCE; break;
                    //DIGIT
                    case 22: message[0] = 0xAA; message[1] = 0xB0; message[2] = 0xFE; message[3] = 0x01; message[4] = 0x23; message[5] = 0xD2; break;
                    //PIP_TV_VIDEO
                    case 23: message[0] = 0xAA; message[1] = 0xB0; message[2] = 0xFE; message[3] = 0x01; message[4] = 0x24; message[5] = 0xD3; break;
                    //EXIT
                    case 24: message[0] = 0xAA; message[1] = 0xB0; message[2] = 0xFE; message[3] = 0x01; message[4] = 0x2D; message[5] = 0xDC; break;
                    //REWND
                    case 25: message[0] = 0xAA; message[1] = 0xB0; message[2] = 0xFE; message[3] = 0x01; message[4] = 0x45; message[5] = 0xF4; break;
                    //STOP
                    case 26: message[0] = 0xAA; message[1] = 0xB0; message[2] = 0xFE; message[3] = 0x01; message[4] = 0x46; message[5] = 0xF5; break;
                    //PLAY
                    case 27: message[0] = 0xAA; message[1] = 0xB0; message[2] = 0xFE; message[3] = 0x01; message[4] = 0x47; message[5] = 0xF6; break;
                    //FAST_FORWARD
                    case 28: message[0] = 0xAA; message[1] = 0xB0; message[2] = 0xFE; message[3] = 0x01; message[4] = 0x48; message[5] = 0xF7; break;
                    //PAUSE
                    case 29: message[0] = 0xAA; message[1] = 0xB0; message[2] = 0xFE; message[3] = 0x01; message[4] = 0x4A; message[5] = 0xF9; break;
                    //TOOLS
                    case 30: message[0] = 0xAA; message[1] = 0xB0; message[2] = 0xFE; message[3] = 0x01; message[4] = 0x4B; message[5] = 0xFA; break;
                    //RETURN
                    case 31: message[0] = 0xAA; message[1] = 0xB0; message[2] = 0xFE; message[3] = 0x01; message[4] = 0x58; message[5] = 0x07; break;
                    //MAGNIFICO_LITE
                    case 32: message[0] = 0xAA; message[1] = 0xB0; message[2] = 0xFE; message[3] = 0x01; message[4] = 0x5B; message[5] = 0x0A; break;
                    //UP
                    case 33: message[0] = 0xAA; message[1] = 0xB0; message[2] = 0xFE; message[3] = 0x01; message[4] = 0x60; message[5] = 0x0F; break;
                    //DOWN
                    case 34: message[0] = 0xAA; message[1] = 0xB0; message[2] = 0xFE; message[3] = 0x01; message[4] = 0x61; message[5] = 0x10; break;
                    //RIGHT
                    case 35: message[0] = 0xAA; message[1] = 0xB0; message[2] = 0xFE; message[3] = 0x01; message[4] = 0x62; message[5] = 0x11; break;
                    //LEFT
                    case 36: message[0] = 0xAA; message[1] = 0xB0; message[2] = 0xFE; message[3] = 0x01; message[4] = 0x65; message[5] = 0x14; break;
                    //ENTER
                    case 37: message[0] = 0xAA; message[1] = 0xB0; message[2] = 0xFE; message[3] = 0x01; message[4] = 0x68; message[5] = 0x17; break;
                    //RED
                    case 38: message[0] = 0xAA; message[1] = 0xB0; message[2] = 0xFE; message[3] = 0x01; message[4] = 0x6C; message[5] = 0x1B; break;
                    //LOCK
                    case 39: message[0] = 0xAA; message[1] = 0xB0; message[2] = 0xFE; message[3] = 0x01; message[4] = 0x77; message[5] = 0x26; break;
                    //CONTENT
                    case 40: message[0] = 0xAA; message[1] = 0xB0; message[2] = 0xFE; message[3] = 0x01; message[4] = 0x79; message[5] = 0x28; break;
                    //DISCRETE_POWER_OFF
                    case 41: message[0] = 0xAA; message[1] = 0xB0; message[2] = 0xFE; message[3] = 0x01; message[4] = 0x98; message[5] = 0x47; break;
                    //3D
                    case 42: message[0] = 0xAA; message[1] = 0xB0; message[2] = 0xFE; message[3] = 0x01; message[4] = 0x9F; message[5] = 0x4E; break;
                }
                _comms.SendByteMessage(message);
            }
        }

        static int Delay(int toReturnAfterDelay, int milisecondDelay)
        {
            Thread.Sleep(milisecondDelay);

            return toReturnAfterDelay;
        }
    }
}
