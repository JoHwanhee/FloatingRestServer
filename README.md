# FloatingRestServer - Simple .NET REST Server
A simple way to use Rest Server In C#

## Features

1. No dependency.
2. Create route in run time.
3. Easily to add rest resource.

## How to use

#### 0. Make the routes

~~~C#
public class PostTestRouter : RouterCore
{
    public PostTestRouter(HttpMethod method, string path) : base(method, path)
    {
    }

    public override void Route(HttpListenerContext context)
    {
        var body = context.Request.Body();
        context.Response.SendResponse(HttpStatusCode.OK, body, Encoding.UTF8);
    }
}
~~~

#### 1. Initialize Rest Server
~~~C#
RestServer server = RestServer.Create(setting =>
{
    setting.Logger = LogManager.Instance.Logger;
    setting.Schema = "http";
    setting.Host = "localhost";
    setting.Port = 1234;
    setting.Connections = 50;
});
~~~

#### 2. Add Route
~~~C#
foreach (var num in Enumerable.Range(0, 10))
{
    server.Add(new HelloWorldRouter(HttpMethod.Get, $"/hello/{num}", num.ToString()));
}
server.Add(new ImageTestRouter(HttpMethod.Get, $"/test/img"));
server.Add(new PostTestRouter(HttpMethod.Post, $"/test/post"));
~~~

#### 3. Start Server
~~~C#
server.Start();
~~~

#### 4. Add route in run-time
~~~C#
server.Add(new HelloWorldRouter(HttpMethod.Get, $"/hello/tttt", "tttt"));
~~~

#### 5. Stop server
~~~C#
server.Stop();
~~~


