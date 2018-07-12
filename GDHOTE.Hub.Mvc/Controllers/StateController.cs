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
    public class StateController : BaseController
    {
        // GET: State
        public ActionResult Index()
        {
            var states = PortalStateService.GetAllStates(SetToken());
            return View("StateIndex", states);
        }
        public ActionResult New()
        {
            return View("StateForm", ReturnViewModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(CreateStateRequest createRequest)
        {
            if (!ModelState.IsValid)
            {
                return View("StateForm", ReturnViewModel());
            }
            var result = PortalStateService.CreateState(createRequest, SetToken());
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
            return View("StateForm", ReturnViewModel());
        }
        public ActionResult Edit(string id)
        {
            var state = PortalStateService.GetState(id, SetToken());
            var viewModelTemp = ReturnViewModel();
            var item = JsonConvert.SerializeObject(state);
            var viewModel = JsonConvert.DeserializeObject<StateFormViewModel>(item);
            viewModel.Countries = viewModelTemp.Countries;
            return View("StateForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteState(string id)
        {
            var result = PortalStateService.DeleteState(id, SetToken());
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private StateFormViewModel ReturnViewModel()
        {
            var statuses = PortalStatusService.GetStatuses(SetToken());
            var countries = PortalCountryService.GetActiveCountries();
            var viewModel = new StateFormViewModel
            {
                Statuses = statuses,
                Countries = countries

            };
            return viewModel;
        }
    }
}