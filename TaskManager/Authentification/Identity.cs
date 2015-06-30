using System;
using System.Collections.Generic;
using System.Security.Principal;
using BLL.Interfaces;
using TaskManager.Providers;

namespace TaskManager.Authentification
{
    public class Identity : IIdentity
    {
        public Identity(IPrincipal user)
        {
            var current = (null == user)?null:user.Identity as Identity;
            if (current == null)
            {
                Login = "Guest";
                Roles = new string[] { "unauthorized" };
                return;
            }

            Id = current.Id;
            Login = current.Login;
            Email = current.Email;
            Roles = current.Roles ?? new string[] { "unauthorized" };
            RememberMe = current.RememberMe;
        }

        public Identity(UserEntity user)
        {            
            if (user == null)
            {
                Login = "Guest";
                Roles = new string[] { "unauthorized" };
                return;
            }

            Id = user.Id;
            Login = user.Login;
            Email = user.Email;
            Roles = CustomRoleProvider.GetRolesForUser(user.Email);
        }

        public Identity(Cookie user)
        {
            if (user == null)
            {
                Login = "Guest";
                Roles = new string[] { "unauthorized" };
                return;
            }

            Id = user.Id;
            Login = user.Login;
            Email = user.Email;
            Roles = user.Roles ?? new string[] { "unauthorized" };
        }

        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public bool RememberMe { get; set; }

        #region IIdentity Members

        public string AuthenticationType
        {
            get { return "SuperAuthen"; }
        }

        public bool IsAuthenticated
        {
          get { return !(Id == Guid.Empty || string.IsNullOrWhiteSpace(Email)); }
        }

        public string Name { get { return Login; } }

        #endregion
    }
}