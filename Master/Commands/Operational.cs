// Copyright (c) 2018, BrandonT42, The TurtleCoin Developers
//
// Please see the included LICENSE file for more information.

using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RainBorg.Commands
{
    public partial class Commands : ModuleBase<SocketCommandContext>
    {
        [Command("pause")]
        public async Task PauseAsync([Remainder]string Remainder = null)
        {
            if (RainBorg.Operators.Contains(Context.Message.Author.Id))
            {
                RainBorg.Paused = true;
                await Context.Message.Author.SendMessageAsync("Bot paused.");
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

        [Command("resume")]
        public async Task ResumeAsync([Remainder]string Remainder = null)
        {
            if (RainBorg.Operators.Contains(Context.Message.Author.Id))
            {
                RainBorg.Paused = false;
                await Context.Message.Author.SendMessageAsync("Bot resumed.");
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

        [Command("adduser")]
        public async Task AddUserAsync([Remainder]string Remainder = null)
        {
            if (RainBorg.Operators.Contains(Context.Message.Author.Id))
            {
                foreach (SocketUser user in Context.Message.MentionedUsers)
                    try
                    {
                        if (user != null && RainBorg.UserPools.ContainsKey(Context.Channel.Id) && !RainBorg.UserPools[Context.Channel.Id].Contains(user.Id))
                            RainBorg.UserPools[Context.Channel.Id].Add(user.Id);
                    }
                    catch { }
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

        [Command("adduser")]
        public async Task AddUserAsync(params ulong[] users)
        {
            if (RainBorg.Operators.Contains(Context.Message.Author.Id))
            {
                foreach (ulong user in users)
                    try
                    {
                        if (RainBorg.UserPools.ContainsKey(Context.Channel.Id) && !RainBorg.UserPools[Context.Channel.Id].Contains(user))
                            RainBorg.UserPools[Context.Channel.Id].Add(user);
                    }
                    catch { }
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

        [Command("removeuser")]
        public async Task RemoveUserAsync([Remainder]string Remainder = null)
        {
            if (RainBorg.Operators.Contains(Context.Message.Author.Id))
            {
                foreach (SocketUser user in Context.Message.MentionedUsers)
                    try
                    {
                        if (RainBorg.UserPools.ContainsKey(Context.Channel.Id) && RainBorg.UserPools[Context.Channel.Id].Contains(user.Id))
                            RainBorg.UserPools[Context.Channel.Id].Remove(user.Id);
                    }
                    catch { }
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

        [Command("removeuser")]
        public async Task RemoveUserAsync(params ulong[] users)
        {
            if (RainBorg.Operators.Contains(Context.Message.Author.Id))
            {
                foreach (ulong user in users)
                    try
                    {
                        if (RainBorg.UserPools.ContainsKey(Context.Channel.Id) && RainBorg.UserPools[Context.Channel.Id].Contains(user))
                            RainBorg.UserPools[Context.Channel.Id].Remove(user);
                    }
                    catch { }
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

        [Command("reset")]
        public async Task ResetAsync([Remainder]string Remainder = null)
        {
            if (RainBorg.Operators.Contains(Context.Message.Author.Id))
            {
                foreach (KeyValuePair<ulong, List<ulong>> Entry in RainBorg.UserPools)
                    Entry.Value.Clear();
                RainBorg.Greylist.Clear();
                await Context.Message.Author.SendMessageAsync("User pools and greylist cleared.");
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

        [Command("dotip")]
        public async Task DoTipAsync([Remainder]string Remainder = null)
        {
            if (RainBorg.Operators.Contains(Context.Message.Author.Id))
            {
                RainBorg.Waiting = RainBorg.waitTime;
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

        [Command("exit")]
        public Task ExitAsync([Remainder]string Remainder = null)
        {
            if (RainBorg.Operators.Contains(Context.Message.Author.Id))
            {
                RainBorg.ConsoleEventCallback(2);
                Environment.Exit(0);
            }
            return Task.CompletedTask;
        }
    }
}
