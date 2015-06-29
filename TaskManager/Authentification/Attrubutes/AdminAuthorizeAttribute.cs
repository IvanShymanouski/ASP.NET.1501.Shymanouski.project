using TaskManager.Infrastructure;
using System.Collections.Generic;


namespace TaskManager.Authentification
{
    public class AdminAuthorizeAttribute : CustomAuthorizeAttribute
    {
        public AdminAuthorizeAttribute()
        {
            try
            {
                Roles = AreasAccess.Roles["Admin"];
                Users = AreasAccess.Users["Admin"];
            }
            catch (KeyNotFoundException ex)
            {
                //if (Roles == "") log else log
            }
        }
    }
}