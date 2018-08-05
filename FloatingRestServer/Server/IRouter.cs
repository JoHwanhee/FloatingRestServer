using System.Net;
using System.Net.Http;

namespace FloatingRestServer.Server
{
    internal interface IRouter
    {
        HttpMethod Method { get; set; }
        string Path { get; set; }
        void Route(HttpListenerContext context);
    }
}
