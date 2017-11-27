using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GDHOTE.Hub.Core.BusinessLogic;
using GDHOTE.Hub.Core.Models;
using GDHOTE.Hub.Core.Services;
using GDHOTE.Hub.Core.ViewModels;

namespace GDHOTE.Hub.Mvc.Controllers
{
    public class ActivityTypeController : Controller
    {
        // GET: ActivityType
        public ActionResult Index()
        {
            var activiteTypes = ActivityTypeService.GetActivityTypes().ToList();
            return View(activiteTypes);
        }
        public ActionResult New()
        {
            var statuses = StatusService.GetStatus().ToList();
            var activityTypes = ActivityTypeService.GetActivityTypes().ToList();
            var viewModel = new ActivityTypeFormViewModel
            {
                Status = statuses,
                DependencyTypes = activityTypes,
                ActivityType = new ActivityType()
            };
            return View("ActivityTypeForm", viewModel);
        }
        public ActionResult Edit(int id)
        {
            var activityTypeInDb = ActivityTypeService.GetActivityType(id);
            if (activityTypeInDb == null) return HttpNotFound();
            var statuses = StatusService.GetStatus().ToList();
            var activityTypes = ActivityTypeService.GetActivityTypes().ToList();
            var viewModel = new ActivityTypeFormViewModel
            {
                Status = statuses,
                DependencyTypes = activityTypes,
                ActivityType = activityTypeInDb
            };
            return View("ActivityTypeForm", viewModel);
        }
        public ActionResult Save(ActivityType activityType)
        {
            if (!ModelState.IsValid)
            {
                var statuses = StatusService.GetStatus().ToList();
                var activityTypes = ActivityTypeService.GetActivityTypes().ToList();
                var viewModel = new ActivityTypeFormViewModel
                {
                    Status = statuses,
                    DependencyTypes = activityTypes,
                    ActivityType = activityType
                };
                return View("ActivityTypeForm", viewModel);
            }
            activityType.RecordDate = DateTime.Now;
            activityType.Description = StringCaseManager.TitleCase(activityType.Description);
            if (activityType.ActivityTypeId == 0)
            {
                var result = ActivityTypeService.Save(activityType);
            }
            else
            {
                var activityTypeInDb = ActivityTypeService.GetActivityType(activityType.ActivityTypeId);
                if (activityTypeInDb == null) return HttpNotFound();
                activityTypeInDb.Description = activityType.Description;
                activityTypeInDb.Status = activityType.Status;
                activityTypeInDb.DependencyTypeId = activityType.DependencyTypeId;
                var result = ActivityTypeService.Update(activityTypeInDb);
            }
            return RedirectToAction("Index");
        }
    }
}