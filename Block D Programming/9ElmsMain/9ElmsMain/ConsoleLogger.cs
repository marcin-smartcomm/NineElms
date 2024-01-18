using System;
using System.Collections.Generic;
using Crestron.SimplSharp;
using WebsocketServer;

namespace _9ElmsMain
{
    public static class ConsoleLogger
    {
        private static WebsocketSrvr _server;
        private static bool _clientConnected;

        private static List<string> _backlog;

        static ConsoleLogger()
        {
            try
            {
                _server = new WebsocketSrvr();
                _server.Initialize(55555);
                _server.OnClientConnectedChange += OnClientConnected;
                _server.OnStringSignalChange += OnReceivingMessage;

                _backlog = new List<string>();

                _clientConnected = false;
            }
            catch (Exception e)
            {
                WriteLine(e.ToString());
            }
        }

        public static void Start()
        {
            _server.StartServer();
        }

        public static void Stop()
        {
            _server.StopServer();
        }

        public static void WriteLine(string msg, params object[] args)
        {
            if (_backlog.Count > 100) _backlog.Clear();

            var text = String.Format(msg, args) + "\n";

                if (_clientConnected)
            {
                _server.SetIndirectTextSignal(1, text);
            }
            else
            {
                _backlog.Add(text);
            }
        }

        private static void OnClientConnected(ushort state)
        {
            if (state == 0)
            {
                // Disconnected
                _clientConnected = false;
            }
            else
            {
                // Connected
                _clientConnected = true;
                _server.SetIndirectTextSignal(1, "\n-- CONNECTED --\n");

                if (_backlog.Count > 0)
                {
                    foreach (var msg in _backlog)
                    {
                        _server.SetIndirectTextSignal(1, msg);
                    }
                }

                _backlog.Clear();
            }
        }


        private static void OnReceivingMessage(ushort state, SimplSharpString value)
        {
            if (value.ToString() == "__ping__")
            {
                _server.SetIndirectTextSignal(1, "__pong__");
            }
        }
    }
}
