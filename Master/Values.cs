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
            _version = "1.8",
            _timezone = TimeZone.CurrentTimeZone.StandardName,
            botAddress = "TRTLv12WtKJAzTNtxSkbcXf7mjeVApSqRYACtoJE2X52UBSce7qGAQ1JQgG3MmArnZSbkJXKqBXiPX2Mno7xD4tqD3p8SySoBc5",
            botPaymentId = "bca975edfe710a64337beb1685f32ab900989aa9767946efd8537f09db594bbd",
            successReact = "kthx",
            waitNext = "";

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

            waitMin = 1 * 60,
            waitMax = 1 * 60,
            waitTime = 1,

            accountAge = 3,

            timeoutPeriod = 30;

        public static List<ulong>
            Operators = new List<ulong>(),
            OptedOut = new List<ulong>();

        [JsonExtensionData]
        public static List<ulong>
            Greylist = new List<ulong>();

        public static Dictionary<ulong, string>
            Blacklist = new Dictionary<ulong, string>();

        [JsonExtensionData]
        public static Dictionary<ulong, List<ulong>>
            UserPools = new Dictionary<ulong, List<ulong>>();

        [JsonExtensionData]
        public static Dictionary<ulong, UserMessage>
            UserMessages = new Dictionary<ulong, UserMessage>();

        public static List<ulong>
            ChannelWeight = new List<ulong>(),
            StatusChannel = new List<ulong>();

        public static string
            tipBalanceError = "My tip balance was too low to send out a tip, consider donating {0} TRTL to keep the rain a-pouring!\n\n" +
                "To donate, simply send some TRTL to the following address, REMEMBER to use the provided payment ID, or else your funds will NOT reach the tip pool.\n" +
                "```Address:\n" + botAddress + "\n" + "Payment ID (INCLUDE THIS):\n" + botPaymentId + "```",
            entranceMessage = "",
            exitMessage = "",
            wikiURL = "https://github.com/Sajo811/turtlewiki/wiki/RainBorg-Wat-Dat",
            spamWarning = "You've been issued a spam warning, this means you won't be included in my next tip. Try to be a better turtle, okay? ;) Consider reading up on how to be a good turtle:\nhttps://medium.com/@turtlecoin/how-to-be-a-good-turtle-20a427028a18";

        private static List<string>
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
