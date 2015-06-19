using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManager.Infrastructure;
using BLL.Interfaces;

namespace TaskManager.Areas.User.Controllers
{
    [Authorize(Roles = RoleKeysNames.roleUser)]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return RedirectToAction("About", "Home", new { area = "" });
        }

        public ActionResult Contact()
        {
            return RedirectToAction("Contact", "Home", new { area = "" });
        }
    }
}
