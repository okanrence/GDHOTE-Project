using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GDHOTE.Hub.Core.Services;

namespace GDHOTE.Hub.Mvc.Controllers
{
    public class StateController : Controller
    {
        // GET: State
        public ActionResult Index()
        {
            var states = StateService.GetStates().ToList();
            return View(states);
        }
    }
}