using System;
using System.Text;

namespace _9ElmsMain
{
    public class LutronProcessor
    {
        int _connectionRequests = 0;
        AsyncTCPClient _comms;

        public event Action<int, int, int> newSceneSelected;

        public LutronProcessor(string ip, int port)
        {
            _comms = new AsyncTCPClient(ip, port, 4000);
            _comms.ConnectedEvent += _comms_ConnectedEvent;
            _comms.MessageReceived += _comms_MessageReceived;
            _comms.Connect();
        }

        public LutronProcessor(AsyncTCPClient comms)
        {
            _comms = comms;
        }

        public void SetScene(int roomNum, int sceneNum)
        {
            _comms.SendMessage("Lutron:Proc" + ProcessorInfo.ID + ":Room" + roomNum + ":Scene" + sceneNum);
        }

        public void GetSceneSelected(int roomNum)
        {
            for(int i = 1; i < 6; i++)
            {
                _comms.SendMessage("Lutron:Proc" + ProcessorInfo.ID + ":Room" + roomNum + ":GetLED" + i + "State");
            }
        }

        public void evaluateMessage(string message)
        {
            try
            {
                string roomIDString = message.Split(':')[2];
                string newSceneString = message.Split(':')[3];

                int roomID = int.Parse(roomIDString.Remove(0, 4));
                int newScene = int.Parse(newSceneString.Remove(0, 5));

                OnSceneSelected(ProcessorInfo.ID, roomID, newScene);
            }
            catch(Exception ex)
            {
                ConsoleLogger.WriteLine("Problem in LutronProcessor.evaluateMessage " + ex);
            }
        }

        public void OnSceneSelected(int processorID, int roomID, int newScene)
        {
            if(this.newSceneSelected != null)
            {
                this.newSceneSelected(processorID, roomID, newScene);
            }
        }

        public void Connect()
        {
            if (ProcessorInfo.ID == 2)
            {
                if (_comms.GetConnectionStatus())
                {
                    _connectionRequests++;
                }
                else
                {
                    _comms.Connect();
                    _connectionRequests++;
                }
            }
            else
                _comms.Connect();
        }
        public void Disconnect()
        {
            _connectionRequests--;
            if (_connectionRequests <= 0)
            {
                _connectionRequests = 0;
                _comms.Disconnect(999);
            }
            else { };
        }
        public bool GetConnectionStatus() => _comms.GetConnectionStatus();

        private void _comms_ConnectedEvent(bool obj)
        {
            ConsoleLogger.WriteLine("Connected to Lutron Processor");
        }
        private void _comms_MessageReceived(object source, MessageReceivedEventArgs args)
        {
            string fromLutron = Encoding.ASCII.GetString(args.message);
            ConsoleLogger.WriteLine("Received from Lutron Processor: " + fromLutron);
            evaluateMessage(fromLutron);
        }
    }
}
