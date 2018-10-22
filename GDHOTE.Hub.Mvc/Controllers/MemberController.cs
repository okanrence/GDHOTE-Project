﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
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
            //string criteria = "";
            //string startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("dd-MMM-yyyy");
            //string endDate = DateTime.Now.ToString("dd-MMM-yyyy");
            //var members = PortalMemberService.GetMembersByCriteria(criteria, startDate, endDate).ToList();

            var members = PortalMemberService.GetRecentMembers(SetToken());
            return View(members);
        }

        public ActionResult GetMemberByID(string id)
        {
            //string criteria = "";
            //string startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("dd-MMM-yyyy");
            //string endDate = DateTime.Now.ToString("dd-MMM-yyyy");
            //var members = PortalMemberService.GetMembersByCriteria(criteria, startDate, endDate).ToList();

            var member = PortalMemberService.GetMemberById(id,SetToken());
            return View("_Index", member);
        }
        public ActionResult List()
        {
            string criteria = "";
            string startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("dd-MMM-yyyy");
            string endDate = DateTime.Now.ToString("dd-MMM-yyyy");
            var members = PortalMemberService.GetMembersByCriteria(criteria, startDate, endDate, SetToken());
            return View("ReadOnlyList", members);
        }
        public ActionResult New()
        {
            var viewModel = ReturnViewModel();
            return View("MemberForm", viewModel);
        }
        public ActionResult Edit(string id)
        {
            var member = PortalMemberService.GetMember(id, SetToken());
            var viewModelTemp = ReturnViewModel();
            var item = JsonConvert.SerializeObject(member);
            var viewModel = JsonConvert.DeserializeObject<UpdateMemberFormViewModel>(item);
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
            var result = PortalMemberService.CreateMember(createRequest, SetToken());
            if (result != null)
            {
                //Successful
                if (result.ErrorCode == "00")
                {
                    return RedirectToAction("Index", "MemberDetails");
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
                return View("UpdateMemberForm");
            }
            var result = PortalMemberService.UpdateMember(updateRequest, SetToken());
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
            return View("UpdateMemberForm");
        }


        public ActionResult ApproveMember()
        {
            var members = PortalMemberService.GetPendingApproval(SetToken());
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
            var result = PortalMemberService.ApproveMember(approveRequest, SetToken());
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteMember(string id)
        {
            var result = PortalMemberService.DeleteMember(id, SetToken());
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UploadMember(UploadMemberRequest uploadRequest, HttpPostedFileBase uploadFile)
        {
            if (uploadFile == null)
            {
                ViewBag.ErrorBag = "Please specify a valid file";
            }
            else
            {
                uploadRequest.UploadFile = uploadFile.FileName;
                uploadRequest.UploadFileContent = new byte[uploadFile.ContentLength];
                uploadFile.InputStream.Read(uploadRequest.UploadFileContent, 0, uploadFile.ContentLength);
                uploadRequest.ChannelCode = (int)CoreObject.Enumerables.Channel.Web;
                var result = PortalMemberService.UploadMembers(uploadRequest, SetToken());
                if (result != null)
                {
                    ViewBag.ErrorBag = result.ErrorMessage;
                }
                else
                {
                    ViewBag.ErrorBag = "Unable to complete your request at the moment";
                }
            }
            // If we got this far, something failed, redisplay form
            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult GetMember(string term)
        {
            var result = PortalMemberService.GetMembersByName(term, SetToken());
            var data = result.Select(r => new { value = r.MemberId, label = r.FirstName + " " + r.Surname }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetMemberInfo()
        {
            return PartialView("_MemberInfo");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetMemberInfo(string id)
        {
            var result = PortalMemberService.GetMemberInformation(id, SetToken());
            return PartialView("_MemberInfo", result);
        }

        [HttpGet]
        public ActionResult GetUser(string searchTerm, int pageSize, int pageNum)
        {

            var member = PortalMemberService.SearchMember(searchTerm, SetToken());

            var data = member.Select(x => new { name = $"{x.Surname} {x.FirstName} {x.OtherNames} - {x.MobileNumber}", id = x.MemberId });
           

            var result = new
            {
                Total = data.Count(),
                Results = data.Skip((pageNum * pageSize) - 100).Take(pageSize)
            };

            return new JsonResult
            {
                Data = result,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };



        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult FetchReportByDate(string criteria, string startdate, string enddate)
        {
            var members = PortalMemberService.GetMembersByCriteria(criteria, startdate, enddate, SetToken());
            return PartialView("_MemberListReport", members);
        }

        private static MemberFormViewModel ReturnViewModel()
        {
            var genders = PortalGenderService.GetAllGenders();
            var maritalStatuses = PortalMaritalStatusService.GetAllMaritalStatuses();
            var viewModel = new MemberFormViewModel
            {
                Genders = genders,
                MaritalStatuses = maritalStatuses
            };
            return viewModel;
        }
    }
}