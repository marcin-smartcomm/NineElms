using Crestron.SimplSharpPro.AudioDistribution;
using Lutron.Leap.CommLib.Models.Bodies.SubClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace _9ElmsMain
{
    public class HVACProcessor
    {
        int _connectionRequests = 0;
        AsyncTCPClient _comms;

        public event Action<short, float> actualTempChanged;
        public event Action<short, float> desiredTempChanged;

        public HVACProcessor(string ip, int port)
        {
            _comms = new AsyncTCPClient(ip, port, 4000);
            _comms.ConnectedEvent += _comms_ConnectedEvent;
            _comms.MessageReceived += _comms_MessageReceived;
            _comms.Connect();
        }

        public HVACProcessor(AsyncTCPClient comms)
        {
            _comms = comms;
        }

        public void TempUp(int procID, int roomID)
        {
            _comms.SendMessage("HVAC:Proc"+procID+":Room"+roomID+":TempUp");
        }

        public void TempDown(int procID, int roomID)
        {
            _comms.SendMessage("HVAC:Proc" + procID + ":Room" + roomID + ":TempDown");
        }

        public void evaluateMessage(string message)
        {
            string[] newInfo = message.Split(':');

            short roomID = short.Parse(newInfo[2].Remove(0, 4));
            float newTempValue = float.Parse(newInfo[4]);

            if (newInfo[3].Contains("ActualTemp"))
                OnActualTempChanged(roomID, newTempValue);
            if (newInfo[3].Contains("DesiredTemp"))
                OnDesiredTempChanged(roomID, newTempValue);
        }

        public void OnActualTempChanged(short roomID, float actualTemp)
        {
            if (actualTempChanged != null)
                actualTempChanged(roomID, actualTemp);
        }
        public void OnDesiredTempChanged(short roomID, float desiredTemp)
        {
            if (desiredTempChanged != null)
                desiredTempChanged(roomID, desiredTemp);
        }

        private void _comms_ConnectedEvent(bool obj)
        {
            ConsoleLogger.WriteLine("Connected to HVAC Processor");
        }
        private void _comms_MessageReceived(object source, MessageReceivedEventArgs args)
        {

        }

        public void Connect()
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
    }
}
