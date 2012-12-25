using System;
using System.Web.Mvc;
using System.Diagnostics;

namespace WebGeneric.Config
{
    public class RequestTimingFilter : IActionFilter, IResultFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            GetTimer(filterContext, "action").Start();
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            GetTimer(filterContext, "action").Stop();
        }

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            GetTimer(filterContext, "render").Start();
        }

        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
            GetTimer(filterContext, "render").Stop();

            RenderResults(filterContext);
        }

        private void RenderResults(ResultExecutedContext filterContext)
        {
            var renderTimer = GetTimer(filterContext, "render");
            var actionTimer = GetTimer(filterContext, "action");

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

        private Stopwatch GetTimer(ControllerContext context, string name)
        {
            string key = "__timer__" + name;

            if (context.HttpContext.Items.Contains(key))
                return (Stopwatch)context.HttpContext.Items[key];

            var result = new Stopwatch();
            context.HttpContext.Items[key] = result;
            return result;
        }
    }
}