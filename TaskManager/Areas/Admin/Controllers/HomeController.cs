using System.Web.Mvc;
using TaskManager.Infrastructure;
using TaskManager.Authentification;

namespace TaskManager.Areas.Admin.Controllers
{
    [AdminAuthorize]
    public class HomeController : Controller
    {
        public ActionResult Index(string message = "")
        {
            ViewBag.message = message;
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

    }
}