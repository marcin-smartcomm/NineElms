using Newtonsoft.Json;

namespace NineElmsParksideBlockBMain
{
    public class RoomControl
    {
        public static string ChangeCourceSelected(int roomID, int srcID)
        {
            RoomCoreData rcd = JsonConvert.DeserializeObject<RoomCoreData>(FileOperations.loadRoomJson(roomID, "Core"));
            rcd.sourceSelected = rcd.menuItems[srcID].menuItemName;

            FileOperations.saveRoomJson(roomID.ToString(), "Core", JsonConvert.SerializeObject(rcd));

            ControlSystem.SendMessageToSIMPL($"Room{roomID}:Source{srcID}");

            RoomMenuItem selectedItem = rcd.menuItems[srcID];
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

            return rcd.sourceSelected;
        }
    }
}
