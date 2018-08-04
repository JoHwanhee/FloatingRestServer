using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FloatingRestServer.Server;

namespace ServerTestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            RestServer server = new RestServer();
            server.Start();
            server.Stop();

            Console.ReadLine();
        }
    }
}
