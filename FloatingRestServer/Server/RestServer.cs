using System;
using System.Collections.Generic;
using System.Configuration;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FloatingRestServer.Common.Loggers;

namespace FloatingRestServer.Server
{
    public class RestServer : IRestServer, IDisposable
    {
        private static RestServerLogger Logger;
        private readonly UriBuilder _uriBuilder;

        private readonly HttpListener _listener;
        private readonly Thread _listening;

        public bool IsListening => _listener.IsListening;
        public string ListenerPrefix => _uriBuilder.ToString();

        public string Schema;
        public string Host;
        public int Port;
        public int Connections;

        public List<IRouter> RouterList = new List<IRouter>();

        public RestServer(RestServerSettings settings)
        {
            _listener = new HttpListener();
            _listening = new Thread(HandleRequests)
            {
                IsBackground = true
            };

            Logger = settings.Logger;

            Schema = settings.Schema;
            Host = settings.Host;
            Port = settings.Port;
            Connections = settings.Connections;

            _uriBuilder = new UriBuilder(Schema, Host, Port, "/");
        }

        public static RestServer Create(Action<RestServerSettings> configure)
        {
            var settings = new RestServerSettings();
            configure(settings);
            return new RestServer(settings);
        }

        public void Start()
        {
            Logger.Info("Start RestServer!");
            Logger.Info(ListenerPrefix);

            _listener.Prefixes.Clear();
            _listener.Prefixes.Add(ListenerPrefix);
            _listener.Start();
            _listening.Start();
        }

        private async void HandleRequests()
        {
            while (IsListening)
            {
                // todo : how to check cannot found route ?
                var context = await _listener.GetContextAsync();
                
                var successCount = 0;
                foreach (IRouter router in RouterList)
                {
                    successCount += Route(router, context);
                }
                
                if (successCount == 0)
                {
                    Route(new NotFoundRouter(), context);
                }
            }
        }
        
        private int Route(IRouter router, HttpListenerContext context)
        {
            if (router.Path == context.Request.RawUrl)
            {
                router.Route(context);

                return 1;
            }

            return 0;
        }

        public void Add(IRouter router)
        {
            RouterList.Add(router);
        }

        public void Remove(IRouter router)
        {
            RouterList.Remove(router);
        }

        public void Stop()
        {
            Logger.Info("Stop RestServer!");
            _listening.Join();
            _listener.Stop();
        }

        public void Dispose()
        {
            if (IsListening) Stop();
            ((IDisposable)_listener)?.Dispose();
        }
    }
}
