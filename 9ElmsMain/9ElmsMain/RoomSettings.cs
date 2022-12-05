
namespace _9ElmsMain
{
    public class RoomSettings
    {
        public string roomName { get; set; }
        public short roomID { get; set; }

        public bool hasLights { get; set; }
        public bool hasHVAC { get; set; }
        public bool hasBGMusic { get; set; }
        public bool hasTV { get; set; }
        public bool hasNVX { get; set; }
        public bool hasFirePlace { get; set; }

        public int lightSceneSelected { get; set; }

        public float desiredTemp { get; set; }

        public string[] sources { get; set; }
        public string[] sourceType { get; set; }
        public string sourceSelected { get; set; }

        public int BGvolume { get; set; }
        public int[] IndividualBGVolume { get; set; }
        public bool BGMMuteState { get; set; }
        public bool[] IndividualMuteState { get; set; }

        public int SonosVolume { get; set; }
        public bool SonosMuteState { get; set; }

        public string[] TVNames { get; set; }
        public string[] TVIP { get; set; }
        public int[] TVPort { get; set; }
        public uint[] TVReceiverIPID { get; set; }
    }
}
