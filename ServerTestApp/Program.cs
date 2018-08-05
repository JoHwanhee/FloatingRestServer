using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

            foreach (var num in Enumerable.Range(0, 10))
            {
                server.Add(new HelloWorldRouter(HttpMethod.Get, $"/hello/{num}", num.ToString()));
            }

            server.Add(new ImageTestRouter(HttpMethod.Get, $"/test/img"));
            server.Add(new PostTestRouter(HttpMethod.Post, $"/test/post"));

            server.Start();
            Console.ReadLine();
            server.Stop();
        }
    }
}
