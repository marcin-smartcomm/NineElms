using Newtonsoft.Json;
using System.Collections.Generic;

namespace NineElmsParksideBlockDMain
{
    public class RoomControl
    {
        public static string ChangeSourceSelected(int roomID, string srcName)
        {
            RoomCoreData rcd = JsonConvert.DeserializeObject<RoomCoreData>(FileOperations.loadRoomJson(roomID, "Core"));
            rcd.sourceSelected = srcName;

            FileOperations.saveRoomJson(roomID.ToString(), "Core", JsonConvert.SerializeObject(rcd));

            RoomMenuItem selectedItem = rcd.menuItems.Find(x => x.menuItemName == srcName);

            ControlSystem.SendMessageToSIMPL($"Room{roomID}:Source{rcd.menuItems.FindIndex(a => a == selectedItem)}");

            if (selectedItem.tvRequired)
            {
                ControlSystem.SendMessageToSIMPL($"Room{roomID}TVPON");
                ControlSystem.SendMessageToSIMPL($"Room{roomID}TVHDMI{selectedItem.tvHDMIRequired}");
            }
            else ControlSystem.SendMessageToSIMPL($"Room{roomID}TVPOFF");

            return rcd.sourceSelected;
        }

        public static void VolUp(string roomID)
        {
            ControlSystem.SendMessageToSIMPL($"Room{roomID}TVKP:Vol+");
        }

        public static void VolDown(string roomID)
        {
            ControlSystem.SendMessageToSIMPL($"Room{roomID}TVKP:Vol-");
        }

        public static void Mute(string roomID)
        {
            ControlSystem.SendMessageToSIMPL($"Room{roomID}TVKP:MuteToggle");
        }

        public static string Shutdown(string roomID)
        {
            RoomCoreData rcd = JsonConvert.DeserializeObject<RoomCoreData>(FileOperations.loadRoomJson(int.Parse(roomID), "Core"));
            rcd.sourceSelected = "Off";
            FileOperations.saveRoomJson(roomID, "Core", JsonConvert.SerializeObject(rcd));

            ControlSystem.SendMessageToSIMPL($"Room{roomID}TVPOFF");
            ControlSystem.SendMessageToSIMPL($"BGM:Room{roomID}:MuteOn");
            ControlSystem.SendMessageToSIMPL($"SNS:Room{roomID}:MuteOn");

            return rcd.sourceSelected;
        }
    }
    public class RoomMenuItem
    {
        public string menuItemName { get; set; }
        public string menuItemPageAssigned { get; set; }
        public string volControlType { get; set; }
        public bool tvRequired { get; set; }
        public int tvHDMIRequired { get; set; }
        public bool volumeThroughSonos { get; set; }
    }

    public class RoomCoreData
    {
        public int roomID { get; set; }
        public int floor { get; set; }
        public string roomName { get; set; }
        public int leftNeighbour { get; set; }
        public int rightNeighbour { get; set; }
        public List<RoomMenuItem> menuItems { get; set; }
        public string sourceSelected { get; set; }
        public int volLevel { get; set; }
        public bool volMute { get; set; }
        public uint skyIRPort { get; set; }
    }
}
