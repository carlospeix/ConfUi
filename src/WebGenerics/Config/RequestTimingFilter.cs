using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Diagnostics;

namespace WebGeneric.Config
{
    public class RequestTimingFilter : IActionFilter, IResultFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            GetTimer(filterContext.HttpContext, "action").Start();
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            GetTimer(filterContext.HttpContext, "action").Stop();
        }

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            GetTimer(filterContext.HttpContext, "render").Start();
        }

        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
            GetTimer(filterContext.HttpContext, "render").Stop();

            RenderResults(filterContext);
        }

        private void RenderResults(ResultExecutedContext filterContext)
        {
            var renderTimer = GetTimer(filterContext.HttpContext, "render");
            var actionTimer = GetTimer(filterContext.HttpContext, "action");

            var response = filterContext.HttpContext.Response;

            if (response.ContentType == "text/html")
            {
                response.Write(
                    String.Format(
                        "<p>Action '{0}.{1}', Execute: {2}ms, Render: {3}ms.</p>",
                        filterContext.RouteData.Values["controller"],
                        filterContext.RouteData.Values["action"],
                        actionTimer.ElapsedMilliseconds,
                        renderTimer.ElapsedMilliseconds
                    )
                );
            }
        }

        private Stopwatch GetTimer(HttpContext context, string name)
        {
            string key = "__timer__" + name;

            if (context.Items.ContainsKey(key))
                return (Stopwatch)context.Items[key];

            var result = new Stopwatch();
            context.Items[key] = result;
            return result;
        }
    }
}