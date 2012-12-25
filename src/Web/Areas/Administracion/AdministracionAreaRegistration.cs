using System.Web.Mvc;

namespace Centros.Web.Areas.Administracion
{
    public class AdministracionAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "Administracion"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Administracion_default",
                "Administracion/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new [] { "Centros.Web.Areas.Administracion.Controllers" }
            );
        }
    }
}
