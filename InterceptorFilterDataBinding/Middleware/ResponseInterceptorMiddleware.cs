using System.Diagnostics;

namespace InterceptorFilterDataBinding.Middleware
{
    public class ResponseInterceptorMiddleware
    {
        private readonly RequestDelegate _next;

        public ResponseInterceptorMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            context.Response.OnStarting(() =>
            {
                stopwatch.Stop();
                context.Response.Headers.Add("Action-Name", context.Request.RouteValues["action"] as string);
                context.Response.Headers.Add("HTTP-Method", context.Request.Method);
                context.Response.Headers.Add("HTTP-Scheme", context.Request.Scheme);
                context.Response.Headers.Add("Host", context.Request.Host.Host);
                context.Response.Headers.Add("Port", context.Request.Host.Port.ToString());
                context.Response.Headers.Add("Time-Taken", stopwatch.ElapsedMilliseconds.ToString());
                context.Response.Headers.Add("Server-Date-Time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                return Task.CompletedTask;
            });

            await _next(context);
        }
    }
}
