using System.Web.Mvc;
using TaskManager.Infrastructure;

namespace TaskManager.Areas.Manager.Controllers
{
    [Authorize(Roles = RoleKeysNames.roleManager)]
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
