using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Discord.Commands;

using Netvir.Attributes;
using Netvir.Data;

namespace Netvir.Modules
{
    class AdministrationModule : ModuleBase
    {
        [Command("check service availability"), OwnerOnly]
        public async Task CheckAvailability([Remainder]string Service)
        {
            if (!Globals.ServiceStatus.ContainsKey(Service)) {
                await ReplyAsync("This service is not registered");
                return;
            }

            await ReplyAsync(
                Globals.ServiceStatus[Service] ?
                "This service is available" : 
                $"This service is unavailable: {Globals.ServiceThrowReasons[Service]}");
        }
    }
}
