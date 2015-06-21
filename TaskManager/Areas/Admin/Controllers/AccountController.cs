using System.Web.Mvc;
using System.Web.Security;
using TaskManager.Infrastructure;

namespace TaskManager.Areas.Admin.Controllers
{
    [Authorize(Roles=RoleKeysNames.roleAdmin)]
    public class AccountController : Controller
    {
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}
