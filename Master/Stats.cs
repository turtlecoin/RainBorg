using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace RainBorg
{
    // Utility class to hold tip information
    class Tip
    {
        public DateTime Date { get; }
        public double Amount { get; }
        public ulong Channel { get; }
        public Tip(DateTime date, ulong channel, double amount)
        {
            Date = date;
            Amount = amount;
            Channel = channel;
        }
    }

    // Utility class to hold channel information
    class StatTracker
    {
        public int TotalTips;
        public double TotalAmount;
        public double TipAverage;
        public List<Tip> Tips;
        public StatTracker()
        {
            TotalTips = 0;
            TotalAmount = 0;
            TipAverage = 0;
            Tips = new List<Tip>();
        }
    }

    class Stats
    {
        // Global stats
        public static StatTracker GlobalStats = new StatTracker();

        // Channel stats
        public static Dictionary<ulong, StatTracker> ChannelStats = new Dictionary<ulong, StatTracker>();

        // User stats
        public static Dictionary<ulong, StatTracker> UserStats = new Dictionary<ulong, StatTracker>();

        // Update user tips
        public static Task Tip(DateTime Date, ulong Channel, ulong Id, double Amount)
        {
            // Update user stats
            if (!UserStats.ContainsKey(Id))
                UserStats.Add(Id, new StatTracker());
            UserStats[Id].Tips.Add(new Tip(Date, Channel, Amount));
            UserStats[Id].TotalTips++;
            UserStats[Id].TotalAmount += Amount;
            UserStats[Id].TipAverage = UserStats[Id].TotalAmount / UserStats[Id].TotalTips;

            // Update channel stats
            if (!ChannelStats.ContainsKey(Channel))
                ChannelStats.Add(Channel, new StatTracker());
            ChannelStats[Channel].Tips.Add(new Tip(Date, Channel, Amount));
            ChannelStats[Channel].TotalTips++;
            ChannelStats[Channel].TotalAmount += Amount;
            ChannelStats[Channel].TipAverage = ChannelStats[Channel].TotalAmount / ChannelStats[Channel].TotalTips;

            // Update global stats
            GlobalStats.TotalTips++;
            GlobalStats.TotalAmount += Amount;
            GlobalStats.TipAverage = GlobalStats.TotalAmount / GlobalStats.TotalTips;

            // Completed
            return Task.CompletedTask;
        }

        // Loads stats from stat sheet
        public static async Task Load()
        {
            // Create stat sheet if it doesn't exist
            if (File.Exists(Constants.StatSheet))
            {
                // Load values
                JObject StatSheet = JObject.Parse(File.ReadAllText(Constants.StatSheet));
                GlobalStats = StatSheet["globalStats"].ToObject<StatTracker>();
                ChannelStats = StatSheet["channelStats"].ToObject<Dictionary<ulong, StatTracker>>();
                UserStats = StatSheet["userStats"].ToObject<Dictionary<ulong, StatTracker>>();
            }
            else await Update();
        }

        // Flush updates to stat sheet
        public static Task Update()
        {
            // Store values
            JObject StatSheet = new JObject
            {
                ["globalStats"] = JToken.FromObject(GlobalStats),
                ["channelStats"] = JToken.FromObject(ChannelStats),
                ["userStats"] = JToken.FromObject(UserStats)
            };

            // Flush to file
            File.WriteAllText(Constants.StatSheet, StatSheet.ToString());

            // Completed
            return Task.CompletedTask;
        }
    }
}
