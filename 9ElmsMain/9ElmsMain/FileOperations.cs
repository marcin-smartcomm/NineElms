using Newtonsoft.Json;
using System.IO;

namespace _9ElmsMain
{
    public static class FileOperations
    {
        public static RoomSettings loadRoomSettings(string roomNum)
        {
            StreamReader sr = new StreamReader("../Nvram/Room" + roomNum + ".json");

            string json = sr.ReadToEnd();
            sr.Close();

            return JsonConvert.DeserializeObject<RoomSettings>(json);
        }

        public static ProcessorSettings loadProcessorSettings()
        {
            StreamReader sr = new StreamReader("../Nvram/ProcessorSettings.json");

            string json = sr.ReadToEnd();
            sr.Close();

            return JsonConvert.DeserializeObject<ProcessorSettings>(json);
        }

        public static void UpdateSettings(string roomNum, RoomSettings rs)
        {
            File.Delete("../Nvram/Room" + roomNum + ".json");
            File.WriteAllText(
                "../Nvram/Room" + roomNum + ".json",
                JsonConvert.SerializeObject(rs, Formatting.Indented));
        }
    }
}
