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
        void Route(object context);
        void Route(HttpListenerContext context);
    }
}
