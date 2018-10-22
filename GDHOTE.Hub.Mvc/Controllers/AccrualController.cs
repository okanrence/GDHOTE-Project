using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GDHOTE.Hub.Mvc.Controllers
{
    public class AccrualController : BaseController
    {
        // GET: Accrual
        public ActionResult Index()
        {
            return View("AccrualIndex");
        }

        public ActionResult New()
        {
            return View();
        }
    }
}