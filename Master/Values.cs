// Copyright (c) 2018, BrandonT42, The TurtleCoin Developers
//
// Please see the included LICENSE file for more information.

using Discord.Commands;
using Discord.WebSocket;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace RainBorg
{
    partial class RainBorg
    {
        public static DiscordSocketClient _client;
        public static CommandService _commands;
        public static IServiceProvider _services;

        public static string
            _username = "RainBorg",
            _version = "1.9",
            successReact = "kthx",
            waitNext = "",
            tipTrigger = ".tip",
            tipCurrency = "TRTL",
            botToken = "NEED_THIS_GET_IT_AND_PUT_IT_HERE",
            balanceURL = "",
            botPrefix = "$";


        public static double
            tipBalance = 0,
            tipFee = 0.1,
            tipMin = 1,
            tipMax = 10,
            tipAmount = 1;

        public static int
            userMin = 1,
            userMax = 20,
            logLevel = 1,
            logLevelDiscord = 0,

            waitMin = 1 * 60,
            waitMax = 1 * 60,
            waitTime = 1,

            accountAge = 3,

            timeoutPeriod = 30,

            discordPermissions = 76864;

        public static List<ulong>
            Operators = new List<ulong>(),
            OptedOut = new List<ulong>();

        [JsonExtensionData]
        public static List<ulong>
            Greylist = new List<ulong>();

        public static Dictionary<ulong, string>
            Blacklist = new Dictionary<ulong, string>();

        public static Dictionary<string, string>
            Messages = new Dictionary<string, string>()
             {
                {"rainTitle", "TUT TUT"},
                {"rain1", "Huzzah, "},
                {"rain2", " just rained on "},
                {"rain3", " chatty turtle"},
                {"tipBalanceErrorTitle", "UH OH"},
                {"tipBalanceError", "My tipjar balance was too low to send out a tip, consider donating to keep the rain a-pouring!\n\nTo donate, simply send me tips.\n"},
                {"entranceMessage", ""},
                {"exitMessage", ""},
                {"wikiURL", "https,//github.com/turtlecoin/turtlecoin/wiki/RainBorg-Wat-Dat"},
                {"wikiURLop", "https,//github.com/turtlecoin/rainborg/wiki/Operator-Commands"},
                {"spamWarning", "You've been issued a spam warning, this means you won't be included in my next tip. Complaining about this is the best way to get blacklisted to never receive tips in the future"}
             };

        [JsonExtensionData]
        public static Dictionary<ulong, List<ulong>>
            UserPools = new Dictionary<ulong, List<ulong>>();

        [JsonExtensionData]
        public static Dictionary<ulong, UserMessage>
            UserMessages = new Dictionary<ulong, UserMessage>();

        public static List<ulong>
            ChannelWeight = new List<ulong>(),
            StatusChannel = new List<ulong>();

        public static List<string>
            RaindanceImages = new List<string>
            {
                "https://i.imgur.com/6zJpNZx.png",
                "https://i.imgur.com/fM26s0m.png",
                "https://i.imgur.com/SdWh89i.png"
            },
            DonationImages = new List<string>
            {
                "https://i.imgur.com/SZgzfAC.png"
            };

        private static string
            Banner =
            "\n" +
            " ██████         ███      █████████   ███      ███   ██████         ███      ██████         ██████   \n" +
            " ███   ███   ███   ███      ███      ██████   ███   ███   ███   ███   ███   ███   ███   ███      ███\n" +
            " ███   ███   ███   ███      ███      ██████   ███   ██████      ███   ███   ███   ███   ███         \n" +
            " ██████      █████████      ███      ███   ██████   ███   ███   ███   ███   ██████      ███   ██████\n" +
            " ███   ███   ███   ███      ███      ███   ██████   ███   ███   ███   ███   ███   ███   ███      ███\n" +
            " ███   ███   ███   ███   █████████   ███      ███   ██████         ███      ███   ███      ██████    v" + _version;

        public static double
            Waiting = 0;

        public static bool
            Startup = true,
            ShowDonation = true,
            Paused = false;

        static ConsoleEventDelegate handler;
        private delegate bool ConsoleEventDelegate(int eventType);
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetConsoleCtrlHandler(ConsoleEventDelegate callback, bool add);
    }

    // Utility class for serialization of message log on restart
    public class UserMessage
    {
        public DateTimeOffset CreatedAt;
        public string Content;
        public UserMessage(SocketMessage Message)
        {
            CreatedAt = Message.CreatedAt;
            Content = Message.Content;
        }
        public UserMessage() { }
    }
}
