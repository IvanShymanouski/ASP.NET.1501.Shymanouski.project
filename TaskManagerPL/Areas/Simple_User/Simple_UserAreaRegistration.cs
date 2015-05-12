using System.Web.Mvc;

namespace TaskManagerPL.Areas.Simple_User
{
    public class Simple_UserAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Simple_User";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Simple_User_default",
                "Simple_User/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
