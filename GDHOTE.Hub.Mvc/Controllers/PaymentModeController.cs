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
    public class PaymentModeController : BaseController
    {
        // GET: PaymentMode
        public ActionResult Index()
        {
            var paymentModes = PortalPaymentModeService.GetAllPaymentModes().ToList();
            return View("PaymentModeIndex", paymentModes);
        }
        public ActionResult New()
        {
            return View("PaymentModeForm", ReturnViewModel());
        }
        public ActionResult Edit(string id)
        {
            var paymentMode = PortalPaymentModeService.GetPaymentMode(id);
            var viewModel = ReturnViewModel();
            var item = JsonConvert.SerializeObject(paymentMode);
            viewModel = JsonConvert.DeserializeObject<PaymentModeFormViewModel>(item);
            return View("PaymentModeForm", viewModel);
        }
        public ActionResult Save(CreatePaymentModeRequest createRequest)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = ReturnViewModel();
                var item = JsonConvert.SerializeObject(createRequest);
                viewModel = JsonConvert.DeserializeObject<PaymentModeFormViewModel>(item);
                return View("PaymentModeForm", viewModel);
            }
            var result = PortalPaymentModeService.CreatePaymentMode(createRequest);
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
            return View("PaymentModeForm");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DeletePaymentMode(string id)
        {
            var result = PortalPaymentModeService.DeletePaymentMode(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private static PaymentModeFormViewModel ReturnViewModel()
        {
            var statuses = PortalStatusService.GetStatuses().ToList();
            var viewModel = new PaymentModeFormViewModel
            {
                Statuses = statuses
            };
            return viewModel;
        }
    }
}