using System;
using System.Threading;
using System.Threading.Tasks;

namespace _9ElmsMain
{
    public class LutronProcessor
    {
        TCPConnectionHandler _tcpComms;
        ControlSystem _cs;
        string _delimeter;
        bool _initialized = false;

        public event Action<int, int, int> newSceneSelected;

        public LutronProcessor(string ip, int port, string connectionName, ControlSystem cs)
        {
            _cs = cs;
            _tcpComms = new TCPConnectionHandler(ip, port, connectionName, "?SYSTEM,1\x0D\x0A");
            _tcpComms.newMessageEvent += _tcpComms_newMessageEvent;
            _delimeter = "\x0D\x0A";
        }

        public LutronProcessor(ControlSystem comms)
        {
            _cs = comms;
        }

        private void _tcpComms_newMessageEvent(string lutronRx)
        {
            if (lutronRx.Contains("login:"))
            {
                _initialized = false;
                _tcpComms.SendMessage("kupaUser" + _delimeter);
            }
            if (lutronRx.Contains("password:"))
                _tcpComms.SendMessage("kupa123kupa456" + _delimeter);
            if (lutronRx.Contains("QNET") && !_initialized)
                InitializeComms();

            if (lutronRx.Contains("~DEVICE"))
                CheckMessage(lutronRx);
        }

        void InitializeComms()
        {
            _initialized = true;
            Task.Run(() =>
            {
                _tcpComms.SendMessage("#MONITORING,2,1" + _delimeter);
                Thread.Sleep(500);
                _tcpComms.SendMessage("#MONITORING,3,1" + _delimeter);
                Thread.Sleep(500);
                _tcpComms.SendMessage("#MONITORING,4,1" + _delimeter);
                Thread.Sleep(500);
                _tcpComms.SendMessage("#MONITORING,5,1" + _delimeter);
                Thread.Sleep(500);
                _tcpComms.SendMessage("#MONITORING,8,1" + _delimeter);
            });
        }

        void CheckMessage(string newMsg)
        {
            try
            {
                string ledNum = newMsg.Split(',')[2];
                if (ledNum.Equals("2001") || ledNum.Equals("2004"))
                {
                    uint integrationID = uint.Parse(newMsg.Split(',')[1]);
                    for (int i = 0; i < _cs.rooms.Count; i++)
                        if (_cs.rooms[i].GetLutronKeypadID() == integrationID)
                        {
                            bool sceneState = false;
                            if(newMsg.Split(',')[4].Contains("1")) { sceneState = true; }

                            if (ledNum.Equals("2001") && sceneState)
                                OnSceneSelected(ProcessorInfo.ID, i + 1, 1);
                            if (ledNum.Equals("2004") && sceneState)
                                OnSceneSelected(ProcessorInfo.ID, i + 1, 0);
                        }
                }
            }catch(Exception ex) { ConsoleLogger.WriteLine("Problem in LutronProcessor.CheckMessage: " + ex.Message); }
        }

        public void SetScene(uint integrationID, int sceneNum)
        {
            if(sceneNum == 0) sceneNum = 4;
            _tcpComms.SendMessage("#DEVICE,"+ integrationID + "," + sceneNum + ",3" + _delimeter);
        }

        public void SetDim(uint integrationID, string dimDirection, string dimAction)
        {
            string directionCode = "", actionCode = "";
            if (dimDirection.Equals("Up")) directionCode = "2";
            if (dimDirection.Equals("Down")) directionCode = "3";
            if (dimAction.Equals("On")) actionCode = "5";
            if (dimAction.Equals("Off")) actionCode = "4";

            _tcpComms.SendMessage("#DEVICE," + integrationID + "," + directionCode + "," + actionCode + _delimeter);
        }

        public void GetSceneSelected(uint integrationID)
        {
            _tcpComms.SendMessage("?DEVICE," + integrationID + ",2001,9" + _delimeter);
            _tcpComms.SendMessage("?DEVICE," + integrationID + ",2004,9" + _delimeter);
        }

        public void OnSceneSelected(int processorID, int roomID, int newScene)
        {
            if(this.newSceneSelected != null)
            {
                this.newSceneSelected(processorID, roomID, newScene);
            }
        }
    }
}
