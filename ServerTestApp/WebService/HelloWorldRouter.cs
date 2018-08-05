using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using FloatingRestServer.Common.Extension;
using FloatingRestServer.Server;

namespace ServerTestApp.WebService
{
    public class HelloWorldRouter : RouterCore
    {
        public string Text { get; set; }
        public HelloWorldRouter(HttpMethod method, string path, string text) : base(method, path)
        {
            Text = text;
        }

        public override void Route(HttpListenerContext context)
        {
            context.Response.SendResponse(HttpStatusCode.OK, $"HelloWorld {Text}", Encoding.UTF8);
        }
    }
}