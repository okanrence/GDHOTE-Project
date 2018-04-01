using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GDHOTE.Hub.CoreObject.Models;
using GDHOTE.Hub.BusinessCore.Services;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using GDHOTE.Hub.CoreObject.ViewModels;
using GDHOTE.Hub.PortalCore.Services;
using Newtonsoft.Json;

namespace GDHOTE.Hub.Mvc.Controllers
{
    public class PaymentController : BaseController
    {
        // GET: Payment
        public ActionResult Index()
        {
            string startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("dd-MMM-yyyy");
            string endDate = DateTime.Now.ToString("dd-MMM-yyyy");
            var payments = PortalPaymentService.GetPayments(startDate, endDate).ToList();
            return View("PaymentIndex", payments);
        }
        public ActionResult New()
        {
            return View("PaymentForm", ReturnViewModel());
        }
        public ActionResult Edit(string id)
        {
            var payment = PortalPaymentService.GetPayment(id);
            var viewModel = ReturnViewModel();
            var item = JsonConvert.SerializeObject(payment);
            viewModel = JsonConvert.DeserializeObject<PaymentFormViewModel>(item);
            return View("PaymentForm", viewModel);
        }
        public ActionResult Save(CreatePaymentRequest createRequest)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = ReturnViewModel();
                var item = JsonConvert.SerializeObject(createRequest);
                viewModel = JsonConvert.DeserializeObject<PaymentFormViewModel>(item);
                return View("PaymentForm", viewModel);
            }
            var result = PortalPaymentService.CreatePayment(createRequest);
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
            return View("PaymentForm", ReturnViewModel());
        }

        public ActionResult Manage()
        {
            string startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("dd-MMM-yyyy");
            string endDate = DateTime.Now.ToString("dd-MMM-yyyy");
            var payments = PortalPaymentService.GetPayments(startDate, endDate).ToList();
            return View("ManagePayment", payments);
        }
       

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteMember(ConfirmPaymentRequest confirmRequest)
        {
            var result = PortalPaymentService.DeletePayment(confirmRequest);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ConfirmPayment(ConfirmPaymentRequest confirmRequest)
        {
            var result = PortalPaymentService.ConfirmPayment(confirmRequest);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult FetchReportByDate(string startDate, string endDate)
        {
            var visitors = PortalPaymentService.GetPayments(startDate, endDate);
            return PartialView("_PaymentReport", visitors);
        }

        private static PaymentFormViewModel ReturnViewModel()
        {
            var paymentModes = PortalPaymentModeService.GetActivePaymentModes().ToList();
            var paymentTypes = PortalPaymentTypeService.GetActivePaymentTypes().ToList();
            var currencies = PortalCurrencyService.GetActiveCurrencies().ToList();
            var viewModel = new PaymentFormViewModel
            {
                PaymentTypes = paymentTypes,
                ModeOfPayments = paymentModes,
                Currencies = currencies
            };
            return viewModel;
        }
    }
}