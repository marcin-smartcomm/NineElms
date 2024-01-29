using Crestron.SimplSharpPro;
using Crestron.SimplSharpPro.Gateways;
using Crestron.SimplSharpPro.Remotes;
using Newtonsoft.Json;

namespace NineElmsParksideBlockBMain
{
    public class CinemaRemote
    {
        ControlSystem _cs;

        CenGwExEr cinemaGW;
        Hr310 cinemaRemote;

        public CinemaRemote(ControlSystem cs)
        {
            _cs = cs;

            cinemaGW = new CenGwExEr(0x30, cs);
            cinemaGW.Register();

            cinemaRemote = new Hr310(0x03, cinemaGW);
            cinemaRemote.Register();

            cinemaRemote.OnlineStatusChange += CinemaRemote_OnlineStatusChange;
            cinemaRemote.ButtonStateChange += CinemaRemote_ButtonStateChange;

            ConsoleLogger.WriteLine("Cinema Remote Added to system");
        }

        private void CinemaRemote_ButtonStateChange(GenericBase device, Crestron.SimplSharpPro.DeviceSupport.ButtonEventArgs args)
        {
            if (args.Button.State == Crestron.SimplSharpPro.DeviceSupport.eButtonState.Pressed)
            {
                string currentCinemaSource = JsonConvert.DeserializeObject<RoomCoreData>(FileOperations.loadRoomJson(6, "Core")).sourceSelected;
                ConsoleLogger.WriteLine("CurrentCinemaSource: " + currentCinemaSource);

                if (args.Button.Name == Crestron.SimplSharpPro.DeviceSupport.eButtonName.Custom1) RoomControl.ChangeCourceSelected(6, 0);
                if (args.Button.Name == Crestron.SimplSharpPro.DeviceSupport.eButtonName.Custom2) RoomControl.ChangeCourceSelected(6, 1);
                if (args.Button.Name == Crestron.SimplSharpPro.DeviceSupport.eButtonName.Custom3) RoomControl.ChangeCourceSelected(6, 2);
                if (args.Button.Name == Crestron.SimplSharpPro.DeviceSupport.eButtonName.Custom4) RoomControl.ChangeCourceSelected(6, 3);

                if (args.Button.Name == Crestron.SimplSharpPro.DeviceSupport.eButtonName.VolumeUp)
                    RoomControl.VolUp("6");
                if (args.Button.Name == Crestron.SimplSharpPro.DeviceSupport.eButtonName.VolumeDown)
                    RoomControl.VolDown("6");
                if (args.Button.Name == Crestron.SimplSharpPro.DeviceSupport.eButtonName.Mute)
                    RoomControl.Mute("6");

                if (args.Button.Name == Crestron.SimplSharpPro.DeviceSupport.eButtonName.Power)
                    RoomControl.Shutdown("6");

                if (args.Button.Name == Crestron.SimplSharpPro.DeviceSupport.eButtonName.Keypad1)
                    if (currentCinemaSource.Contains("Sky")) _cs.SkyQBtnPress("1");
                if (args.Button.Name == Crestron.SimplSharpPro.DeviceSupport.eButtonName.Keypad2Abc)
                    if (currentCinemaSource.Contains("Sky")) _cs.SkyQBtnPress("2");
                if (args.Button.Name == Crestron.SimplSharpPro.DeviceSupport.eButtonName.Keypad3Def)
                    if (currentCinemaSource.Contains("Sky")) _cs.SkyQBtnPress("3");
                if (args.Button.Name == Crestron.SimplSharpPro.DeviceSupport.eButtonName.Keypad4Ghi)
                    if (currentCinemaSource.Contains("Sky")) _cs.SkyQBtnPress("4");
                if (args.Button.Name == Crestron.SimplSharpPro.DeviceSupport.eButtonName.Keypad5Jkl)
                    if (currentCinemaSource.Contains("Sky")) _cs.SkyQBtnPress("5");
                if (args.Button.Name == Crestron.SimplSharpPro.DeviceSupport.eButtonName.Keypad6Mno)
                    if (currentCinemaSource.Contains("Sky")) _cs.SkyQBtnPress("6");
                if (args.Button.Name == Crestron.SimplSharpPro.DeviceSupport.eButtonName.Keypad7Pqrs)
                    if (currentCinemaSource.Contains("Sky")) _cs.SkyQBtnPress("7");
                if (args.Button.Name == Crestron.SimplSharpPro.DeviceSupport.eButtonName.Keypad8Tuv)
                    if (currentCinemaSource.Contains("Sky")) _cs.SkyQBtnPress("8");
                if (args.Button.Name == Crestron.SimplSharpPro.DeviceSupport.eButtonName.Keypad9Wxyz)
                    if (currentCinemaSource.Contains("Sky"))  _cs.SkyQBtnPress("9");
                if (args.Button.Name == Crestron.SimplSharpPro.DeviceSupport.eButtonName.Keypad0)
                    if (currentCinemaSource.Contains("Sky")) _cs.SkyQBtnPress("0");

                if (args.Button.Name == Crestron.SimplSharpPro.DeviceSupport.eButtonName.Home)
                    if (currentCinemaSource.Contains("Sky")) _cs.SkyQBtnPress("Home");
                if (args.Button.Name == Crestron.SimplSharpPro.DeviceSupport.eButtonName.Guide)
                    if (currentCinemaSource.Contains("Sky")) _cs.SkyQBtnPress("Sky");
                if (args.Button.Name == Crestron.SimplSharpPro.DeviceSupport.eButtonName.ChannelUp)
                    if (currentCinemaSource.Contains("Sky")) _cs.SkyQBtnPress("Ch+");
                if (args.Button.Name == Crestron.SimplSharpPro.DeviceSupport.eButtonName.ChannelDown)
                    if (currentCinemaSource.Contains("Sky")) _cs.SkyQBtnPress("Ch-");
                if (args.Button.Name == Crestron.SimplSharpPro.DeviceSupport.eButtonName.Info)
                    if (currentCinemaSource.Contains("Sky")) _cs.SkyQBtnPress("i");
                if (args.Button.Name == Crestron.SimplSharpPro.DeviceSupport.eButtonName.Menu)
                {
                    if (currentCinemaSource.Contains("Sky")) _cs.SkyQBtnPress("Dotdotdot");
                    if (currentCinemaSource.Contains("Apple")) _cs.AppleTVBtnPress("MENU");
                }

                if (args.Button.Name == Crestron.SimplSharpPro.DeviceSupport.eButtonName.DialPadUp)
                {
                    if (currentCinemaSource.Contains("Sky")) _cs.SkyQBtnPress("Up");
                    if (currentCinemaSource.Contains("Apple")) _cs.AppleTVBtnPress("UP_ARROW");
                }
                if (args.Button.Name == Crestron.SimplSharpPro.DeviceSupport.eButtonName.DialPadDown)
                {
                    if (currentCinemaSource.Contains("Sky")) _cs.SkyQBtnPress("Down");
                    if (currentCinemaSource.Contains("Apple")) _cs.AppleTVBtnPress("DOWN_ARROW");
                }
                if (args.Button.Name == Crestron.SimplSharpPro.DeviceSupport.eButtonName.DialPadLeft)
                {
                    if (currentCinemaSource.Contains("Sky")) _cs.SkyQBtnPress("Left");
                    if (currentCinemaSource.Contains("Apple")) _cs.AppleTVBtnPress("LEFT_ARROW");
                }
                if (args.Button.Name == Crestron.SimplSharpPro.DeviceSupport.eButtonName.DialPadRight)
                {
                    if (currentCinemaSource.Contains("Sky")) _cs.SkyQBtnPress("Right");
                    if (currentCinemaSource.Contains("Apple")) _cs.AppleTVBtnPress("RIGHT_ARROW");
                }
                if (args.Button.Name == Crestron.SimplSharpPro.DeviceSupport.eButtonName.DialPadSelect)
                {
                    if (currentCinemaSource.Contains("Sky")) _cs.SkyQBtnPress("Ok");
                    if (currentCinemaSource.Contains("Apple")) _cs.AppleTVBtnPress("SELECT");
                }

                if (args.Button.Name == Crestron.SimplSharpPro.DeviceSupport.eButtonName.Red)
                    if (currentCinemaSource.Contains("Sky")) _cs.SkyQBtnPress("Red");
                if (args.Button.Name == Crestron.SimplSharpPro.DeviceSupport.eButtonName.Green)
                    if (currentCinemaSource.Contains("Sky")) _cs.SkyQBtnPress("Green");
                if (args.Button.Name == Crestron.SimplSharpPro.DeviceSupport.eButtonName.Yellow)
                    if (currentCinemaSource.Contains("Sky")) _cs.SkyQBtnPress("Yellow");
                if (args.Button.Name == Crestron.SimplSharpPro.DeviceSupport.eButtonName.Blue)
                    if (currentCinemaSource.Contains("Sky")) _cs.SkyQBtnPress("Blue");

                if (args.Button.Name == Crestron.SimplSharpPro.DeviceSupport.eButtonName.Exit)
                    if (currentCinemaSource.Contains("Sky")) _cs.SkyQBtnPress("Back");
                if (args.Button.Name == Crestron.SimplSharpPro.DeviceSupport.eButtonName.Last)
                { }

                if (args.Button.Name == Crestron.SimplSharpPro.DeviceSupport.eButtonName.Rewind)
                    if (currentCinemaSource.Contains("Sky")) _cs.SkyQBtnPress("Rew");
                if (args.Button.Name == Crestron.SimplSharpPro.DeviceSupport.eButtonName.Play)
                {
                    if (currentCinemaSource.Contains("Sky")) _cs.SkyQBtnPress("Play/Pause");
                    if (currentCinemaSource.Contains("Apple")) _cs.AppleTVBtnPress("PLAY/PAUSE");
                }
                if (args.Button.Name == Crestron.SimplSharpPro.DeviceSupport.eButtonName.FastForward)
                    if (currentCinemaSource.Contains("Sky")) _cs.SkyQBtnPress("Fwd");
                if (args.Button.Name == Crestron.SimplSharpPro.DeviceSupport.eButtonName.PreviousTrack)
                { }
                if (args.Button.Name == Crestron.SimplSharpPro.DeviceSupport.eButtonName.Pause)
                {
                    if (currentCinemaSource.Contains("Sky")) _cs.SkyQBtnPress("Play/Pause");
                    if (currentCinemaSource.Contains("Apple")) _cs.AppleTVBtnPress("PLAY/PAUSE");
                }
                if (args.Button.Name == Crestron.SimplSharpPro.DeviceSupport.eButtonName.NextTrack)
                { }
                if (args.Button.Name == Crestron.SimplSharpPro.DeviceSupport.eButtonName.Stop)
                { }
                if (args.Button.Name == Crestron.SimplSharpPro.DeviceSupport.eButtonName.Dvr)
                { }
                if (args.Button.Name == Crestron.SimplSharpPro.DeviceSupport.eButtonName.Record)
                    if (currentCinemaSource.Contains("Sky")) _cs.SkyQBtnPress("Record");
            }
        }
        private void CinemaRemote_OnlineStatusChange(GenericBase currentDevice, OnlineOfflineEventArgs args)
        {
            ConsoleLogger.WriteLine("Cinema Remote Online Status changed to: " + args.DeviceOnLine);
        }
    }
}
