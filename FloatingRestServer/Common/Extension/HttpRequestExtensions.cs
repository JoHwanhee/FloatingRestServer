using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using FloatingRestServer.Server;

namespace FloatingRestServer.Common.Extension
{
    public static class HttpRequestExtensions
    {
        public static string Body(this HttpListenerRequest request)
        {
            string text;
            using (var reader = new StreamReader(request.InputStream, request.ContentEncoding))
            {
                text = reader.ReadToEnd();
            }

            return text;
        }

        public static bool AnyUrl(this HttpListenerRequest request, List<RouterCore> routerList)
        {
            return routerList.Any(
                x =>
                {
                    var existUrl = x.Path == request.RawUrl;
                    var existMethod = x.Method.Method == request.HttpMethod;
                    return existUrl && existMethod;
                });
        }

        public static RouterCore FindRouter(this HttpListenerRequest request, List<RouterCore> routerList)
        {
            var router = routerList.Find(x =>
            {
                var existUrl = x.Path == request.RawUrl;
                var existMethod = x.Method.Method == request.HttpMethod;
                return existUrl && existMethod;
            });

            return router;
        }
    }
}