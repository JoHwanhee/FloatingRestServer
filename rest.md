## 서버  로직

1. 서버 생성

```C#
RestServer server = RestServer.Create(setting =>
            {
                setting.Logger = LogManager.Instance.Logger;
                setting.Schema = "http";
                setting.Host = "localhost";
                setting.Port = 8080;
                setting.Connections = 50;
            });
```

```C#
public static RestServer Create(Action<RestServerSettings> configure)
{
	var settings = new RestServerSettings();
	configure(settings);
	return new RestServer(settings);
}
```

```C#
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
```

2. 리퀘스트 처리

```C#
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
```

3. 라우트 처리

```C#
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
```

## 라우트 


```C#
public abstract class RouterCore : IRouter
{
	public HttpMethod Method { get; set; }
	public string Path { get; set; }

	protected RouterCore(HttpMethod method, string path)
	{
		Method = method;
		Path = path;
	}

	public abstract void Route(HttpListenerContext context);
}
```

```C#
public class ImageTestRouter : RouterCore
{
	public ImageTestRouter(HttpMethod method, string path) : base(method, path)
	{
	}

	public override async void Route(HttpListenerContext context)
	{
		context.Response.Headers.Add("Content-Type", "image/jpeg");
		
		try
		{
			byte[] buffer = null;
			using (FileStream fs = File.OpenRead("./test.jpg"))
			{
				buffer = new byte[fs.Length];
				await fs.ReadAsync(buffer, 0, (int)fs.Length);
			}
			context.Response.SendResponse(HttpStatusCode.OK, buffer, MediaTypeNames.Image.Jpeg);
		}
		catch (Exception e)
		{
			context.Response.SendResponse(HttpStatusCode.InternalServerError, e.ToString(), Encoding.UTF8);
		}
	}
}
```

## 사용 방법
```C#
RestServer server = RestServer.Create(setting =>
{
	setting.Logger = LogManager.Instance.Logger;
	setting.Schema = "http";
	setting.Host = "localhost";
	setting.Port = 8080;
	setting.Connections = 50;
});

// Runtime에 route 추가 가능
foreach (var num in Enumerable.Range(0, 10))
{
	server.Add(new HelloWorldRouter(HttpMethod.Get, $"/hello/{num}", num.ToString()));
}

server.Add(new ImageTestRouter(HttpMethod.Get, $"/test/img"));
server.Add(new PostTestRouter(HttpMethod.Post, $"/test/post"));

server.Start();
server.Add(new HelloWorldRouter(HttpMethod.Get, $"/hello/tttt", "tttt"));
Console.ReadLine();
server.Stop();
```
