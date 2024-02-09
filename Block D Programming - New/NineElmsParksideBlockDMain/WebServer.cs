using System.Net;
using System.Text;
using System;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace NineElmsParksideBlockDMain
{
    class WebServer
    {
        ControlSystem _cs;

        public WebServer(ControlSystem cs)
        {
            try
            {
                _cs = cs;
                ListenAsync();
            }
            catch (Exception ex)
            {
                ConsoleLogger.WriteLine("Problem in WebServer Constructor: " + ex.Message);
            }
        }

        public async void ListenAsync()
        {
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://*:50000/api/");
            listener.Start();

            ConsoleLogger.WriteLine("Server Started...");

            while (true)
            {
                try
                {
                    //Await Client Request
                    HttpListenerContext context = await listener.GetContextAsync();
                    await Task.Run(() => ProcessRequestAsync(context));
                }
                catch (HttpListenerException) { break; }
                catch (InvalidOperationException) { break; }
            }

            listener.Stop();
        }

        async void ProcessRequestAsync(HttpListenerContext context)
        {
            try
            {
                //Respond to Request
                string response = "";
                string incomingRequest = context.Request.RawUrl;
                ConsoleLogger.WriteLine("Request Coming on " + context.Request.RawUrl + " || from: " + context.Request.RemoteEndPoint.Address.ToString());

                if (incomingRequest.Contains("/RoomData"))
                {
                    string clientIP = context.Request.RemoteEndPoint.Address.ToString();
                    string roomID = incomingRequest.Split('?')[1];

                    if (roomID.Contains("999"))
                        response = FileOperations.loadRoomJson(GetRoomAssigned(clientIP), "Core");
                    else
                        response = FileOperations.loadRoomJson(Int32.Parse(roomID), "Core");
                }

                if (incomingRequest.Contains("/RoomsList"))
                {
                    List<string> roomData = new List<string>();
                    foreach (string directory in FileOperations.GetRoomDirectories())
                    {
                        string roomRaw = directory.Split('/')[1];
                        int roomID = int.Parse(roomRaw.Replace("Room", ""));

                        RoomCoreData rcd = JsonConvert.DeserializeObject<RoomCoreData>(FileOperations.loadRoomJson(roomID, "Core"));
                        roomData.Add($"{rcd.roomName}:{rcd.roomID}");
                    }

                    response = JsonConvert.SerializeObject(roomData);
                }

                if (incomingRequest.Contains("ChangeZone"))
                {
                    string clientIP = context.Request.RemoteEndPoint.Address.ToString();
                    string newRoomID = incomingRequest.Split('?')[1];

                    IPtoRoom ipToRoom = JsonConvert.DeserializeObject<IPtoRoom>(FileOperations.loadCoreInfo("IPtoRoom"));
                    int indexOfClient = Array.IndexOf(ipToRoom.IPAddress, clientIP);
                    ipToRoom.RoomID[indexOfClient] = int.Parse(newRoomID);

                    FileOperations.saveCoreJson("IPtoRoom", JsonConvert.SerializeObject(ipToRoom));

                    response = "{ \"CommandProcessed\": \"true\" }";
                }

                #region SourceSelection

                else if (incomingRequest.Contains("ChangeSouceSelected"))
                {
                    string roomID = incomingRequest.Split('?')[1].Split(':')[0];
                    string srcName = incomingRequest.Split('?')[1].Split(':')[1].Replace("%20", " ");

                    response = "{ \"currentSource\": \"" + RoomControl.ChangeSourceSelected(int.Parse(roomID), srcName) + "\" }";
                }

                else if (incomingRequest.Contains("GetSouceSelected"))
                {
                    string roomID = incomingRequest.Split('?')[1];

                    RoomCoreData rcd = JsonConvert.DeserializeObject<RoomCoreData>(FileOperations.loadRoomJson(Int32.Parse(roomID), "Core"));

                    response = "{ \"currentSource\": \"" + rcd.sourceSelected + "\" }";
                }

                #endregion

                #region VolumeControls

                else if (incomingRequest.Contains("ChangeVolumeLevel"))
                {
                    string roomID = incomingRequest.Split('?')[1].Split(':')[0];
                    string newLevel = incomingRequest.Split('?')[1].Split(':')[1];

                    RoomCoreData rcd = JsonConvert.DeserializeObject<RoomCoreData>(FileOperations.loadRoomJson(int.Parse(roomID), "Core"));

                    RoomMenuItem currentSource = rcd.menuItems.Find(x => x.menuItemName == rcd.sourceSelected);

                    string roomPart = string.Empty;
                    if (int.Parse(roomID) > 9) roomPart = $"Room{roomID}";
                    else roomPart = $"Room0{roomID}";

                    if (!currentSource.volumeThroughSonos) ControlSystem.SendMessageToSIMPL($"BGM:{roomPart}:Volume:{newLevel}");
                    else ControlSystem.SendMessageToSIMPL($"SNS:{roomPart}:Volume:{newLevel}");

                    response = "{ \"CommandProcessed\": \"true\" }";
                }

                else if (incomingRequest.Contains("MuteVolume"))
                {
                    string roomID = incomingRequest.Split('?')[1];

                    RoomCoreData rcd = JsonConvert.DeserializeObject<RoomCoreData>(FileOperations.loadRoomJson(int.Parse(roomID), "Core"));

                    RoomMenuItem currentSource = rcd.menuItems.Find(x => x.menuItemName == rcd.sourceSelected);

                    if (currentSource.volControlType == "Btns") RoomControl.Mute(roomID);
                    if (currentSource.volControlType == "Slider")
                    {
                        if (!currentSource.volumeThroughSonos) ControlSystem.SendMessageToSIMPL($"BGM:Room{roomID}:MuteToggle");
                        else ControlSystem.SendMessageToSIMPL($"SNS:Room{roomID}:MuteToggle");
                    }

                    response = "{ \"CommandProcessed\": \"true\" }";
                }

                else if (incomingRequest.Contains("GetSliderLevel"))
                {
                    response = "{ \"CommandProcessed\": \"true\" }";
                }

                else if (incomingRequest.Contains("GetMuteState"))
                {
                    response = "{ \"CommandProcessed\": \"true\" }";
                }

                else if (incomingRequest.Contains("VolUpBtnPress"))
                {
                    string roomID = incomingRequest.Split('?')[1];
                    RoomControl.VolUp(roomID);

                    response = "{ \"CommandProcessed\": \"true\" }";
                }

                else if (incomingRequest.Contains("VolDownBtnPress"))
                {
                    string roomID = incomingRequest.Split('?')[1];
                    RoomControl.VolDown(roomID);

                    response = "{ \"CommandProcessed\": \"true\" }";
                }

                else if (incomingRequest.Contains("TVOnBtnPress"))
                {
                    string roomID = incomingRequest.Split('?')[1];
                    ControlSystem.SendMessageToSIMPL($"Room{roomID}TVPON");

                    response = "{ \"CommandProcessed\": \"true\" }";
                }

                else if (incomingRequest.Contains("TVOffBtnPress"))
                {
                    string roomID = incomingRequest.Split('?')[1];
                    ControlSystem.SendMessageToSIMPL($"Room{roomID}TVPOFF");

                    response = "{ \"CommandProcessed\": \"true\" }";
                }

                #endregion

                #region Source Control

                else if (incomingRequest.Contains("/FreeviewCtrl"))
                {
                    string roomID = incomingRequest.Split('?')[1].Split(':')[0];
                    string btnPressed = incomingRequest.Split('?')[1].Split(':')[1];

                    ControlSystem.FreeviewBtnPress(roomID, btnPressed);
                }

                else if (incomingRequest.Contains("/SkyCtrl"))
                {
                    string roomID = incomingRequest.Split('?')[1].Split(':')[0];
                    string btnPressed = incomingRequest.Split('?')[1].Split(':')[1];

                    _cs.SkyBtnPress(int.Parse(roomID) ,btnPressed);

                    response = "{ \"CommandProcessed\": \"true\" }";
                }

                #endregion

                else if (incomingRequest.Contains("ChangeRoom"))
                {
                    string clientIP = context.Request.RemoteEndPoint.Address.ToString();
                    string newRoomID = incomingRequest.Split('?')[1];

                    IPtoRoom itr = JsonConvert.DeserializeObject<IPtoRoom>(FileOperations.loadCoreInfo("IPtoRoom"));

                    for (int i = 0; i < itr.IPAddress.Length; i++)
                        if (itr.IPAddress[i] == clientIP)
                            itr.RoomID[i] = int.Parse(newRoomID);

                    FileOperations.saveCoreJson("IPtoRoom", JsonConvert.SerializeObject(itr));

                    response = "{ \"roomChangeStatus\": \"success\" }";
                }

                else if (incomingRequest.Contains("/RoomInfoUpdate"))
                {
                    string roomID = incomingRequest.Split('?')[1];
                    RoomCoreData rcd = JsonConvert.DeserializeObject<RoomCoreData>(FileOperations.loadRoomJson(int.Parse(roomID), "Core"));
                    
                    UpdateInfo update = new UpdateInfo();
                    update.fireAlarm = ControlSystem.fireAlarm;
                    update.volMute = rcd.volMute;
                    update.volLevel = rcd.volLevel;
                    update.sourceSelected = rcd.sourceSelected;

                    response = JsonConvert.SerializeObject(update);
                }

                else if (incomingRequest.Contains("/RoomShutdown"))
                {
                    string roomID = incomingRequest.Split('?')[1];

                    response = "{ \"currentSource\": \"" + RoomControl.Shutdown(roomID) + "\" }";
                }

                context.Response.ContentLength64 = Encoding.UTF8.GetByteCount(response);
                context.Response.AddHeader("Access-Control-Allow-Origin", "*");
                context.Response.AddHeader("Access-Control-Allow-Methods", "*");
                context.Response.AddHeader("Access-Control-Allow-Headers", "*");
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.OK;

                using (Stream s = context.Response.OutputStream)
                using (StreamWriter writer = new StreamWriter(s))
                    await writer.WriteAsync(response);
            }
            catch (Exception ex) { ConsoleLogger.WriteLine("Bad Request: " + ex.Message); }
        }

        int GetRoomAssigned(string TP_IPAddress)
        {
            IPtoRoom ipToRoomData = JsonConvert.DeserializeObject<IPtoRoom>(FileOperations.loadCoreInfo("IPtoRoom"));

            for (int i = 0; i < ipToRoomData.IPAddress.Length; i++)
                if (ipToRoomData.IPAddress[i] == TP_IPAddress)
                    return ipToRoomData.RoomID[i];

            return 2;
        }
    }

    public class IPtoRoom
    {
        public string[] IPAddress { get; set; }
        public int[] RoomID { get; set; }
    }

    public class UpdateInfo
    {
        public bool fireAlarm { get; set; }
        public string sourceSelected { get; set; }
        public int volLevel { get; set; }
        public bool volMute { get; set; }
    }
}
