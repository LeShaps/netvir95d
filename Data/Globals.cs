using Discord;
using Discord.WebSocket;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

using Netvir.Services;
using Netvir.Attributes;
using Netvir.Exceptions;

namespace Netvir.Data
{
    class Globals
    {
        /* Discord */
        public static string BotToken { get; private set; }
        public static DiscordSocketClient Client;

        /* Ownership */
        public static string OwnerName { get; private set; }
        public static ulong OwnerId { get; private set; }

        /* Advanced debug */
        public static string DebugWebhookUrl { get; private set; }

        /* Services */
        public static Listener ListenerService = new Listener("http://localhost:3000/Connect/", "http://localhost:3000/Report/");
        public static Dictionary<string, bool> ServiceStatus = new Dictionary<string, bool>();
        public static Dictionary<string, string> ServiceThrowReasons = new Dictionary<string, string>();

        /* Location data */
        public static Dictionary<ulong, ITextChannel> ReportChannels = new Dictionary<ulong, ITextChannel>();

        public static void InitConfig()
        {
            if (!File.Exists("Loggers/Credentials.json"))
                throw new FileNotFoundException($"You must have a \"Credentials.json\" file located in {AppDomain.CurrentDomain.BaseDirectory}Loggers");
            JObject ConfigurationJson = JsonConvert.DeserializeObject<JObject>(File.ReadAllText("Loggers/Credentials.json"));
            if (ConfigurationJson["botToken"] == null || ConfigurationJson["ownerId"] == null || ConfigurationJson["ownerStr"] == null)
                throw new FileNotFoundException("Missing critical informations in Credentials.json, please complete mandatory informations before continuing");

            BotToken = ConfigurationJson.Value<string>("botToken");
            OwnerName = ConfigurationJson.Value<string>("ownerStr");
            OwnerId = ConfigurationJson.Value<ulong>("ownerId");

            InitServiceStatus();
        }

        public static void InitListener()
        {
            if (!ServiceStatus["Listener"])
                throw new UnavailableServiceException(ServiceThrowReasons["Listener"]);
            ListenerService.OnMessageReceived += HttpResponseProcess.Dispatch;
            ListenerService.OnStartListening += HttpResponseProcess.StartingNotice;
            ListenerService.OnStopListening += HttpResponseProcess.StopListeningNotice;
        }

        public static void InitServiceStatus()
        {
            Assembly CurrentAssembly = Assembly.GetExecutingAssembly();

            var Services = CurrentAssembly.GetTypes()
                .Where(x => x.GetCustomAttribute<ServiceAttribute>() != null)
                .Select(x => x.GetCustomAttribute<ServiceAttribute>())
                .ToArray();

            foreach (ServiceAttribute service in Services)
            {
                if (ServiceStatus.ContainsKey(service.Name)) {
                    ServiceStatus[service.Name] = service.IsActive;
                    ServiceThrowReasons[service.Name] = service.ThrowReason;
                } else {
                    ServiceStatus.Add(service.Name, service.IsActive);
                    ServiceThrowReasons.Add(service.Name, service.ThrowReason);
                }
            }
        }
    }
}