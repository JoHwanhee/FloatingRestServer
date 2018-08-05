using System;
using System.Collections.Generic;
using System.Configuration;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FloatingRestServer.Common.Extension;
using FloatingRestServer.Common.Loggers;
using FloatingRestServer.PredefinedRouters;

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

        public List<RouterCore> RouterList = new List<RouterCore>();

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

        private void HandleRequests()
        {
            while (IsListening)
            {
                var context = _listener.BeginGetContext(ContextReady, null);
                WaitHandle.WaitAny(new[] {context.AsyncWaitHandle});
            }
        }

        private void ContextReady(IAsyncResult asyncResult)
        {
            HttpListenerContext context = null;
            try
            {
                context = _listener.EndGetContext(asyncResult);
                var request = context.Request;

                if (request.AnyUrl(RouterList))
                {
                    var router = request.FindRouter(RouterList);
                    Route(router, context);
                }
                else
                {
                    new NotFoundRouter().Route(context);
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
                new InternalServerErrorRouter().Route(context);
            }
        }
        
        private void Route(IRouter router, HttpListenerContext context)
        {
            if (router.Method.ToString() != context.Request.HttpMethod)
            {
                return;
            }

            if (router.Path != context.Request.RawUrl)
            {
                return;
            }

            router.Route(context);
        }

        public void Add(RouterCore router)
        {
            Logger.Trace($"Add route {router.Path}.");
            RouterList.Add(router);
        }

        public void Remove(RouterCore router)
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
