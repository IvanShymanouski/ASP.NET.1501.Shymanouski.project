using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TaskManagerPL.Areas.Admin.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        //
        // GET: /Admin/HomeAdmin/

        public ActionResult Index()
        {
            return View("~/Areas/Admin/Views/Home/Index.cshtml");
        }

        public ActionResult About()
        {
            return View("~/Areas/Admin/Views/Home/About.cshtml");
        }

        public ActionResult Contact()
        {
            return View("~/Areas/Admin/Views/Home/Contact.cshtml");
        }

    }
}
