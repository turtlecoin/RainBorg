using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RainBorg
{
    partial class RainBorg
    {
        // Checks a message for spam
        private Task CheckForSpamAsync(SocketUserMessage message, out bool result)
        {

            // Purposely left empty to keep spam filters private

            // Completed
            result = false;
            return Task.CompletedTask;
        }
    }
}
