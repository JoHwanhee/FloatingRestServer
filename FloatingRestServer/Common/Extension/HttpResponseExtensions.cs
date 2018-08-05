using System.Net;
using System.Net.Mime;
using System.Text;

namespace FloatingRestServer.Common.Extension
{
    public static class HttpResponseExtensions
    {
        public static void SendResponse(this HttpListenerResponse response, HttpStatusCode statusCode, string responseText, Encoding encoding)
        {
            byte[] buffer = encoding.GetBytes(responseText);

            using (response)
            {
                response.StatusCode = (int)statusCode;
                response.ContentLength64 = buffer.Length;
                
                using (var output = response.OutputStream)
                {
                    output.Write(buffer, 0, buffer.Length);
                }
            }
        }

        public static void SendResponse(this HttpListenerResponse response, HttpStatusCode statusCode, byte[] bytes, string contentType)
        {
            byte[] buffer = bytes;
            response.AddHeader("Content-Type", contentType);
            using (response)
            {
                response.StatusCode = (int)statusCode;
                response.ContentLength64 = buffer.Length;

                using (var output = response.OutputStream)
                {
                    output.Write(buffer, 0, buffer.Length);
                }
            }
        }
    }
}