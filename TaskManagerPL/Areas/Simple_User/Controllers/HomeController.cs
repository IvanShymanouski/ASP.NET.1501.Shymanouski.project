using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interfaces;

namespace TaskManagerPL.Areas.Simple_User.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        //
        // GET: /Simple_User/HomeUser/

        private readonly IService<UserEntity> userService;
        private readonly IService<RoleEntity> roleService;

        public HomeController(IService<RoleEntity> roleService, IService<UserEntity> userService)
        {
            this.roleService = roleService;
            this.userService = userService;
        }

        public ActionResult Index()
        {
            ViewBag.Users = userService.GetAll();
            return View("~/Areas/Simple_User/Views/Home/Index.cshtml");
        } 
        public ActionResult About()
        {
            return View("~/Areas/Simple_User/Views/Home/About.cshtml");
        }

        public ActionResult Contact()
        {
            return View("~/Areas/Simple_User/Views/Home/Contact.cshtml");
        }
    }
}
