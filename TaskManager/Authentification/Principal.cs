using System.Security.Principal;
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
            return CustomRoleProvider.IsUserInRoles(identity,new string[] {roleName});
        }

        public IIdentity Identity
        {
            get { return identity; }
        }

        #endregion
    }
}