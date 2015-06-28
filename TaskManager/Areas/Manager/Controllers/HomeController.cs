using System.Web.Mvc;
using TaskManager.Infrastructure;
using TaskManager.Authentification;

namespace TaskManager.Areas.Manager.Controllers
{
    [CustomAuthorize(Roles = RoleKeysNames.roleManager)]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
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
