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
    public class ActivityController : BaseController
    {
        // GET: Activity
        public ActionResult Index()
        {
            //string criteria = "";
            //string startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("dd-MMM-yyyy");
            //string endDate = DateTime.Now.ToString("dd-MMM-yyyy");
            var members = PortalMemberService.GetAllMembers(SetToken());
            return View("ActivityIndex", members);

            //string startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("dd-MMM-yyyy");
            //string endDate = DateTime.Now.ToString("dd-MMM-yyyy");
            //var activities = PortalActivityService.GetAllActivities(startDate, endDate).ToList();
            //return View("ActivityIndex", activities);
        }
        public ActionResult List()
        {
            string criteria = "";
            string startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("dd-MMM-yyyy");
            string endDate = DateTime.Now.ToString("dd-MMM-yyyy");
            var activities = PortalActivityService.GetActivitiesByCriteria(criteria, startDate, endDate, SetToken()).ToList();
            return View("ActivityList", activities);
        }
        public ActionResult New()
        {
            var viewModel = ReturnViewModel();
            return View("ActivityForm", viewModel);
        }
        public ActionResult Edit(string id)
        {
            var activitype = PortalActivityService.GetActivity(id, SetToken());
            var viewModelTemp = ReturnViewModel();
            var item = JsonConvert.SerializeObject(activitype);
            var viewModel = JsonConvert.DeserializeObject<ActivityFormViewModel>(item);
            viewModel.ActivityTypes = viewModelTemp.ActivityTypes;
            return View("ActivityForm", viewModel);
        }
        public ActionResult Save(CreateActivityRequest createRequest)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = ReturnViewModel();
                return View("ActivityForm", viewModel);
            }
            var result = PortalActivityService.CreateActivity(createRequest, SetToken());
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
            return View("ActivityForm", ReturnViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteActivity(string id)
        {
            var result = PortalActivityService.DeleteActivity(id, SetToken());
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult FetchReportByDate(string criteria, string startdate, string enddate)
        {
            var activities = PortalActivityService.GetActivitiesByCriteria(criteria, startdate, enddate, SetToken()).ToList();
            return PartialView("_ActivityReport", activities);
        }
        public ActionResult MemberActivity(string id)
        {
            //Ensure to review this code later
            long memberId = 0;
            var activities = PortalActivityService.GetMemberActivities(id, SetToken());
            if (activities.Count == 0)
            {
                //Get Member Id
                var member = PortalMemberService.GetMember(id, SetToken());
                if (member != null) memberId = member.Id;
            }
            else
            {
                memberId = activities[0].MemberId;

            }
            ViewBag.MemberId = memberId;
            return View("MemberActivity", activities);
        }
        public ActionResult NewMemberActivity(string id)
        {
            var member = PortalMemberService.GetMember(id, SetToken());
            var viewModelTemp = ReturnViewModel();
            viewModelTemp.MemberId = member.Id;
            return View("_MemberActivityForm", viewModelTemp);
        }
        public JsonResult SaveMemberActivty(CreateActivityRequest createRequest)
        {
            var result = PortalActivityService.CreateActivity(createRequest, SetToken());
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        private ActivityFormViewModel ReturnViewModel()
        {
            var activityTypes = PortalActivityTypeService.GetActiveActivityTypes(SetToken());
            var viewModel = new ActivityFormViewModel
            {
                ActivityTypes = activityTypes
            };
            return viewModel;
        }
    }
}