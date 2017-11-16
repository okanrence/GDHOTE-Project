using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GDHOTE.Hub.Core.Models;
using GDHOTE.Hub.Core.Services;

namespace GDHOTE.Hub.Mvc.Controllers
{
    public class MembershipStatusController : Controller
    {
        // GET: MembershipStatus
        public ActionResult Index()
        {
            var membershipStatuses = MembershipStatusService.GetCMembershipStatuses().ToList();
            return View(membershipStatuses);
        }
        public ActionResult New()
        {
            var viewModel = new MembershipStatus();
            return View("MembershipStatusForm", viewModel);
        }
        public ActionResult Edit(int id)
        {
            var membershipStatus = MembershipStatusService.GetMembershipStatus(id);
            if (membershipStatus == null) return HttpNotFound();
            return View("MembershipStatusForm", membershipStatus);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(MembershipStatus membershipStatus)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = membershipStatus;
                return View("MembershipStatusForm", viewModel);
            }
            if (membershipStatus.Id == 0)
            {
                var result = MembershipStatusService.Save(membershipStatus);
            }
            else
            {
                var membershipStatusInDb = MembershipStatusService.GetMembershipStatus(membershipStatus.Id);
                if (membershipStatusInDb == null) return HttpNotFound();
                membershipStatusInDb.StatusCode = membershipStatus.StatusCode;
                membershipStatusInDb.StatusDescription = membershipStatus.StatusDescription;
                var result = MembershipStatusService.Update(membershipStatusInDb);
            }
            return RedirectToAction("Index","MembershipStatus");
        }
    }
}