using System.Web.Mvc;
using System.Web.Security;
using TaskManager.Infrastructure;
using TaskManager.Authentification;

namespace TaskManager.Areas.Admin.Controllers
{
    [CustomAuthorize(Roles=RoleKeysNames.roleAdmin)]
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
