using FluorineFx;
using FluorineFx.AMF3;
using MspLib.Autorization;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using WebSocket4Net;

namespace MspLib.Socket
{
    public class MspSocketUser
    {
        private string webSocketPath = string.Empty;
        private WebSocket webSocket = null;
        public event EventHandler OnConnected;
        private static string GetWebSocketUrl(string server)
        {
            WebClient webClient = new WebClient();
            webClient.Proxy = null;
            return webClient.DownloadString((server == "US") ? "https://presence-us.mspapis.com/getServer" : "https://presence.mspapis.com/getServer");
        }

        public MspSocketUser(string Server)
        {
            webSocketPath = GetWebSocketUrl(Server);
            webSocket = new WebSocket($"ws://{webSocketPath.Replace('-', '.')}:10843/{webSocketPath.Replace('.', '-')}/?transport=websocket");
            webSocket.Opened += delegate (object sender, EventArgs e)
            {
                int PingId = 0;
                Timer timer = new Timer(5000);
                timer.Elapsed += delegate {
                    webSocket.Send("42[\"500\",{\"pingId\":" + PingId + ",\"messageContent\":null,\"lastPingDelay\":0,\"senderProfileId\":null,\"messageType\":500}]");
                    PingId++;
                };
                timer.AutoReset = true;
                timer.Enabled = true;
            };
            webSocket.MessageReceived += WebSocket_MessageReceived;
            webSocket.Open();
        }

        public static string serializeToString(object param1)
        {
            if(param1 == null)
            {
                throw new Exception("null isn\'t a legal serialization candidate");
            }
            ByteArray byteArray = new ByteArray();
            byteArray.WriteObject(param1);
            byteArray.Position = 0;
            return Convert.ToBase64String(byteArray.ToArray());
        }

        private void WebSocket_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            if (e.Message.StartsWith("42"))
            {
                string messageParse = e.Message.Substring(2);
                JArray messageParsed = JArray.Parse(messageParse);
                if ((int)messageParsed[1]["messageType"] == 11)
                {
                    if ((bool)messageParsed[1]["messageContent"]["success"])
                    {
                        OnConnected?.Invoke(null, null);
                    }
                }
            }
        }

        private void WaitIsConnected()
        {
            while(webSocket.State != WebSocketState.Open) { }
        }
        public void SendAuthentification(string Server, string AccessToken, string ProfileId)
        {
            WaitIsConnected();
            webSocket.Send("42[\"10\",{\"messageContent\":{\"country\":\"" + Server.ToUpper() + "\",\"version\":1,\"access_token\":\"" + AccessToken + "\",\"applicationId\":\"APPLICATION_WEB\",\"username\":\"" + ProfileId + "\"},\"senderProfileId\":null,\"messageType\":10}]");
        }
        public void CreateMovie(string Username, int ActorId, int MovieId)
        {
            webSocket.Send("42[\"101\",{\"messageType\":101,\"messageContent\":{\"serializedString\":\"" + serializeToString(new ASObject("NotificationObject")
            {
                { "localizedText", "I just created this super cool Movie." },
                { "actorId", ActorId },
                { "eventName", null },
                { "entityId", MovieId },
                { "applicationId", "APPLICATION_WEB" },
                { "iconSubPath", string.Empty },
                { "type", "MOVIE" },
                { "entityType", 3 },
                { "userId", ActorId },
                { "userName", Username },
                { "notificationTypeId", "MOVIE" },
                { "entitySnapshotSubPath", "https://snapshots.mspcdns.com/v1/snapshots/MSP_FR_movie_" + ChecksumCalculator.fileNamePartFromId(MovieId) + ".jpg" },
                { "importance", 1 },
                { "notificationCatetoryId", 2 }
            }) + "\",\"actorId\":" + ActorId + ",\"eventName\":null,\"rawObjUnparsed\":null,\"type\":\"MOVIE\",\"notificationObject\":{\"localizedText\":\"I just created this super cool Movie.\",\"actorId\":" + ActorId + ",\"eventName\":null,\"entityId\":" + MovieId + ",\"applicationId\":\"APPLICATION_WEB\",\"iconSubPath\":\"\",\"type\":\"MOVIE\",\"entityType\":3,\"userId\":" + ActorId + ",\"userName\":\"" + Username + "\",\"notificationTypeId\":\"MOVIE\",\"entitySnapshotSubPath\":\"https://snapshots.mspcdns.com/v1/snapshots/MSP_FR_movie_" + ChecksumCalculator.fileNamePartFromId(MovieId) + ".jpg\",\"importance\":1,\"notificationCatetoryId\":2},\"rawObj\":{\"localizedText\":\"I just created this super cool Movie.\",\"actorId\":" + ActorId + ",\"eventName\":null,\"entityId\":" + MovieId + ",\"applicationId\":\"APPLICATION_WEB\",\"iconSubPath\":\"\",\"type\":\"MOVIE\",\"entityType\":3,\"userId\":" + ActorId + ",\"userName\":\"" + Username + "\",\"notificationTypeId\":\"MOVIE\",\"entitySnapshotSubPath\":\"https://snapshots.mspcdns.com/v1/snapshots/MSP_FR_movie_" + ChecksumCalculator.fileNamePartFromId(MovieId) + ".jpg\",\"importance\":1,\"notificationCatetoryId\":2}},\"senderProfileId\":null}]");
        }
        public void WatchMovie(string MovieAuthorProfileId, int ActorId, string Username, int MovieId)
        {
            webSocket.Send("42[\"100\",{\"senderProfileId\":null,\"targetUserId\":\"" + MovieAuthorProfileId + "\",\"messageType\":100,\"messageContent\":{\"serializedString\":\"" + serializeToString(new ASObject("NotificationObject")
            {
                { "applicationId", null },
                { "eventName", null },
                { "userId", ActorId },
                { "actorId", ActorId },
                { "userName", Username },
                { "payload",
                    new
                    {
                        type = "HAS_WATCHED_YOUR_MOVIE",
                        actorName = Username,
                        userName = Username,
                        actorId = ActorId,
                        movieId = MovieId
                    } },
                { "type", "HAS_WATCHED_YOUR_MOVIE" },
                { "notificationTypeId", "HAS_WATCHED_YOUR_MOVIE" },
                { "iconSubPath", null },
                { "notificationCatetoryId", -1 },
                { "localizedText", null },
                { "importance", 0 }
            }) + "\",\"eventName\":null,\"actorId\":" + ActorId + ",\"notificationObject\":{\"applicationId\":null,\"eventName\":null,\"userId\":" + ActorId + ",\"actorId\":" + ActorId + ",\"userName\":\"" + Username + "\",\"payload\":{\"type\":\"HAS_WATCHED_YOUR_MOVIE\",\"actorName\":\"" + Username + "\",\"userName\":\"" + Username + "\",\"actorId\":" + ActorId + ",\"movieId\":" + MovieId + "},\"type\":\"HAS_WATCHED_YOUR_MOVIE\",\"notificationTypeId\":\"HAS_WATCHED_YOUR_MOVIE\",\"iconSubPath\":null,\"notificationCatetoryId\":-1,\"localizedText\":null,\"importance\":0},\"type\":\"HAS_WATCHED_YOUR_MOVIE\",\"rawObj\":{\"type\":\"HAS_WATCHED_YOUR_MOVIE\",\"actorName\":\"" + Username + "\",\"userName\":\"" + Username + "\",\"actorId\":" + ActorId + ",\"movieId\":" + MovieId + "},\"rawObjUnparsed\":null}}]");
        }
    }
}
