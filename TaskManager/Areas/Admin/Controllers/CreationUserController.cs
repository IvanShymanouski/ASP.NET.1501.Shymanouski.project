using System;
using System.Web.Security;
using System.Web.Mvc;
using TaskManager.Infrastructure;
using TaskManager.Models;
using TaskManager.Providers;
using TaskManager.Authentification;

namespace TaskManager.Areas.Admin.Controllers
{
    [CustomAuthorize(Roles = RoleKeysNames.roleAdmin)]
    public class CreationUserController : Controller
    {
        public ActionResult Index(string message = "")
        {
            ViewBag.roles = RoleKeysNames.names;
            ViewBag.message = message;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(RegisterModel model, string role="")
        {
            ViewBag.roles = RoleKeysNames.names;

            ActionResult result = View(model);

            if (ModelState.IsValid)
            {
                if (null != ((CustomMembershipProvider)Membership.Provider).CreateUser(model.Login, model.Email, model.Password))
                {
                    if (role!=String.Empty) (new CustomRoleProvider()).AddUsersToRoles(new string[] { model.Login }, new string[] { role });
                    result = RedirectToAction("Index", new { message = "Account " + model.Login + " create" });
                }
                else
                {
                    ModelState.AddModelError("", "Registration error, this login or email already exist");
                }
            }

            return result;
        }
    }
}
