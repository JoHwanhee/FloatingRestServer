using System.Net;
using System.Net.Http;

namespace FloatingRestServer.Server
{
    public abstract class RouterCore : IRouter
    {
        public HttpMethod Method { get; set; }
        public string Path { get; set; }

        protected RouterCore(HttpMethod method, string path)
        {
            Method = method;
            Path = path;
        }

        public abstract void Route(HttpListenerContext context);
    }
}