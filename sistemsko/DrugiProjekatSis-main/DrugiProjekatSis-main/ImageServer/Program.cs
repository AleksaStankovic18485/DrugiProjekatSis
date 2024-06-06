using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace ImageServer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:5050/");
            listener.Start();
            Console.WriteLine("Server started...");

            while (true)
            {
                // var context = listener.GetContext();
                HttpListenerContext context = await listener.GetContextAsync();
                ThreadPool.QueueUserWorkItem(o => RequestHandler.HandleRequestAsync(context));
            }
        }
    }
}
