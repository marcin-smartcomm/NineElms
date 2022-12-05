using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace _9ElmsMain
{
    public class AsyncTCPServer
    {
        Socket _serverSocket, _clientSocket;
        byte[] _buffer;

        public delegate void MessageReceivedEventHandler(object source, MessageReceivedEventArgs args);
        public event MessageReceivedEventHandler MessageReceived;

        public delegate void ClientConnectedEventHandler(object source, EventArgs args);
        public event ClientConnectedEventHandler ClientConnected;

        public AsyncTCPServer(int listenPort)
        {
            try
            {
                _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                _serverSocket.Bind(new IPEndPoint(IPAddress.Any, listenPort));
                _serverSocket.Listen(0);
                _serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);
            }
            catch (Exception ex)
            {
                ConsoleLogger.WriteLine("problem in AsyncTCPServer Constructor\n" + ex.ToString());
            }
        }

        public bool GetClientsConnected()
        {
            if(ClientConnected == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void SendMessage(string message)
        {
            try
            {
                byte[] buffer = Encoding.ASCII.GetBytes(message);
                _clientSocket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(SendCallback), null);
            }
            catch (SocketException) { }
            catch (Exception ex)
            {
                ConsoleLogger.WriteLine("Exception in CorioMAster.SendMessage() " + ex.ToString());
            }
        }

        void SendCallback(IAsyncResult AR)
        {
            try
            {
                _clientSocket.EndSend(AR);
            }
            catch (Exception ex)
            {
                ConsoleLogger.WriteLine("Exception in CorioMAster.SendCallback() " + ex.ToString());
            }
        }

        void AcceptCallback(IAsyncResult AR)
        {
            try
            {
                _clientSocket = _serverSocket.EndAccept(AR);
                _buffer = new byte[_clientSocket.ReceiveBufferSize];
                _clientSocket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), null);
                OnClientConnected();
                _serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);
            }
            catch (Exception ex)
            {
                ConsoleLogger.WriteLine("problem in AsyncTCPServer.AcceptCallback()\n" + ex.ToString());
            }
        }

        void ReceiveCallback(IAsyncResult AR)
        {
            try
            {
                int received = _clientSocket.EndReceive(AR);

                if (received == 0)
                    return;

                Array.Resize(ref _buffer, received);
                string text = Encoding.ASCII.GetString(_buffer);

                OnMessageReceived(_buffer);

                Array.Resize(ref _buffer, _clientSocket.ReceiveBufferSize);
                _clientSocket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), null);

                SendMessage(text);
            }
            catch (Exception ex)
            {
                ConsoleLogger.WriteLine("problem in AsyncTCPServer.ReceiveCallback()\n" + ex.ToString());
            }
        }

        protected virtual void OnMessageReceived(byte[] message)
        {
            if (MessageReceived != null)
                MessageReceived(this, new MessageReceivedEventArgs() { message = message });
        }

        protected virtual void OnClientConnected()
        {
            if (ClientConnected != null)
                ClientConnected(this, new EventArgs() { });
        }
    }
}
