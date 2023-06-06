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
        ControlSystem _comms;

        public event Action<short, float> actualTempChanged;
        public event Action<short, float> desiredTempChanged;

        public HVACProcessor(string ip, int port)
        {

        }

        public HVACProcessor(ControlSystem comms)
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
    }
}
