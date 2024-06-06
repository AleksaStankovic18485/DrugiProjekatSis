using System;
using System.Net;
using System.Threading.Tasks;

namespace ImageServer
{
    public static class RequestHandler
    {
        public static async void HandleRequestAsync(HttpListenerContext context)
        {
            string fileName = context.Request.Url.LocalPath.TrimStart('/');
            Console.WriteLine($"Received request for {fileName}");

            if (string.IsNullOrEmpty( fileName ) )
            {
                await ResponseService.SendErrorResponseAsync(context, "Molim, navedite ime slike.");
                return;
            }

            string rootPath = AppDomain.CurrentDomain.BaseDirectory;
            string imagesPath = System.IO.Path.Combine(rootPath, "Images");

            string filePath = await Task.Run(() => FileSearchService.SearchFileRecursively(imagesPath, fileName));

            if (filePath != null && System.IO.File.Exists(filePath))
            {
                await ResponseService.SendResponseAsync(context, filePath);
            }
            else
            {
                await ResponseService.SendErrorResponseAsync(context, "Nepostoji slika");
            }
        }
    }
}
