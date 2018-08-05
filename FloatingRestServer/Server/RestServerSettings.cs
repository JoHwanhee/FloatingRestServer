using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FloatingRestServer.Common.Loggers;

namespace FloatingRestServer.Server
{
    public class RestServerSettings
    {
        public RestServerLogger Logger { get; set; } = LogManager.Instance.Logger;

        public string Schema { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public int Connections { get; set; }

        public RestServerSettings()
        {
            Schema = "http";
            Host = "localhost";
            Port = 123;
            Connections = 50;
        }
    }
}
