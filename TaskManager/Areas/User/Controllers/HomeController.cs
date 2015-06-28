using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManager.Infrastructure;
using BLL.Interfaces;
using TaskManager.Authentification;

namespace TaskManager.Areas.User.Controllers
{
    [CustomAuthorize(Roles = RoleKeysNames.roleUser)]
    public class HomeController : Controller
    {
        public ActionResult Index(string message = "")
        {
            ViewBag.message = message;
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}
