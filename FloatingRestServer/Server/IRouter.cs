using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FloatingRestServer.Server
{
    public interface IRouter
    {
        string Path { get; set; }

        HttpListenerResponse Route(HttpListenerContext context);
    }
}
