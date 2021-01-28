using System.Threading.Tasks;

using Discord.Commands;


namespace Netvir.Modules
{
    public class CommunicationModule : ModuleBase
    {
        [Command("who are you?")]
        public async Task TellWhoYouAre()
        {
            await ReplyAsync("Hi! I'm Netvir, here to administrate Shaps's online applications, plus some other stuff");
        }
    }
}
