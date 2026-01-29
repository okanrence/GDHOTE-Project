using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using GDHOTE.Hub.CoreObject.Models;
using GDHOTE.Hub.CoreObject.ViewModels;
using GDHOTE.Hub.PortalCore.Services;
using Newtonsoft.Json;

namespace GDHOTE.Hub.Mvc.Controllers
{
    public class AccrualTypeController : BaseController
    {
        // GET: AccuralType
        public ActionResult Index()
        {
            var accrualTypes = PortalAccrualTypeService.GetAllAccrualTypes(SetToken());
            return View("AccrualIndex", accrualTypes);
        }
        public ActionResult New()
        {
            return View("AccrualTypeForm");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(CreateAccrualTypeRequest createRequest)
        {
            if (!ModelState.IsValid)
            {
                return View("AccrualTypeForm", createRequest);
            }
            var result = PortalAccrualTypeService.CreateAccrualType(createRequest, SetToken());
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
            return View("AccrualTypeForm", createRequest);
        }

        public ActionResult Edit(string id)
        {
            var accrualTypes = PortalAccrualTypeService.GetAccrualType(id, SetToken());
            var viewModelTemp = ReturnUpdateViewModel();
            var item = JsonConvert.SerializeObject(accrualTypes);
            var viewModel = JsonConvert.DeserializeObject<UpdateAccrualTypeFormViewModel>(item);
            viewModel.Statuses = viewModelTemp.Statuses;
            return View("UpdateAccrualTypeForm", viewModel);

        }

        public ActionResult Update(UpdateAccrualTypeRequest updateRequest)
        {
            if (!ModelState.IsValid)
            {
                return View("UpdateAccrualTypeForm", ReturnUpdateViewModel());
            }
            var result = PortalAccrualTypeService.UpdateAccrualType(updateRequest, SetToken());
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
            return View("UpdateAccrualTypeForm", ReturnUpdateViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteAccrualType(string id)
        {
            var result = PortalAccrualTypeService.DeleteAccrualType(id, SetToken());
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        private UpdateAccrualTypeFormViewModel ReturnUpdateViewModel()
        {
            var statuses = PortalStatusService.GetStatuses(SetToken());
            var viewModel = new UpdateAccrualTypeFormViewModel
            {
                Statuses = statuses
            };
            return viewModel;
        }
    }
}