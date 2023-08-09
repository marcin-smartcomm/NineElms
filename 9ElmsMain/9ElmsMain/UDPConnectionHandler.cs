using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace _9ElmsMain
{
    public class UDPConnectionHandler
    {
        UdpClient _udpClient;
        int _receivePort, _sendPort;
        string _ipAddress, _connectionName;

        public event Action<string> newMsgReceived;

        public UDPConnectionHandler(string ipAddress, int receivePort, int sendPort, string connectionName)
        {
            _receivePort = receivePort;
            _sendPort = sendPort;
            _connectionName = connectionName;
            _ipAddress = ipAddress;
            _udpClient = new UdpClient(_receivePort);
        }

        public UDPConnectionHandler(string ipAddress, int port, string connectionName)
        {
            _receivePort = port;
            _sendPort = port;
            _connectionName = connectionName;
            _ipAddress = ipAddress;
            _udpClient = new UdpClient(_receivePort);
        }

        public void StartListener()
        {
            _udpClient.BeginReceive(new AsyncCallback(recv), null);
            ConsoleLogger.WriteLine("UDP Listener for " + _connectionName + " strted on port " + _receivePort);
        }

        public void SendData(string message)
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            IPAddress broadcast = IPAddress.Parse(_ipAddress);

            byte[] sendbuf = Encoding.ASCII.GetBytes(message);
            IPEndPoint ep = new IPEndPoint(broadcast, _sendPort);

            s.SendTo(sendbuf, ep);

            ConsoleLogger.WriteLine("To " + _connectionName + ": " + message);
        }

        void recv(IAsyncResult result)
        {
            ConsoleLogger.WriteLine("Receiving...");
            IPEndPoint RemoteIP = new IPEndPoint(IPAddress.Any, _receivePort);
            byte[] received = _udpClient.EndReceive(result, ref RemoteIP);
            string receivedString = Encoding.UTF8.GetString(received);

            ConsoleLogger.WriteLine("From " + _connectionName + ": " + receivedString);

            if(newMsgReceived!= null)
                newMsgReceived(receivedString);

            _udpClient.BeginReceive(new AsyncCallback(recv), null);
        }
    }
}
