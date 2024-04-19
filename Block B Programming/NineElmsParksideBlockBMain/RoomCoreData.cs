using System.Collections.Generic;

namespace NineElmsParksideBlockBMain
{
    public class RoomMenuItem
    {
        public string menuItemName { get; set; }
        public string menuItemPageAssigned { get; set; }
        public string volControlType { get; set; }
        public bool tvRequired { get; set; }
        public int tvHDMIRequired { get; set; }
        public uint skyIRPort { get; set; }
    }

    public class RoomCoreData
    {
        public int roomID { get; set; }
        public int floor { get; set; }
        public string roomName { get; set; }
        public int neighbourRoomID { get; set; }
        public int leftNeighbour { get; set; }
        public int rightNeighbour { get; set; }
        public List<RoomMenuItem> menuItems { get; set; }
        public string sourceSelected { get; set; }
        public int volLevel { get; set; }
        public bool volMute { get; set; }
        public bool tvCardRequired { get; set; }
    }
}
