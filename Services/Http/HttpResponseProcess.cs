using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Netvir.Events;

namespace Netvir.Services
{
    class HttpResponseProcess
    {
        public static void PrintReceivedMessage(object sender, HttpMessageEventArgs e)
        {
            Console.WriteLine(e.Request.RawUrl);

            string ResponseString = "Valid response required for testing";
            byte[] Buffer = Encoding.UTF8.GetBytes(ResponseString);

            e.Response.ContentLength64 = Buffer.Length;
            Stream Output = e.Response.OutputStream;
            Output.Write(Buffer, 0, Buffer.Length);

            Output.Close();
        }

        public static void StartingNotice(object sender, EventArgs e)
        {
            Console.WriteLine("Start listening...");
        }

        public static void StopListeningNotice(object sender, EventArgs e)
        {
            Console.WriteLine("I'm not sure about that...");
        }
    }
}
