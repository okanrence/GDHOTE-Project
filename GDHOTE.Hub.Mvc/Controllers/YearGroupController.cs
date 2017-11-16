using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GDHOTE.Hub.Core.Services;

namespace GDHOTE.Hub.Mvc.Controllers
{
    public class YearGroupController : Controller
    {
        // GET: YearGroup
        public ActionResult Index()
        {
            var yearGroup = YearGroupService.GetYearGroups().ToList();
            return View(yearGroup);
        }
    }
}