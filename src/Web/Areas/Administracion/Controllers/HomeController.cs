using System.Web.Mvc;

namespace Centros.Web.Areas.Administracion.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}