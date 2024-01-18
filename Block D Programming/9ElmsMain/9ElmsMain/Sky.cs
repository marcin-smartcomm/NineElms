using Crestron.SimplSharpPro;
using Crestron.SimplSharpPro.GeneralIO;
using Crestron.SimplSharp.CrestronIO;
using System;

namespace _9ElmsMain
{
    public  class Sky
    {
        ControlSystem _cs;
        int _IRPort;

        public Sky(int irPort, ControlSystem cs)
        {
            _IRPort = irPort;
            _cs = cs;
        }

        public void PushButton(int btnNum)
        {
            if(_IRPort == 1)
                _cs.PushSky2Button(btnNum);
            if (_IRPort == 2)
                _cs.PushSky1Button(btnNum);
        }
    }
}
