using System.Net;
using System.Net.Mime;
using System.Text;

namespace FloatingRestServer.Common.Extension
{
    public static class HttpListenerContextExtension
    {
        public static void SendResponse(this HttpListenerContext context, HttpStatusCode statusCode, string responseText, Encoding encoding)
        {
            byte[] buffer = encoding.GetBytes(responseText);

            using (var response = context.Response)
            {
                response.StatusCode = (int)statusCode;
                response.ContentLength64 = buffer.Length;
                
                using (var output = response.OutputStream)
                {
                    output.Write(buffer, 0, buffer.Length);
                }
            }
        }

        public static void SendResponse(this HttpListenerContext context, HttpStatusCode statusCode, byte[] bytes, string contentType)
        {
            byte[] buffer = bytes;
            context.Response.AddHeader("Content-Type", contentType);
            using (var response = context.Response)
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