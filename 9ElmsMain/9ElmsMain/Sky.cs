using Crestron.SimplSharpPro;
using Crestron.SimplSharpPro.GeneralIO;

namespace _9ElmsMain
{
    public  class Sky
    {
        IROutputPort _IRPort;
        bool _irControl;

        public Sky(IROutputPort irPort)
        {
            _irControl = true;
            _IRPort = irPort;
        }

        public void PushButton(int btnNum)
        {
            if(_IRPort != null && _irControl)
            {
                switch (btnNum)
                {
                    case 0: _IRPort.PressAndRelease("Home", 25); break;
                    case 1: _IRPort.PressAndRelease("Dotdotdot", 25); break;
                    case 2: _IRPort.PressAndRelease("i", 25); break;
                    case 3: _IRPort.PressAndRelease("QuestionMark", 25); break;
                    case 4: _IRPort.PressAndRelease("1", 25); break;
                    case 5: _IRPort.PressAndRelease("2", 25); break;
                    case 6: _IRPort.PressAndRelease("3", 25); break;
                    case 7: _IRPort.PressAndRelease("Red", 25); break;
                    case 8: _IRPort.PressAndRelease("4", 25); break;
                    case 9: _IRPort.PressAndRelease("5", 25); break;
                    case 10: _IRPort.PressAndRelease("6", 25); break;
                    case 11: _IRPort.PressAndRelease("Green", 25); break;
                    case 12: _IRPort.PressAndRelease("7", 25); break;
                    case 13: _IRPort.PressAndRelease("8", 25); break;
                    case 14: _IRPort.PressAndRelease("9", 25); break;
                    case 15: _IRPort.PressAndRelease("Yellow", 25); break;
                    case 16: _IRPort.PressAndRelease("0", 25); break;
                    case 17: _IRPort.PressAndRelease("Blue", 25); break;
                    case 18: _IRPort.PressAndRelease("Up", 25); break;
                    case 19: _IRPort.PressAndRelease("Left", 25); break;
                    case 20: _IRPort.PressAndRelease("Ok", 25); break;
                    case 21: _IRPort.PressAndRelease("Right", 25); break;
                    case 22: _IRPort.PressAndRelease("Down", 25); break;
                    case 23: _IRPort.PressAndRelease("Ch+", 25); break;
                    case 24: _IRPort.PressAndRelease("Ch-", 25); break;
                    case 25: _IRPort.PressAndRelease("Rew", 25); break;
                    case 26: _IRPort.PressAndRelease("PlayPause", 25); break;
                    case 27: _IRPort.PressAndRelease("PlayPause", 25); break;
                    case 28: _IRPort.PressAndRelease("Record", 25); break;
                    case 29: _IRPort.PressAndRelease("Fwd", 25); break;
                    case 30: _IRPort.PressAndRelease("Back", 25); break;
                }
            }
            else
                ConsoleLogger.WriteLine("IRPort not defined, btnNum received: " + btnNum);
        }
    }
}
