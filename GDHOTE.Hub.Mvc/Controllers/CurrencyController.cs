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
    public class CurrencyController : BaseController
    {
        // GET: Currency
        public ActionResult Index()
        {
            var currencies = PortalCurrencyService.GetAllCurrencies(SetToken());
            return View("CurrencyIndex", currencies);
        }
        public ActionResult New()
        {
            return View("CurrencyForm", ReturnViewModel());
        }

        public ActionResult Edit(string id)
        {
            var currency = PortalCurrencyService.GetCurrency(id, SetToken());
            var viewModel = ReturnViewModel();
            var item = JsonConvert.SerializeObject(currency);
            viewModel = JsonConvert.DeserializeObject<CurrencyFormViewModel>(item);
            return View("CurrencyForm", viewModel);
        }
        public ActionResult Save(CreateCurrencyRequest createRequest)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = ReturnViewModel();
                var item = JsonConvert.SerializeObject(createRequest);
                viewModel = JsonConvert.DeserializeObject<CurrencyFormViewModel>(item);
                return View("CurrencyForm", viewModel);
            }
            var result = PortalCurrencyService.CreateCurrency(createRequest, SetToken());
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
            return View("CurrencyForm");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteCurrency(string id)
        {
            var result = PortalCurrencyService.DeleteCurrency(id, SetToken());
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private CurrencyFormViewModel ReturnViewModel()
        {
            var statuses = PortalStatusService.GetStatuses(SetToken());
            var viewModel = new CurrencyFormViewModel
            {
                Statuses = statuses
            };
            return viewModel;
        }
    }
}