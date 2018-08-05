using System.Net;
using System.Net.Http;
using System.Text;
using FloatingRestServer.Common.Extension;
using FloatingRestServer.Server;

namespace FloatingRestServer.PredefinedRouters
{
    public class InternalServerErrorRouter : IRouter
    {
        public HttpMethod Method { get; set; }
        public string Path { get; set; }
        private string html = "<HTML><BODY> 500 Internal Server Error </BODY></HTML>";

        public void Route(HttpListenerContext context)
        {
            context?.Response.SendResponse(HttpStatusCode.InternalServerError, html, Encoding.UTF8);
        }
    }
}
