// Copyright (c) 2018, BrandonT42, The TurtleCoin Developers
//
// Please see the included LICENSE file for more information.

using Discord;
using Discord.Commands;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace RainBorg.Commands
{
    public partial class Commands : ModuleBase<SocketCommandContext>
    {
        [Command("info")]
        public async Task InfoAsync([Remainder]string Remainder = null)
        {
            if (RainBorg.Operators.Contains(Context.Message.Author.Id))
            {
                using (WebClient client = new WebClient())
                {
                    string dl = client.DownloadString(RainBorg.balanceURL);

                    JObject j = JObject.Parse(dl);
                    RainBorg.tipBalance = (double)j["balance"];
                }

                double i = RainBorg.tipMin + RainBorg.tipFee - RainBorg.tipBalance;
                if (i < 0) i = 0;

                string m = "```Current tip balance: " + String.Format("{0:n}", RainBorg.tipBalance) + " " + RainBorg.tipCurrency + "\n" +
                    "Amount needed for next tip: " + String.Format("{0:n}", i) + " " + RainBorg.tipCurrency + "\n" +
                    "Next tip at: " + RainBorg.waitNext + "\n" +
                    "Tip minimum: " + String.Format("{0:n}", RainBorg.tipMin) + " " + RainBorg.tipCurrency + "\n" +
                    "Tip maximum: " + String.Format("{0:n}", RainBorg.tipMax) + " " + RainBorg.tipCurrency + "\n" +
                    "Minimum users: " + RainBorg.userMin + "\n" +
                    "Maximum users: " + RainBorg.userMax + "\n" +
                    "Minimum wait time: " + String.Format("{0:n0}", RainBorg.waitMin) + "s (" + TimeSpan.FromSeconds(RainBorg.waitMin).ToString() + ")\n" +
                    "Maximum wait time: " + String.Format("{0:n0}", RainBorg.waitMax) + "s (" + TimeSpan.FromSeconds(RainBorg.waitMax).ToString() + ")\n" +
                    "Message timeout: " + String.Format("{0:n0}", RainBorg.timeoutPeriod) + "s (" + TimeSpan.FromSeconds(RainBorg.timeoutPeriod).ToString() + ")\n" +
                    "Minimum account age: " + TimeSpan.FromHours(RainBorg.accountAge).ToString() + "\n" +
                    "Operators: " + RainBorg.Operators.Count + "\n" +
                    "Blacklisted: " + RainBorg.Blacklist.Count + "\n" +
                    "Greylisted: " + RainBorg.Greylist.Count + "\n" +
                    "Channels: " + RainBorg.UserPools.Keys.Count + "\n" +
                    "Paused: " + RainBorg.Paused.ToString() +
                    "```";
                await Context.Message.Author.SendMessageAsync(m);
            }
        }

        [Command("operators")]
        public async Task OperatorsAsync([Remainder]string Remainder = null)
        {
            if (RainBorg.Operators.Contains(Context.Message.Author.Id))
            {
                string m = "```Operators:\n";
                foreach (ulong i in RainBorg.Operators)
                    try
                    {
                        m += Context.Client.GetUser(i).Username + "\n";
                    }
                    catch { }
                m += "```";
                await Context.Message.Author.SendMessageAsync(m);
            }
        }

        [Command("blacklist")]
        public async Task BlacklistAsync([Remainder]string Remainder = null)
        {
            if (RainBorg.Operators.Contains(Context.Message.Author.Id))
            {
                string m = "```Blacklisted Users:\n";
                foreach (KeyValuePair<ulong, string> i in RainBorg.Blacklist)
                    try
                    {
                        m += Context.Client.GetUser(i.Key).Username + " (" + i.Key + ")";
                        if (i.Value != "")
                            m += " - " + i.Value;
                        m += "\n";
                    }
                    catch
                    {
                        m += "User Not Found (" + i.Key + ")";
                        if (i.Value != "")
                            m += " - " + i.Value;
                        m += "\n";
                    }
                m += "\nGreylisted Users:\n";
                foreach (ulong i in RainBorg.Greylist)
                    try
                    {
                        m += Context.Client.GetUser(i).Username + " (" + i + ")\n";
                    }
                    catch
                    {
                        m += i + "\n";
                    }
                m += "```";
                await Context.Message.Author.SendMessageAsync(m);
            }
        }

        [Command("userpools")]
        public async Task UserPoolsAsync([Remainder]string Remainder = null)
        {
            if (RainBorg.Operators.Contains(Context.Message.Author.Id))
            {
                string m = "```Current User Pools:\n";
                foreach (KeyValuePair<ulong, List<ulong>> entry in RainBorg.UserPools)
                    try
                    {
                        m += "#" + Context.Client.GetChannel(entry.Key) + " (" + entry.Key + ") :\n";

                        List<ulong> v = entry.Value;
                        foreach (ulong s in v) m += Context.Client.GetUser(s).Username + " (" + s + ")\n";
                        m += "\n\n";
                    }
                    catch { }
                m += "```";
                await Context.Message.Author.SendMessageAsync(m);
            }
        }

        [Command("channels")]
        public async Task ChannelsAsync([Remainder]string Remainder = null)
        {
            if (RainBorg.Operators.Contains(Context.Message.Author.Id))
            {
                string m = "```Tippable Channels:\n";
                foreach (KeyValuePair<ulong, List<ulong>> entry in RainBorg.UserPools)
                    try
                    {
                        m += "#" + Context.Client.GetChannel(entry.Key) + ", weight of ";

                        var x = RainBorg.ChannelWeight.GroupBy(i => i);
                        foreach (var channel in x)
                            if (channel.Key == entry.Key) m += channel.Count();

                        m += "\n";
                    }
                    catch { }
                m += "```";
                await Context.Message.Author.SendMessageAsync(m);
            }
        }

        [Command("stats")]
        public async Task StatsAsync([Remainder]ulong Id = 0)
        {
            if (RainBorg.Operators.Contains(Context.Message.Author.Id))
            {
                string m = "```";

                // Channel stats
                if (Stats.ChannelStats.ContainsKey(Id))
                {
                    m += "#" + Context.Client.GetChannel(Id) + " Channel Stats:\n";
                    m += "Total " + RainBorg.tipCurrency + " Sent: " + String.Format("{0:n}", Stats.ChannelStats[Id].TotalAmount) + " " + RainBorg.tipCurrency + "\n";
                    m += "Total Tips Sent: " + Stats.ChannelStats[Id].TotalTips + "\n";
                    m += "Average Tip: " + String.Format("{0:n}", Stats.ChannelStats[Id].TipAverage) + " " + RainBorg.tipCurrency;
                }

                // User stats
                else if (Stats.UserStats.ContainsKey(Id))
                {
                    m += "@" + Context.Client.GetUser(Id).Username + " User Stats:\n";
                    m += "Total " + RainBorg.tipCurrency + " Sent: " + String.Format("{0:n}", Stats.UserStats[Id].TotalAmount) + " " + RainBorg.tipCurrency + "\n";
                    m += "Total Tips Sent: " + Stats.UserStats[Id].TotalTips + "\n";
                    m += "Average Tip: " + String.Format("{0:n}", Stats.UserStats[Id].TipAverage) + " " + RainBorg.tipCurrency;
                }

                // Global stats
                else
                {
                    m += "Global Stats:\n";
                    m += "Total " + RainBorg.tipCurrency + " Sent: " + String.Format("{0:n}", Stats.GlobalStats.TotalAmount) + " " + RainBorg.tipCurrency + "\n";
                    m += "Total Tips Sent: " + Stats.GlobalStats.TotalTips + "\n";
                    m += "Average Tip: " + String.Format("{0:n}", Stats.GlobalStats.TipAverage) + " " + RainBorg.tipCurrency;
                }

                m += "```";
                await Context.Message.Author.SendMessageAsync(m);
            }
        }
    }
}
