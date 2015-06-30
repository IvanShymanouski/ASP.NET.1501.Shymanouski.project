using TaskManager.Infrastructure;
using System.Collections.Generic;

namespace TaskManager.Authentification
{
    public class ManagerAuthorizeAttribute : CustomAuthorizeAttribute
    {
        public ManagerAuthorizeAttribute()
        {
            try
            {
                Roles = AreasAccess.Roles["Manager"];
                Users = AreasAccess.Users["Manager"];
            }
            catch (KeyNotFoundException)
            {
                //if (Roles == "") log else log
            }
        }
    }
}