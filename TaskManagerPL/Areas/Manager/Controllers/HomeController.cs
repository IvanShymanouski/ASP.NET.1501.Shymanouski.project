using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interfaces;

namespace TaskManagerPL.Areas.Manager.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        //
        // GET: /Manager/Manager/

        private readonly IUserService userService;
        private readonly IRoleService roleService;

        public HomeController(IRoleService roleService, IUserService userService)
        {
            this.roleService = roleService;
            this.userService = userService;
        }

        public ActionResult Index()
        {
            ViewBag.Users = userService.GetAll().Where((x)=>x.RoleId==2);
            return View("~/Areas/Manager/Views/Home/Index.cshtml");
        }

        public ActionResult About()
        {
            return View("~/Areas/Manager/Views/Home/About.cshtml");
        }

        public ActionResult Contact()
        {
            return View("~/Areas/Manager/Views/Home/Contact.cshtml");
        }

    }
}
