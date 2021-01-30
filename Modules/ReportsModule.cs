using Discord;
using Discord.Commands;
using System.Linq;
using System.Threading.Tasks;

using Netvir.Data;
using Netvir.Attributes;

namespace Netvir.Modules
{
    class ReportsModule : ModuleBase
    {
        [Command("set current report channel"), OwnerOnly]
        public async Task SetReportChannel([Remainder]string Chan)
        {
            ulong ChannelId = Context.Message.MentionedChannelIds.FirstOrDefault();

            ITextChannel Channel = (ITextChannel)await Context.Guild.GetChannelAsync(ChannelId);
            if (Globals.ReportChannels.ContainsKey(Context.Guild.Id)) {
                Globals.ReportChannels[Context.Guild.Id] = Channel;
            } else {
                Globals.ReportChannels.Add(Context.Guild.Id, Channel);
            }

            await ReplyAsync("Report channel successfully added");
        }
    }
}
