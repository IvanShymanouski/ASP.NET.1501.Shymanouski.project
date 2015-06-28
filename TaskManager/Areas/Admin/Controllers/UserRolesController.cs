using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
using TaskManager.Infrastructure;
using BLL.Interfaces;
using TaskManager.Models;
using TaskManager.Providers;
using System.Text.RegularExpressions;
using TaskManager.Authentification;
using TaskManager.Authentification;

namespace TaskManager.Areas.Admin.Controllers
{
    [CustomAuthorize(Roles = RoleKeysNames.roleAdmin)]
    public class UserRolesController : Controller
    {
        IHasIdService<UserEntity> userService;

        public UserRolesController(IHasIdService<UserEntity> userService)
        {
            this.userService = userService;
        }

        public ActionResult Index(string message)
        {
            ViewBag.roles = RoleKeysNames.names;
            ViewBag.message = message;
            return View();            
        }

        #region roles of user
        public ActionResult SeeUserRoles(string message = "")
        {
            ViewBag.roles = TempData["roles"];
            ViewBag.login = TempData["login"];
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SeeUserRoles(string userLogin, string unesed = "")
        {
            TempData["roles"] = (new CustomRoleProvider()).GetRolesForUser(userLogin);
            TempData["login"] = userLogin;
            return RedirectToAction("SeeUserRoles");
        }       

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult SeeUserRolesAjax(string userLogin)
        {
            return Json((new CustomRoleProvider()).GetRolesForUser(userLogin), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Add user to roles
        public ActionResult AddUserToRoles()
        {
            ViewBag.Title = "Add user to roles";
            ViewBag.GetRolesAction = "AddUserToRoles";
            ViewBag.ApplyAction = "AddUserToRolesAction";
            ViewBag.login = TempData["login"];
            ViewBag.actionName = "TakeTheRoleDoesNotBelongToTheUserAjax";
            ViewBag.messsage = "Account have all roles";

            return View("UserRoles", TempData["roles"]);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUserToRoles(string userLogin)
        {
            TempData["login"] = userLogin;
            TempData["roles"] = TakeTheRoleDoesNotBelongToTheUser(userLogin);
            return RedirectToAction("AddUserToRoles");
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult TakeTheRoleDoesNotBelongToTheUserAjax(string userLogin)
        {
            return Json(TakeTheRoleDoesNotBelongToTheUser(userLogin), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUserToRolesAction(string userLogin, string[] roles)
        {
            (new CustomRoleProvider()).AddUsersToRoles(new string[] { userLogin }, roles);
            return RedirectToAction("Index", new { message = "New roles to "+userLogin+" added" });
        }
        #endregion

        #region Delete user from roles
        public ActionResult DeleteUserFromRoles()
        {
            ViewBag.Title = "Delete user from roles";
            ViewBag.GetRolesAction = "DeleteUserFromRoles";
            ViewBag.ApplyAction = "DeleteUserFromRolesAction";
            ViewBag.login = TempData["login"];
            ViewBag.actionName = "TakeUserRolesAjax";
            ViewBag.messsage = "Account have no roles";

            return View("UserRoles",TempData["roles"]);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteUserFromRoles(string userLogin)
        {
            TempData["login"] = userLogin;
            TempData["roles"] = (new CustomRoleProvider()).GetRolesForUser(userLogin);
            return RedirectToAction("DeleteUserFromRoles");
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult TakeUserRolesAjax(string userLogin)
        {
            return Json((new CustomRoleProvider()).GetRolesForUser(userLogin), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteUserFromRolesAction(string userLogin, string[] roles)
        {
            (new CustomRoleProvider()).RemoveUsersFromRoles(new string[] { userLogin }, roles);
            return RedirectToAction("Index", new { message = "Roles from "+userLogin+" deleted" });
        }
        #endregion

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetUsersAjax(string userLogin)
        {
            return Json(HelperFunctions.GetUsersAjax(userLogin, userService), JsonRequestBehavior.AllowGet);
        }        

        private IEnumerable<string> TakeTheRoleDoesNotBelongToTheUser(string userLogin)
        {
            CustomRoleProvider CRP = new CustomRoleProvider();
            string[] roles = CRP.GetRolesForUser(userLogin);

            return CRP.GetAllRoles().Where(x => !RoleInRoles(x, roles));
        }

        private bool RoleInRoles(string role, string[] roles)
        {
            bool inRoles = false;
            for (int i = 0; i < roles.Length; i++)
            {
                if (role == roles[i])
                {
                    inRoles = true;
                    break;
                }
            }

            return inRoles;
        }
    }
}
