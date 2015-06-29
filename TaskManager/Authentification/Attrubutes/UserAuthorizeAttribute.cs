using TaskManager.Infrastructure;
using System.Collections.Generic;

namespace TaskManager.Authentification
{
    public class UserAuthorizeAttribute : CustomAuthorizeAttribute
    {
        public UserAuthorizeAttribute()
        {

            try
            {
                Roles = AreasAccess.Roles["User"];
                Users = AreasAccess.Users["User"];
            }
            catch (KeyNotFoundException ex)
            {
                //if (Roles == "") log else log
            }
        }
    }
}