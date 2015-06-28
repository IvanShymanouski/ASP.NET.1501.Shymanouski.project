﻿using System.Web.Mvc;
using System.Web.Security;
using TaskManager.Infrastructure;
using TaskManager.Authentification;

namespace TaskManager.Areas.Manager.Controllers
{
    [CustomAuthorize(Roles=RoleKeysNames.roleManager)]
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
