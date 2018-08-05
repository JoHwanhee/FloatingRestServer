using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using FloatingRestServer.Common.Extension;
using FloatingRestServer.Server;

namespace ServerTestApp.WebService
{
    public class ImageTestRouter : RouterCore
    {
        public ImageTestRouter(HttpMethod method, string path) : base(method, path)
        {
        }

        public override async void Route(HttpListenerContext context)
        {
            context.Response.Headers.Add("Content-Type", "Content-Type: application/x-www-form-urlencoded");

            try
            {
                byte[] buffer = null;
                using (FileStream fs = File.OpenRead("./test.jpg"))
                {
                    buffer = new byte[fs.Length];
                    await fs.ReadAsync(buffer, 0, (int)fs.Length);
                }
                context.Response.SendResponse(HttpStatusCode.OK, buffer, MediaTypeNames.Image.Jpeg);
            }
            catch (Exception e)
            {
                context.Response.SendResponse(HttpStatusCode.InternalServerError, e.ToString(), Encoding.UTF8);
            }
        }
    }
}