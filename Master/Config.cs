// Copyright (c) 2018, BrandonT42, The TurtleCoin Developers
//
// Please see the included LICENSE file for more information.

using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace RainBorg
{
    class Config
    {
        public static async Task Load()
        {
            // Check if config file exists and create it if it doesn't
            if (File.Exists(Constants.Config))
            {
                // Load values
                JObject Config = JObject.Parse(File.ReadAllText(Constants.Config));
                RainBorg._username = (string)Config["username"];
                RainBorg.tipFee = (double)Config["tipFee"];
                RainBorg.tipMin = (double)Config["tipMin"];
                RainBorg.tipMax = (double)Config["tipMax"];
                RainBorg.userMin = (int)Config["userMin"];
                RainBorg.userMax = (int)Config["userMax"];
                RainBorg.waitMin = (int)Config["waitMin"];
                RainBorg.waitMax = (int)Config["waitMax"];
                RainBorg.accountAge = (int)Config["accountAge"];
                RainBorg.timeoutPeriod = (int)Config["timeoutPeriod"];
                RainBorg.logLevel = (int)Config["logLevel"];
                RainBorg.logLevelDiscord = (int)Config["logLevelDiscord"];
                RainBorg.Operators = Config["operators"].ToObject<List<ulong>>();
                RainBorg.Blacklist = Config["blacklist"].ToObject<Dictionary<ulong, string>>();
                RainBorg.Messages = Config["messages"].ToObject<Dictionary<string, string>>();
                RainBorg.RaindanceImages = Config["raindanceImages"].ToObject<List<string>>();
                RainBorg.DonationImages = Config["donationImages"].ToObject<List<string>>();
                RainBorg.OptedOut = Config["optedOut"].ToObject<List<ulong>>();
                RainBorg.ChannelWeight = Config["channelWeight"].ToObject<List<ulong>>();
                RainBorg.StatusChannel = Config["statusChannel"].ToObject<List<ulong>>();
                RainBorg.tipTrigger = (string)Config["tipTrigger"];
                RainBorg.tipCurrency = (string)Config["tipCurrency"];
                RainBorg.botToken = (string)Config["botToken"];
                RainBorg.balanceURL = (string)Config["balanceURL"];
                RainBorg.botPrefix = (string)Config["botPrefix"];
                foreach (ulong Id in RainBorg.ChannelWeight)
                    if (!RainBorg.UserPools.ContainsKey(Id))
                        RainBorg.UserPools.Add(Id, new List<ulong>());
            }
            else await Save();
        }

        public static Task Save()
        {
            // Store values
            JObject Config = new JObject
            {
                ["username"] = RainBorg._username,
                ["tipFee"] = RainBorg.tipFee,
                ["tipMin"] = RainBorg.tipMin,
                ["tipMax"] = RainBorg.tipMax,
                ["userMin"] = RainBorg.userMin,
                ["userMax"] = RainBorg.userMax,
                ["waitMin"] = RainBorg.waitMin,
                ["waitMax"] = RainBorg.waitMax,
                ["accountAge"] = RainBorg.accountAge,
                ["timeoutPeriod"] = RainBorg.timeoutPeriod,
                ["logLevel"] = RainBorg.logLevel,
                ["logLevelDiscord"] = RainBorg.logLevelDiscord,
                ["operators"] = JToken.FromObject(RainBorg.Operators),
                ["blacklist"] = JToken.FromObject(RainBorg.Blacklist),
                ["messages"] = JToken.FromObject(RainBorg.Messages),
                ["optedOut"] = JToken.FromObject(RainBorg.OptedOut),
                ["channelWeight"] = JToken.FromObject(RainBorg.ChannelWeight),
                ["statusChannel"] = JToken.FromObject(RainBorg.StatusChannel),
                ["raindanceImages"] = JToken.FromObject(RainBorg.RaindanceImages),
                ["donationImages"] = JToken.FromObject(RainBorg.DonationImages),
                ["tipTrigger"] = RainBorg.tipTrigger,
                ["tipCurrency"] = RainBorg.tipCurrency,
                ["botToken"] = RainBorg.botToken,
                ["balanceURL"] = RainBorg.balanceURL,
                ["botPrefix"] = RainBorg.botPrefix
            };

            // Flush to file
            File.WriteAllText(Constants.Config, Config.ToString());

            // Completed
            return Task.CompletedTask;
        }
    }
}
