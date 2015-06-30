﻿using System;
using System.Web.Security;
using System.Web.Mvc;
using TaskManager.Infrastructure;
using TaskManager.Models;
using TaskManager.Providers;
using TaskManager.Authentification;

namespace TaskManager.Areas.Admin.Controllers
{
    [AdminAuthorize]
    public class CreationUserController : Controller
    {
        public ActionResult Index(string message = "")
        {
            ViewBag.roles = RoleKeys.names;
            ViewBag.message = message;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(RegisterModel model, string role = "")
        {
            ViewBag.roles = RoleKeys.names;

            ActionResult result = View(model);

            if (ModelState.IsValid)
            {
                if (null != CustomMembershipProvider.CreateUser(model.Login, model.Email, model.Password))
                {
                    if (role != String.Empty) CustomRoleProvider.AddUsersToRoles(new string[] { model.Login }, new string[] { role });
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