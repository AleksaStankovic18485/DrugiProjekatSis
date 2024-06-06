using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace ImageServer
{
    public static class ResponseService
    {
        public static async Task SendResponseAsync(HttpListenerContext context, string filePath)
        {
            try
            {
                var imageBytes = await File.ReadAllBytesAsync(filePath);
                context.Response.ContentType = "image/png";
                context.Response.ContentLength64 = imageBytes.Length;
                await context.Response.OutputStream.WriteAsync(imageBytes, 0, imageBytes.Length);
                context.Response.OutputStream.Close();
                Console.WriteLine("Response sent");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending response: {ex.Message}");
            }
        }

        public static async Task SendErrorResponseAsync(HttpListenerContext context, string message)
        {
            try
            {
                context.Response.StatusCode = 404;
                byte[] responseBytes = System.Text.Encoding.UTF8.GetBytes(message);
                context.Response.ContentType = "text/plain";
                context.Response.ContentLength64 = responseBytes.Length;
                await context.Response.OutputStream.WriteAsync(responseBytes, 0, responseBytes.Length);
                context.Response.OutputStream.Close();
                Console.WriteLine("Error response sent");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending error response: {ex.Message}");
            }
        }
    }
}
