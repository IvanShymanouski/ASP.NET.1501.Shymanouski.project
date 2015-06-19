using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using System.Web.Mvc;
using TaskManager.Infrastructure;
using BLL.Interfaces;
using TaskManager.Models;
using TaskManager.Providers;

namespace TaskManager.Areas.Admin.Controllers
{
    [Authorize(Roles = RoleKeysNames.roleAdmin)]
    public class UserRolesController : Controller
    {
        IHasIdService<UserEntity> userService;

        public UserRolesController(IHasIdService<UserEntity> userService)
        {
            this.userService = userService;
        }


        public ActionResult Index()
        {
            ViewBag.roles = RoleKeysNames.names;
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetUsersAjax(string RoleName)
        {
            List<UserModel> jsondata = new List<UserModel>(0);

            foreach (var name in (new CustomRoleProvider()).GetUsersInRole(RoleName))
            {
                jsondata.Add(new UserModel { Login = name, Rolename = RoleName });
            }

            return Json(jsondata, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetUsers(string RoleName)
        {   
            List<UserModel> list = new List<UserModel>(0);
            foreach (var name in (new CustomRoleProvider()).GetUsersInRole(RoleName))
            {
                list.Add(new UserModel { Login = name, Rolename = RoleName });
            }

            return View(list);
        }

        public ActionResult ReplaseRole(string user, string role)
        {
            UserEntity userForReplace = userService.Find(x => x.Login == user);            
            int ind = 0;
            while (RoleKeysNames.names[ind] != role) ind++;
            if (RoleKeysNames.keys[ind] != userForReplace.RoleId)
            {
                userForReplace.RoleId = RoleKeysNames.keys[ind];
                userService.Edit(userForReplace);
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult DeleteUser(string user)
        {
            userService.Delete(userService.Find(x => x.Login == user));
            
            return RedirectToAction("Index", "Home");
        }
    }
}
