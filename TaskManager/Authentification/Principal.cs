using System;
using System.Security.Principal;
using System.Linq;
using System.Collections.Generic;
using TaskManager.Providers;

namespace TaskManager.Authentification
{
    public class Principal : IPrincipal
    {
        private readonly Identity identity;

        public Principal(Identity identity)
        {
            this.identity = identity;
        }

        #region IPrincipal Members

        public bool IsInRole(string roleName)
        {
            return (new CustomRoleProvider()).IsUserInRole(identity.Email,roleName);
        }

        public IIdentity Identity
        {
            get { return identity; }
        }

        #endregion
    }
}