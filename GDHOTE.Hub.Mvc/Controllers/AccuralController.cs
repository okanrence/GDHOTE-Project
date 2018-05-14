using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GDHOTE.Hub.Mvc.Controllers
{
    public class AccuralController : Controller
    {
        // GET: Accural
        public ActionResult Index()
        {
            return View("AccuralIndex");
        }

        public ActionResult New()
        {
            return View();
        }
    }
}