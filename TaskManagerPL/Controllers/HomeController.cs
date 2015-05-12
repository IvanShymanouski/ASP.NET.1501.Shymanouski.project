using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interfaces;

namespace TaskManagerPL.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService service;

        public HomeController(IUserService service)
        {
            this.service = service;
        }

        public ActionResult Index()
        {

            ViewBag.Title = "Index";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Title = "About";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Title = "Contact";
            return View();
        }
    }
}
