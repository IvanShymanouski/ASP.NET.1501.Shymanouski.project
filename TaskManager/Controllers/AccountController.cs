using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using TaskManager.Models;
using TaskManager.Providers;
using BLL.Interfaces;
using TaskManager.Infrastructure;
using System.Web;
using System.Web.Routing;
using TaskManager.Authentification;

namespace TaskManager.Controllers
{
    [CustomAuthorize]
    public class AccountController : Controller
    {
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            ActionResult result = View(model);
            if (ModelState.IsValid)
            {
                UserEntity user = (new CustomMembershipProvider()).ValidateUserAndReturn(model.EmailOrLogin, model.Password);
                if (null != user)
                {
                    var setCockie = DependencyResolver.Current.GetService<IFormsAuthenticationService>();
                    setCockie.SignIn(new Identity(user), model.RememberMe);
                    result = RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            ActionResult result = View(model);

            if (ModelState.IsValid)
            {
                UserEntity user = (new CustomMembershipProvider()).CreateUser(model.Login, model.Email, model.Password);
                if (null != user)
                {
                    (new CustomRoleProvider()).AddUsersToRoles(new string[] { model.Login }, new string[] { RoleKeysNames.roleUser });
                    var setCockie = DependencyResolver.Current.GetService<IFormsAuthenticationService>();
                    setCockie.SignIn(new Identity(user), false);
                    result = RedirectToAction("Index", "Home", new { area = RoleKeysNames.roleUser });
                }
                else
                {
                    ModelState.AddModelError("", "Registration error, this login or email already exist");
                }
            }

            return result;
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
    }   
}
