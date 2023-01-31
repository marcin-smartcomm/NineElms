using Newtonsoft.Json;
using System.IO;

namespace _9ElmsMain
{
    public static class FileOperations
    {
        public static RoomSettings loadRoomSettings(string roomNum)
        {
            StreamReader sr = new StreamReader("../nvram/Room" + roomNum + ".json");

            string json = sr.ReadToEnd();
            sr.Close();

            return JsonConvert.DeserializeObject<RoomSettings>(json);
        }

        public static ProcessorSettings loadProcessorSettings()
        {
            StreamReader sr = new StreamReader("../nvram/ProcessorSettings.json");

            string json = sr.ReadToEnd();
            sr.Close();

            return JsonConvert.DeserializeObject<ProcessorSettings>(json);
        }

        public static void UpdateSettings(string roomNum, RoomSettings rs)
        {
            File.Delete("../nvram/Room" + roomNum + ".json");
            File.WriteAllText(
                "../nvram/Room" + roomNum + ".json",
                JsonConvert.SerializeObject(rs, Formatting.Indented));
        }
    }
}
