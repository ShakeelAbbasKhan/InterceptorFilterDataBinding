using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Diagnostics;

namespace InterceptorFilterDataBinding.Models
{
    public class ResponseInterceptorFilter : ActionFilterAttribute
    {
        private Stopwatch stopwatch;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            stopwatch.Stop();

            var response = context.HttpContext.Response;

            response.Headers.Add("Action-Name", context.RouteData.Values["action"] as string);
            response.Headers.Add("HTTP-Method", context.HttpContext.Request.Method);
            response.Headers.Add("HTTP-Scheme", context.HttpContext.Request.Scheme);
            response.Headers.Add("Host", context.HttpContext.Request.Host.Host);
            response.Headers.Add("Port", context.HttpContext.Request.Host.Port.ToString());
            response.Headers.Add("Time-Taken", stopwatch.ElapsedMilliseconds.ToString());
            response.Headers.Add("Server-Date-Time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }
}
