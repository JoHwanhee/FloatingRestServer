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
        private Thread Listening;

        public bool IsListening => _listener.IsListening;
        public string ListenerPrefix => _uriBuilder.ToString();

        public string Schema;
        public string Host;
        public int Port;
        public int Connections;

        private IRouter Router;

        public RestServer(RestServerSettings settings)
        {
            _listener = new HttpListener();
            Listening = new Thread(HandleRequests);
            Listening.IsBackground = true;

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
            Listening.Start();
        }

        private void HandleRequests()
        {
            while (IsListening)
            {
                var context = _listener.BeginGetContext(ContextReady, null);
                WaitHandle.WaitAll(new[] {context.AsyncWaitHandle});
            }
        }

        private void ContextReady(IAsyncResult result)
        {
            Route(_listener.EndGetContext(result));
        }

        // todo : test code, to make IRoute class
        private void Route(HttpListenerContext context)
        {
            HttpListenerResponse response = context.Response;
            string responseString = "<HTML><BODY> Hello world!</BODY></HTML>";
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            response.ContentLength64 = buffer.Length;
            System.IO.Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            output.Close();
        }

        public void Stop()
        {
            Logger.Info("Stop RestServer!");
            Listening.Join();
            _listener.Stop();
        }

        public void Dispose()
        {
            if (IsListening) Stop();
            ((IDisposable)_listener)?.Dispose();
        }
    }
}
