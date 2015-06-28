using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;
using System.Web.Security;

namespace TaskManager.Authentification
{
    public class CustomAuthenticationService : IFormsAuthenticationService
    {
        private readonly HttpContextBase _httpContext;

        public CustomAuthenticationService(HttpContextBase httpContext)
        {
            _httpContext = httpContext;
        }

        #region IFormsAuthenticationService Members

        public void SignIn(IIdentity identity, bool createPersistentCookie)
        {
            var user = identity as Identity;
            if (user == null)
                throw new ArgumentNullException("user");

            var cookie = new Cookie
            {
                Id = user.Id,
                Login = user.Login,
                Email = user.Email,
                RememberMe = createPersistentCookie,
                Roles = user.Roles ?? new string[] { "unauthorized" }
            };

            string userData = JsonConvert.SerializeObject(cookie);
            var ticket = new FormsAuthenticationTicket(1, cookie.Login, DateTime.Now,
                                                       DateTime.Now.Add(FormsAuthentication.Timeout),
                                                       createPersistentCookie, userData);
            string encTicket = FormsAuthentication.Encrypt(ticket);
            var httpCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket) { Expires = DateTime.Now.Add(FormsAuthentication.Timeout) };

            _httpContext.Response.Cookies.Add(httpCookie);
        }

        public void SignOut()
        {
            // Not worth covering, has been tested by Microsoft
            FormsAuthentication.SignOut();
        }

        #endregion
    }


    public interface IFormsAuthenticationService                     
    {
        void SignIn(IIdentity user, bool createPersistentCookie);        
        void SignOut();
    }
}