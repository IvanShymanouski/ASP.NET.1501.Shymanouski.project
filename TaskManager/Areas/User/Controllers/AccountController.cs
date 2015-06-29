using System.Web.Mvc;
using System.Web.Security;
using TaskManager.Infrastructure;
using BLL.Interfaces;
using TaskManager.Authentification;

namespace TaskManager.Areas.User.Controllers
{
    [UserAuthorize]
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
