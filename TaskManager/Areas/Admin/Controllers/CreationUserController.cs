using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using System.Web.Mvc;
using TaskManager.Infrastructure;
using BLL.Interfaces;
using TaskManager.Models;
using TaskManager.Providers;

namespace TaskManager.Areas.Admin.Controllers
{
    [Authorize(Roles = RoleKeysNames.roleAdmin)]
    public class CreationUserController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.roles = RoleKeysNames.names;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(RegisterModel model, string role)
        {
            ViewBag.roles = RoleKeysNames.names;
            ActionResult result = View(model);

            if (ModelState.IsValid)
            {
                if (null != ((CustomMembershipProvider)Membership.Provider).CreateUser(model.Login, model.Email, model.Password))
                {
                    result = RedirectToAction("ReplaseRole", "UserRoles" , new { role = role, user = model.Login });
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
