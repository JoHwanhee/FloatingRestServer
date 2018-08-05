using System.Net;
using System.Net.Http;
using System.Text;
using FloatingRestServer.Common.Extension;
using FloatingRestServer.Server;

namespace FloatingRestServer.PredefinedRouters
{
    public class NotFoundRouter : IRouter
    {
        public HttpMethod Method { get; set; }
        public string Path { get; set; }
        private string html = "<HTML><BODY> 404 Not found {0} {1} </BODY></HTML>";

        public void Route(HttpListenerContext context)
        {
            var htmlText = string.Format(html, context.Request.HttpMethod, context.Request.RawUrl);
            context?.Response.SendResponse(HttpStatusCode.NotFound, htmlText, Encoding.UTF8);   
        }
    }
}
