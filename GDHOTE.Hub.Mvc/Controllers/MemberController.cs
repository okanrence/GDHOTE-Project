﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using GDHOTE.Hub.BusinessCore.Services;
using GDHOTE.Hub.CoreObject.ViewModels;
using GDHOTE.Hub.PortalCore.Services;
using Newtonsoft.Json;

namespace GDHOTE.Hub.Mvc.Controllers
{
    public class MemberController : BaseController
    {
        // GET: Member
        public ActionResult Index()
        {
            var members = PortalMemberService.GetAllMembers().ToList();
            return View(members);
        }
        public ActionResult List()
        {
            var members = PortalMemberService.GetAllMembers().ToList();
            return View("ReadOnlyList", members);
        }
        public ActionResult New()
        {
            var viewModel = ReturnViewModel();
            return View("MemberForm", viewModel);
        }
        public ActionResult Edit(string id)
        {
            var member = PortalMemberService.GetMember(id);
            var viewModelTemp = ReturnViewModel();
            var item = JsonConvert.SerializeObject(member);
            var viewModel = JsonConvert.DeserializeObject<MemberFormViewModel>(item);
            viewModel.Genders = viewModelTemp.Genders;
            viewModel.MaritalStatuses = viewModelTemp.MaritalStatuses;
            return View("UpdateMemberForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(CreateMemberRequest createRequest)
        {
            if (!ModelState.IsValid)
            {
                return View("MemberForm", ReturnViewModel());
            }
            var result = PortalMemberService.CreateMember(createRequest);
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
            return View("MemberForm", ReturnViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(UpdateMemberRequest updateRequest)
        {
            if (!ModelState.IsValid)
            {
                return View("MemberForm", ReturnViewModel());
            }
            return View("MemberForm");
        }


        public ActionResult ApproveMember()
        {
            var members = PortalMemberService.GetPendingApproval().ToList();
            return View(members);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ApproveMember(ApproveMemberRequest approveRequest)
        {

            if (!ModelState.IsValid)
            {
                return Json(ModelState);
            }

            var result = PortalMemberService.ApproveMember(approveRequest);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteMember(string id)
        {
            var result = PortalMemberService.DeleteMember(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult GetMember(string term)
        {
            var result = PortalMemberService.GetMembersByName(term);
            var data = result.Select(r => new { value = r.MemberId, label = r.FirstName  + " " + r.Surname }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        private static MemberFormViewModel ReturnViewModel()
        {
            var genders = GenderService.GetGenders();
            var maritalStatuses = MaritalStatusService.GetMaritalStatuses();
            var viewModel = new MemberFormViewModel
            {
                Genders = genders,
                MaritalStatuses = maritalStatuses
            };
            return viewModel;
        }
    }
}