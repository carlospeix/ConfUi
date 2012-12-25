using System.Web.Mvc;

namespace WebGeneric.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Bienvenido";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
