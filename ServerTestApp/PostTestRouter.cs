using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using FloatingRestServer.Common.Extension;
using FloatingRestServer.Common.Loggers;
using FloatingRestServer.Server;

namespace ServerTestApp
{
    public class PostTestRouter : RouterCore
    {
        private RestServerLogger Logger = LogManager.Instance.Logger;
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