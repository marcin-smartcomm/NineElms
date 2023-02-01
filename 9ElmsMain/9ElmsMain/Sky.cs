using Crestron.SimplSharpPro;
using Crestron.SimplSharpPro.GeneralIO;
using Crestron.SimplSharp.CrestronIO;
using System;

namespace _9ElmsMain
{
    public  class Sky
    {
        ControlSystem _cs;
        IROutputPort _IRPort;
        bool _irControl;

        public Sky(IROutputPort irPort, ControlSystem cs)
        {
            _irControl = true;
            _IRPort = irPort;
            _cs = cs;
        }

        public void PushButton(int btnNum)
        {
            _cs.PushSky2Button(btnNum);
            return;

            try
            {
                switch (btnNum)
                {
                    case 0: _IRPort.PressAndRelease("SKY", 25); break;
                    case 1: _IRPort.PressAndRelease("TV_GUIDE", 25); break;
                    case 2: _IRPort.PressAndRelease("I", 25); break;
                    case 3: _IRPort.PressAndRelease("BOX_OFFICE", 25); break;
                    case 4: _IRPort.PressAndRelease("1", 25); break;
                    case 5: _IRPort.PressAndRelease("2", 25); break;
                    case 6: _IRPort.PressAndRelease("3", 25); break;
                    case 7: _IRPort.PressAndRelease("RED", 25); break;
                    case 8: _IRPort.PressAndRelease("4", 25); break;
                    case 9: _IRPort.PressAndRelease("5", 25); break;
                    case 10: _IRPort.PressAndRelease("6", 25); break;
                    case 11: _IRPort.PressAndRelease("GREEN", 25); break;
                    case 12: _IRPort.PressAndRelease("7", 25); break;
                    case 13: _IRPort.PressAndRelease("8", 25); break;
                    case 14: _IRPort.PressAndRelease("9", 25); break;
                    case 15: _IRPort.PressAndRelease("YELLOW", 25); break;
                    case 16: _IRPort.PressAndRelease("0", 25); break;
                    case 17: _IRPort.PressAndRelease("BLUE", 25); break;
                    case 18: _IRPort.PressAndRelease("UP", 25); break;
                    case 19: _IRPort.PressAndRelease("LEFT", 25); break;
                    case 20: _IRPort.PressAndRelease("SELECT", 25); break;
                    case 21: _IRPort.PressAndRelease("RIGHT", 25); break;
                    case 22: _IRPort.PressAndRelease("DOWN", 25); break;
                    case 23: _IRPort.PressAndRelease("CH+", 25); break;
                    case 24: _IRPort.PressAndRelease("CH-", 25); break;
                    case 25: _IRPort.PressAndRelease("REV", 25); break;
                    case 26: _IRPort.PressAndRelease("PLAY", 25); break;
                    case 27: _IRPort.PressAndRelease("STOP", 25); break;
                    case 28: _IRPort.PressAndRelease("RECORD", 25); break;
                    case 29: _IRPort.PressAndRelease("FFWD", 25); break;
                    case 30: _IRPort.PressAndRelease("BACK_UP", 25); break;
                    case 31: _IRPort.PressAndRelease("PAUSE", 25); break;
                }
            }
            catch(Exception ex)
            {
                ConsoleLogger.WriteLine("Problem in Sky: " + ex);
            }
        }
    }
}
