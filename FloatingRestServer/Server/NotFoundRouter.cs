using System.Net;
using System.Net.Http;
using System.Text;

namespace FloatingRestServer.Server
{
    public class NotFoundRouter : IRouter
    {
        public HttpMethod Method { get; set; }
        public string Path { get; set; }
        
        public void Route(HttpListenerContext context)
        {
            HttpListenerResponse response = context.Response;
            string responseString = $"<HTML><BODY> Cannot found {context.Request.RawUrl} </BODY></HTML>";
            byte[] buffer = Encoding.UTF8.GetBytes(responseString);
            response.ContentLength64 = buffer.Length;
            System.IO.Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            output.Close();
            response.Close();
        }
    }
}
