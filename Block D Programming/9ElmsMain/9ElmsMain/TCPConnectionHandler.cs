using Crestron.SimplSharp.CrestronSockets;
using Crestron.SimplSharpPro.EthernetCommunication;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace _9ElmsMain
{
    public class TCPConnectionHandler
    {
        public event Action<string> newMessageEvent;

        TCPClient _comms;
        string _ipAddress, _connectionName, _keepAliveMessage;
        int _port;
        bool _keepConnectionAlive = false;
        System.Timers.Timer _reconnectTimer;

        public TCPConnectionHandler(string ipAddress, int port, string connectionName, string keepAliveMessage)
        {
            _reconnectTimer = new System.Timers.Timer();
            _reconnectTimer.Elapsed += _reconnectTimer_Elapsed;
            _reconnectTimer.Interval = 3000;

            _comms = new TCPClient(ipAddress, port, 4096);
            _ipAddress = ipAddress;
            _port = port;
            _connectionName = connectionName;
            _keepAliveMessage = keepAliveMessage;
            Connect();
        }

        private void _reconnectTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Disconnect();
            _comms.Dispose();
            _comms = new TCPClient(_ipAddress, _port, 4096);
            Connect();
        }

        public void Connect()
        {
            if (!_keepConnectionAlive)
            {
                ConsoleLogger.WriteLine("Tryign to connect to: " + _connectionName + " (" + _comms.AddressClientConnectedTo + ")");
                _comms.ConnectToServerAsync(ClientConnectCallBackFunction);
                _comms.SocketStatusChange += _comms_SocketStatusChange;

                _keepConnectionAlive = true;
                _reconnectTimer.Start();
            }
        }
        public void Disconnect()
        {
            _keepConnectionAlive = false;
            _comms.DisconnectFromServer();
            _comms.SocketStatusChange -= _comms_SocketStatusChange;
        }
        public void SendMessage(string message)
        {
            if (_comms.ClientStatus == SocketStatus.SOCKET_STATUS_CONNECTED)
            {
                ConsoleLogger.WriteLine("To " + _connectionName + ": " + message);
                byte[] messageInBytes = Encoding.ASCII.GetBytes(message);

                _comms.SendData(messageInBytes, messageInBytes.Length);
            }
        }
        public void SendByteMessage(byte[] message)
        {
            if (_comms.ClientStatus == SocketStatus.SOCKET_STATUS_CONNECTED)
                _comms.SendData(message, message.Length);
        }
        private void ClientConnectCallBackFunction(TCPClient myTcpClient)
        {
            _comms.ReceiveDataAsync(SerialRecieveCallBack);
        }

        void KeepAlive()
        {
            Task.Run(() =>
            {
                while (_comms.ClientStatus == SocketStatus.SOCKET_STATUS_CONNECTED)
                {
                    Thread.Sleep(15000);
                    byte[] bytes = Encoding.ASCII.GetBytes(_keepAliveMessage);
                    _comms.SendData(bytes, bytes.Length);
                }
            });
        }
        private void SerialRecieveCallBack(TCPClient myTcpClient, int numberOfBytesReceived)
        {
            var stringdataReceived = Encoding.ASCII.GetString(myTcpClient.IncomingDataBuffer, 0, numberOfBytesReceived);
            ConsoleLogger.WriteLine("From " + _connectionName + ": " + stringdataReceived);
            if (newMessageEvent != null)
                newMessageEvent(stringdataReceived);
            _comms.ReceiveDataAsync(SerialRecieveCallBack);
        }

        void _comms_SocketStatusChange(TCPClient myTCPClient, SocketStatus clientSocketStatus)
        {
            ConsoleLogger.WriteLine("SocketStatus: " + clientSocketStatus);
            if (clientSocketStatus == SocketStatus.SOCKET_STATUS_CONNECTED)
            {
                _reconnectTimer.Stop();
                KeepAlive();
                ConsoleLogger.WriteLine("Connected to: " + _connectionName + " (" + _comms.AddressClientConnectedTo + ")");
            }
            if (clientSocketStatus == SocketStatus.SOCKET_STATUS_NO_CONNECT && _keepConnectionAlive)
            {
                _reconnectTimer.Start();
            }
        }
    }
}
