using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using System.Web.Mvc;
using TaskManager.Infrastructure;
using TaskManager.Models;
using TaskManager.Providers;
using System.Text.RegularExpressions;
using BLL.Interfaces;

namespace TaskManager.Areas.Admin.Controllers
{
    [Authorize(Roles = RoleKeysNames.roleAdmin)]
    public class DeleteUserController : Controller
    {
        IHasIdService<UserEntity> userService;

        public DeleteUserController(IHasIdService<UserEntity> userService)
        {
            this.userService = userService;
        }

        public ActionResult Index(string message = "")
        {
            ViewBag.roles = RoleKeysNames.names;
            ViewBag.message = message;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string userLogin,string role)
        {
            string message = "User " + userLogin + " not found";

            var user = userService.Find(x => x.Login == userLogin);
            if (null != user)
            {
                userService.Delete(user);
                message = "User " + userLogin + " deleted";
            }

            return RedirectToAction("Index", new { message =  message});
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetUsersAjax(string userLogin, string roleName)
        {
            return Json(HelperFunctions.GetUsersAjax(userLogin,userService,roleName), JsonRequestBehavior.AllowGet);
        }
    }
}
