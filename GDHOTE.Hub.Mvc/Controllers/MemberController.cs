using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GDHOTE.Hub.Core.Services;

namespace GDHOTE.Hub.Mvc.Controllers
{
    public class MemberController : Controller
    {
        // GET: Member
        public ActionResult Index()
        {
            var members = MemberService.GetMembers().ToList();
            return View(members);
        }
        public ActionResult New()
        {
            return View("MemberForm");
        }
        public ActionResult Edit(int id)
        {
            var member = MemberService.GetMember(id);
            return View("MemberForm", member);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save()
        {
            //Save Logic
            return RedirectToAction("Index", "State");
        }
    }
}