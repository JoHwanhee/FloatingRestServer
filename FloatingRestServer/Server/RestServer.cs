using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FloatingRestServer.Common.Loggers;

namespace FloatingRestServer.Server
{
    public class RestServer : IRestServer
    {
        private ILogger Logger = LogManager.Instance.Logger;

        public bool IsListening { get; }
        public string ListenerPrefix { get; }

        public void Start()
        {
            Logger.Info("Start RestServer!");
        }

        public void Stop()
        {
            Logger.Info("Stop RestServer!");
        }
    }
}
