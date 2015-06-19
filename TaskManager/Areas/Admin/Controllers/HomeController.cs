using System.Web.Mvc;
using TaskManager.Infrastructure;

namespace TaskManager.Areas.Admin.Controllers
{
    [Authorize(Roles = RoleKeysNames.roleAdmin)]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {            
            return View();
        }

        public ActionResult About()
        {
            return RedirectToAction("About", "Home", new { area = "" });
        }

        public ActionResult Contact()
        {

            return RedirectToAction("Contact", "Home", new { area = "" });            
        }      
        
    }
}
