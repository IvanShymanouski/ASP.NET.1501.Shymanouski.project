using System.Web.Mvc;
using System.Web.Security;
using TaskManager.Infrastructure;
using BLL.Interfaces;

namespace TaskManager.Areas.Manager.Controllers
{
    [Authorize(Roles=RoleKeysNames.roleManager)]
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
