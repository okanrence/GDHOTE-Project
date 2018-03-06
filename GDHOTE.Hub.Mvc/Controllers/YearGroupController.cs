using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GDHOTE.Hub.BusinessCore.BusinessLogic;
using GDHOTE.Hub.CoreObject.Models;
using GDHOTE.Hub.BusinessCore.Services;
using GDHOTE.Hub.CoreObject.ViewModels;

namespace GDHOTE.Hub.Mvc.Controllers
{
    public class YearGroupController : BaseController
    {
        // GET: YearGroup
        public ActionResult Index()
        {
            var yearGroup = YearGroupService.GetYearGroups().ToList();
            return View("YearGroupIndex", yearGroup);
        }
        public ActionResult New()
        {
            var statuses = StatusService.GetStatus().ToList();
            var viewModel = new YearGroupFormViewModel
            {
                Status = statuses,
                YearGroup = new YearGroup()
            };

            return View("YearGroupForm", viewModel);
        }
        public ActionResult Edit(int id)
        {
            var yearGroup = YearGroupService.GetYearGroup(id);
            if (yearGroup == null) return HttpNotFound();
            var statuses = StatusService.GetStatus().ToList();
            var viewModel = new YearGroupFormViewModel
            {
                Status = statuses,
                YearGroup = yearGroup
            };
            return View("YearGroupForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(YearGroup yearGroup)
        {
            if (!ModelState.IsValid)
            {
                var statuses = StatusService.GetStatus().ToList();
                var viewModel = new YearGroupFormViewModel
                {
                    Status = statuses,
                    YearGroup = yearGroup
                };
                return View("YearGroupForm", viewModel);
            }
            yearGroup.RecordDate = DateTime.Now;
            yearGroup.YearGroupCode = yearGroup.YearGroupCode.ToUpper();
            yearGroup.Description = StringCaseManager.TitleCase(yearGroup.Description);
            if (yearGroup.Id == 0)
            {
                var result = YearGroupService.Save(yearGroup);
            }
            else
            {
                var yearGroupInDb = YearGroupService.GetYearGroup(yearGroup.Id);
                if (yearGroupInDb == null) return HttpNotFound();
                yearGroupInDb.YearGroupCode = yearGroup.YearGroupCode;
                yearGroupInDb.Description = yearGroup.Description;
                yearGroupInDb.Status = yearGroup.Status;
                var result = YearGroupService.Update(yearGroupInDb);
            }
            return RedirectToAction("Index", "YearGroup");
        }
    }
}