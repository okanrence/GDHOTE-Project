using System;
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
    public class YearGroupController : BaseController
    {
        // GET: YearGroup
        public ActionResult Index()
        {
            var yearGroup = PortalYearGroupService.GetAllYearGroups().ToList();
            return View("YearGroupIndex", yearGroup);
        }
        public ActionResult New()
        {
            return View("YearGroupForm", ReturnViewModel());
        }
        public ActionResult Edit(string id)
        {
            var yearGroup = PortalYearGroupService.GetYearGroup(id);
            var viewModel = ReturnViewModel();
            var item = JsonConvert.SerializeObject(yearGroup);
            viewModel = JsonConvert.DeserializeObject<YearGroupFormViewModel>(item);
            return View("YearGroupForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(CreateYearGroupRequest createRequest)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = ReturnViewModel();
                var item = JsonConvert.SerializeObject(createRequest);
                viewModel = JsonConvert.DeserializeObject<YearGroupFormViewModel>(item);
                return View("YearGroupForm", viewModel);
            }
            var result = PortalYearGroupService.CreateYearGroup(createRequest);
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
            return View("YearGroupForm");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteYearGroup(string id)
        {
            var result = PortalYearGroupService.DeleteYearGroup(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private static YearGroupFormViewModel ReturnViewModel()
        {
            var statuses = PortalStatusService.GetStatuses().ToList();
            var viewModel = new YearGroupFormViewModel
            {
                Statuses = statuses
            };
            return viewModel;
        }
    }
}