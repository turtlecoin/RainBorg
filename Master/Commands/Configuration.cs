// Copyright (c) 2018, BrandonT42, The TurtleCoin Developers
//
// Please see the included LICENSE file for more information.

using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Linq;
using System.Threading.Tasks;

namespace RainBorg.Commands
{
    public partial class Commands : ModuleBase<SocketCommandContext>
    {
        [Command("tipmin")]
        public async Task TipMinAsync(double s, [Remainder]string Remainder = null)
        {
            if (RainBorg.Operators.Contains(Context.Message.Author.Id))
            {
                RainBorg.tipMin = s;
                await Config.Save();
                try
                {
                    // Add reaction to message
                    IEmote emote = Context.Guild.Emotes.First(e => e.Name == RainBorg.successReact);
                    await Context.Message.AddReactionAsync(emote);
                }
                catch
                {
                    await Context.Message.AddReactionAsync(new Emoji("👌"));
                }
            }
        }

        [Command("tipmax")]
        public async Task TipMaxAsync(double s, [Remainder]string Remainder = null)
        {
            if (RainBorg.Operators.Contains(Context.Message.Author.Id))
            {
                RainBorg.tipMax = s;
                await Config.Save();
                try
                {
                    // Add reaction to message
                    IEmote emote = Context.Guild.Emotes.First(e => e.Name == RainBorg.successReact);
                    await Context.Message.AddReactionAsync(emote);
                }
                catch
                {
                    await Context.Message.AddReactionAsync(new Emoji("👌"));
                }
            }
        }

        [Command("usermin")]
        public async Task UserMinAsync(int s, [Remainder]string Remainder = null)
        {
            if (RainBorg.Operators.Contains(Context.Message.Author.Id))
            {
                RainBorg.userMin = s;
                await Config.Save();
                try
                {
                    // Add reaction to message
                    IEmote emote = Context.Guild.Emotes.First(e => e.Name == RainBorg.successReact);
                    await Context.Message.AddReactionAsync(emote);
                }
                catch
                {
                    await Context.Message.AddReactionAsync(new Emoji("👌"));
                }
            }
        }

        [Command("usermax")]
        public async Task UserMaxAsync(int s, [Remainder]string Remainder = null)
        {
            if (RainBorg.Operators.Contains(Context.Message.Author.Id))
            {
                RainBorg.userMax = s;
                await Config.Save();
                try
                {
                    // Add reaction to message
                    IEmote emote = Context.Guild.Emotes.First(e => e.Name == RainBorg.successReact);
                    await Context.Message.AddReactionAsync(emote);
                }
                catch
                {
                    await Context.Message.AddReactionAsync(new Emoji("👌"));
                }
            }
        }

        [Command("waitmin")]
        public async Task WaitMinAsync(int s, [Remainder]string Remainder = null)
        {
            if (RainBorg.Operators.Contains(Context.Message.Author.Id))
            {
                RainBorg.waitMin = s;
                await Config.Save();
                try
                {
                    // Add reaction to message
                    IEmote emote = Context.Guild.Emotes.First(e => e.Name == RainBorg.successReact);
                    await Context.Message.AddReactionAsync(emote);
                }
                catch
                {
                    await Context.Message.AddReactionAsync(new Emoji("👌"));
                }
            }
        }

        [Command("waitmax")]
        public async Task WaitMaxAsync(int s, [Remainder]string Remainder = null)
        {
            if (RainBorg.Operators.Contains(Context.Message.Author.Id))
            {
                RainBorg.waitMax = s;
                await Config.Save();
                try
                {
                    // Add reaction to message
                    IEmote emote = Context.Guild.Emotes.First(e => e.Name == RainBorg.successReact);
                    await Context.Message.AddReactionAsync(emote);
                }
                catch
                {
                    await Context.Message.AddReactionAsync(new Emoji("👌"));
                }
            }
        }

        [Command("accountage")]
        public async Task AccountAgeAsync(int s, [Remainder]string Remainder = null)
        {
            if (RainBorg.Operators.Contains(Context.Message.Author.Id))
            {
                RainBorg.accountAge = s;
                await Config.Save();
                try
                {
                    // Add reaction to message
                    IEmote emote = Context.Guild.Emotes.First(e => e.Name == RainBorg.successReact);
                    await Context.Message.AddReactionAsync(emote);
                }
                catch
                {
                    await Context.Message.AddReactionAsync(new Emoji("👌"));
                }
            }
        }

        [Command("timeout")]
        public async Task TimeoutAsync(int s, [Remainder]string Remainder = null)
        {
            if (RainBorg.Operators.Contains(Context.Message.Author.Id))
            {
                RainBorg.timeoutPeriod = s;
                await Config.Save();
                try
                {
                    // Add reaction to message
                    IEmote emote = Context.Guild.Emotes.First(e => e.Name == RainBorg.successReact);
                    await Context.Message.AddReactionAsync(emote);
                }
                catch
                {
                    await Context.Message.AddReactionAsync(new Emoji("👌"));
                }
            }
        }
    }
}
