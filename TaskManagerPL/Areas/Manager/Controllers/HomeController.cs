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

        private readonly IHasIdService<UserEntity> userService;
        private readonly IHasIdService<RoleEntity> roleService;

        public HomeController(IHasIdService<RoleEntity> roleService,IHasIdService<UserEntity> userService)
        {
            this.roleService = roleService;
            this.userService = userService;
        }

        public ActionResult Index()
        {
            ViewBag.Users = userService.GetAll().Where((x)=>x.RoleId==Guid.Empty);
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
