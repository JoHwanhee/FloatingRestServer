using System.Net;
using System.Net.Http;
using System.Text;
using FloatingRestServer.Common.Extension;
using FloatingRestServer.Server;

namespace ServerTestApp.WebService
{
    public class PostTestRouter : RouterCore
    {
        public PostTestRouter(HttpMethod method, string path) : base(method, path)
        {
        }

        public override void Route(HttpListenerContext context)
        {
            var body = context.Request.Body();
            context.Response.SendResponse(HttpStatusCode.OK, body, Encoding.UTF8);
        }
    }
}