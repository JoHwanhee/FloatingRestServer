using System;
using System.Net;
using System.Text;
using FloatingRestServer.Server;

namespace ServerTestApp
{
    public class HelloWorldRouter : IRouter
    {
        public string Path { get; set; }
        public string Text { get; set; }

        public HelloWorldRouter(string path, string text)
        {
            Path = path;
            Text = text;
        }
        
        public HttpListenerResponse Route(HttpListenerContext context)
        {
            HttpListenerResponse response = context.Response;
            string responseString = $"<HTML><BODY> Hello world! {Text} </BODY></HTML>";
            byte[] buffer = Encoding.UTF8.GetBytes(responseString);
            response.ContentLength64 = buffer.Length;
            System.IO.Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            output.Close();
            response.Close();

            return response;
        }
    }
}
