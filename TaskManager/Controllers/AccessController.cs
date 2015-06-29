using System.Web.Mvc;
using TaskManager.Authentification;

namespace TaskManager.Controllers
{
    [CustomAuthorize]
    public class AccessController : Controller
    {        
        public ActionResult Denied()
        {
            return View();
        }
    }
}
