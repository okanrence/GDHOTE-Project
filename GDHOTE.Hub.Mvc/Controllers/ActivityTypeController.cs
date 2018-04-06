﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using GDHOTE.Hub.CoreObject.ViewModels;
using GDHOTE.Hub.PortalCore.Services;
using Newtonsoft.Json;
using NPoco.fastJSON;

namespace GDHOTE.Hub.Mvc.Controllers
{
    public class ActivityTypeController : BaseController
    {
        // GET: ActivityType
        public ActionResult Index()
        {
            var activiteTypes = PortalActivityTypeService.GetAllActivityTypes().ToList();
            return View("ActivityTypeIndex", activiteTypes);
        }
        public ActionResult New()
        {
            var viewModel = ReturnViewModel();
            return View("ActivityTypeForm", viewModel);
        }
        public ActionResult Edit(string id)
        {
            var activitype = PortalActivityTypeService.GetActivityType(id);
            var viewModelTemp = ReturnViewModel();
            var item = JsonConvert.SerializeObject(activitype);
            var viewModel = JsonConvert.DeserializeObject<ActivityTypeFormViewModel>(item);
            viewModel.Statuses = viewModelTemp.Statuses;
            viewModel.DependencyTypes = viewModelTemp.DependencyTypes;

            return View("ActivityTypeForm", viewModel);
        }
        public ActionResult Save(CreateActivityTypeRequest createRequest)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = ReturnViewModel();
                return View("ActivityTypeForm", viewModel);
            }
            var result = PortalActivityTypeService.CreateActivityType(createRequest);
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
            return View("ActivityTypeForm", ReturnViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteActivityType(string id)
        {
            var result = PortalActivityTypeService.DeleteActivityType(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private static ActivityTypeFormViewModel ReturnViewModel()
        {
            var statuses = PortalStatusService.GetStatuses().ToList();
            var activityTypes = PortalActivityTypeService.GetActiveActivityTypes().ToList();
            var viewModel = new ActivityTypeFormViewModel
            {
                Statuses = statuses,
                DependencyTypes = activityTypes
            };
            return viewModel;
        }
    }
}