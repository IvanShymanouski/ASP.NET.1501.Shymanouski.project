﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace TaskManagerPL.Areas.Manager.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        //
        // GET: /Manager/Account/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {

            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}
