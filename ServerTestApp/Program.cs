using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FloatingRestServer.Common.Loggers;
using FloatingRestServer.Server;

namespace ServerTestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            RestServer server = RestServer.Create(setting =>
            {
                setting.Logger = LogManager.Instance.Logger;
                setting.Schema = "http";
                setting.Host = "localhost";
                setting.Port = 1234;
                setting.Connections = 50;
            });

            server.Start();
            Console.ReadLine();
            server.Stop();
        }
    }
}
