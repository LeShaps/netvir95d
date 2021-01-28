using Discord.WebSocket;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System;
using System.IO;

using Netvir.Services;

namespace Netvir.Data
{
    class Globals
    {
        public static string BotToken { get; private set; }

        public static DiscordSocketClient Client;

        // public static DbSystem Db = new DbSystem();

        public static string DebugWebhookUrl { get; private set; }

        public static Listener ListenerService = new Listener("http://localhost:3000/");

        public static void InitConfig()
        {
            if (!File.Exists("Loggers/Credentials.json"))
                throw new FileNotFoundException($"You must have a \"Credentials.json\" file located in {AppDomain.CurrentDomain.BaseDirectory}Loggers");
            JObject ConfigurationJson = JsonConvert.DeserializeObject<JObject>(File.ReadAllText("Loggers/Credentials.json"));
            if (ConfigurationJson["botToken"] == null || ConfigurationJson["ownerId"] == null || ConfigurationJson["ownerStr"] == null)
                throw new FileNotFoundException("Missing critical informations in Credentials.json, please complete mandatory informations before continuing");

            BotToken = ConfigurationJson.Value<string>("botToken");
        }

        public static void InitListener()
        {
            ListenerService.OnMessageReceived += HttpResponseProcess.PrintReceivedMessage;
            ListenerService.OnStartListening += HttpResponseProcess.StartingNotice;
            ListenerService.OnStopListening += HttpResponseProcess.StopListeningNotice;
        }
    }
}
