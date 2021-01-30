using System;
using System.Threading.Tasks;

using Netvir.Events;
using Netvir.Attributes;

namespace Netvir.Services
{
    [Service("Listener")]
    class HttpResponseProcess
    {
        public static async Task Dispatch(HttpMessageEventArgs e)
        {
            // TODO: Implement Dispatch w/ other services
        }

        public static Task StartingNotice()
        {
            Console.WriteLine("Start listening...");
            return Task.CompletedTask;
        }

        public static Task StopListeningNotice()
        {
            Console.WriteLine("I'm not sure about that...");
            return Task.CompletedTask;
        }
    }
}
