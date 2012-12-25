using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Centros.Web.Config;

namespace Centros.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            AppInitializer.Initialize();

            RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_End()
        {
            AppInitializer.Dispose();
        }

        private static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("favicon.ico");
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }, // Parameter defaults
                new[] { "Centros.Web.Controllers" }
            );
        }
    }
}