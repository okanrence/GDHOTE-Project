using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using GDHOTE.Hub.PortalCore.Services;
using Newtonsoft.Json;

namespace GDHOTE.Hub.Mvc.Controllers
{
    public class MemberStatusController : BaseController
    {
        // GET: MembershipStatus
        public ActionResult Index()
        {
            var memberStatuses = PortalMemberStatusService.GetAllMemberStatuses(SetToken());
            return View("MemberStatusIndex", memberStatuses);
        }
        public ActionResult New()
        {
            var viewModel = new CreateMemberStatusRequest();
            return View("MemberStatusForm", viewModel);
        }
        public ActionResult Edit(string id)
        {
            var membershipStatus = PortalMemberStatusService.GetMemberStatus(id, SetToken());
            var viewModel = new CreateMemberStatusRequest();
            var item = JsonConvert.SerializeObject(membershipStatus);
            viewModel = JsonConvert.DeserializeObject<CreateMemberStatusRequest>(item);
            return View("MemberStatusForm", viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(CreateMemberStatusRequest createRequest)
        {
            if (!ModelState.IsValid)
            {
                return View("MemberStatusForm");
            }
            var result = PortalMemberStatusService.CreateMemberStatus(createRequest, SetToken());
            if (result != null)
            {
                //Successful
                if (result.ErrorCode == "00")
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ErrorBag = result.ErrorMessage;
                }

            }
            else
            {
                ViewBag.ErrorBag = "Unable to complete your request at the moment";
            }
            // If we got this far, something failed, redisplay form
            return View("MemberStatusForm");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteMemberStatus(string id)
        {
            var result = PortalMemberStatusService.DeleteMemberStatus(id, SetToken());
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}