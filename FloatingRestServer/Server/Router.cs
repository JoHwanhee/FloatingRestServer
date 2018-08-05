using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FloatingRestServer.Server
{
    public class Router : IRouter
    {
        public void Route(object context)
        {
            throw new NotImplementedException();
        }

        // this is the test code, to vertify working well.
        public void Route(HttpListenerContext context)
        {
            HttpListenerResponse response = context.Response;
            string responseString = "<HTML><BODY> Hello world!</BODY></HTML>";
            byte[] buffer = Encoding.UTF8.GetBytes(responseString);
            response.ContentLength64 = buffer.Length;
            System.IO.Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            output.Close();
            response.Close();
        }
    }
}
