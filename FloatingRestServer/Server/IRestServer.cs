using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloatingRestServer.Server
{
    internal interface IRestServer
    {
        bool IsListening { get; }
        string ListenerPrefix { get; }
        void Start();
        void Stop();
    }
}
