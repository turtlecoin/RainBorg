using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Linq;
using System.Threading.Tasks;

namespace RainBorg.Commands
{
    public partial class Commands : ModuleBase<SocketCommandContext>
    {
        [Command("exile")]
        public async Task ExileAsync(SocketUser user, [Remainder]string Remainder = "")
        {
            if (RainBorg.Operators.Contains(Context.Message.Author.Id))
            {
                if (!RainBorg.Blacklist.ContainsKey(user.Id))
                {
                    RainBorg.Blacklist.Add(user.Id, Remainder);
                    await RainBorg.RemoveUserAsync(user, 0);
                }
                await Config.Save();
                await ReplyAsync("Blacklisted users, they will receive no tips.");
                try
                {

                    IEmote emote = Context.Guild.Emotes.First(e => e.Name == RainBorg.successReact);
                    await Context.Message.AddReactionAsync(emote);
                }
                catch
                {
                    await Context.Message.AddReactionAsync(new Emoji("👌"));
                }
            }
        }

        [Command("exile")]
        public async Task ExileAsync(ulong user, [Remainder]string Remainder = "")
        {
            if (RainBorg.Operators.Contains(Context.Message.Author.Id))
            {
                try
                {
                    if (!RainBorg.Blacklist.ContainsKey(Context.Client.GetUser(user).Id))
                    {
                        RainBorg.Blacklist.Add(Context.Client.GetUser(user).Id, Remainder);
                        await RainBorg.RemoveUserAsync(Context.Client.GetUser(user), 0);
                    }
                }
                catch { }
                await Config.Save();
                await ReplyAsync("Blacklisted users, they will receive no tips.");
                try
                {

                    IEmote emote = Context.Guild.Emotes.First(e => e.Name == RainBorg.successReact);
                    await Context.Message.AddReactionAsync(emote);
                }
                catch
                {
                    await Context.Message.AddReactionAsync(new Emoji("👌"));
                }
            }
        }

        [Command("unexile")]
        public async Task UnExileAsync(SocketUser user, [Remainder]string Remainder = null)
        {
            if (RainBorg.Operators.Contains(Context.Message.Author.Id))
            {
                if (RainBorg.Blacklist.ContainsKey(user.Id))
                    RainBorg.Blacklist.Remove(user.Id);
                await Config.Save();
                await ReplyAsync("Removed users from blacklist, they may receive tips again.");
                try
                {

                    IEmote emote = Context.Guild.Emotes.First(e => e.Name == RainBorg.successReact);
                    await Context.Message.AddReactionAsync(emote);
                }
                catch
                {
                    await Context.Message.AddReactionAsync(new Emoji("👌"));
                }
            }
        }

        [Command("unexile")]
        public async Task UnExileAsync(ulong user)
        {
            if (RainBorg.Operators.Contains(Context.Message.Author.Id))
            {
                try
                {
                    if (RainBorg.Blacklist.ContainsKey(Context.Client.GetUser(user).Id))
                        RainBorg.Blacklist.Remove(Context.Client.GetUser(user).Id);
                }
                catch { }
                await Config.Save();
                await ReplyAsync("Removed users from blacklist, they may receive tips again.");
                try
                {

                    IEmote emote = Context.Guild.Emotes.First(e => e.Name == RainBorg.successReact);
                    await Context.Message.AddReactionAsync(emote);
                }
                catch
                {
                    await Context.Message.AddReactionAsync(new Emoji("👌"));
                }
            }
        }

        [Command("warn")]
        public async Task WarnAsync([Remainder]string Remainder = null)
        {
            if (RainBorg.Operators.Contains(Context.Message.Author.Id))
            {
                foreach (SocketUser user in Context.Message.MentionedUsers)
                    if (user != null && !RainBorg.Greylist.Contains(user.Id))
                    {
                        EmbedBuilder builder = new EmbedBuilder();
                        builder.WithColor(Color.Green);
                        builder.WithTitle("SPAM WARNING");
                        builder.Description = RainBorg.Messages["spamWarning"];

                        RainBorg.Greylist.Add(user.Id);
                        await RainBorg.RemoveUserAsync(user, 0);
                        await user.SendMessageAsync("", false, builder);
                    }
                try
                {

                    IEmote emote = Context.Guild.Emotes.First(e => e.Name == RainBorg.successReact);
                    await Context.Message.AddReactionAsync(emote);
                }
                catch
                {
                    await Context.Message.AddReactionAsync(new Emoji("👌"));
                }
            }
        }

        [Command("warn")]
        public async Task WarnAsync(params ulong[] users)
        {
            if (RainBorg.Operators.Contains(Context.Message.Author.Id))
            {
                foreach (ulong user in users)
                    try
                    {
                        if (Context.Client.GetUser(user) != null && !RainBorg.Greylist.Contains(user))
                        {
                            EmbedBuilder builder = new EmbedBuilder();
                            builder.WithColor(Color.Green);
                            builder.WithTitle("SPAM WARNING");
                            builder.Description = RainBorg.Messages["spamWarning"];

                            RainBorg.Greylist.Add(user);
                            await RainBorg.RemoveUserAsync(Context.Client.GetUser(user), 0);
                            await Context.Client.GetUser(user).SendMessageAsync("", false, builder);
                        }
                    }
                    catch { }
                try
                {

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
