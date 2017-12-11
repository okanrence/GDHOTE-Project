using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GDHOTE.Hub.Core.Models;
using GDHOTE.Hub.Core.Services;
using GDHOTE.Hub.Core.ViewModels;

namespace GDHOTE.Hub.Mvc.Controllers
{
    public class PaymentController : BaseController
    {
        // GET: Payment
        public ActionResult Index()
        {
            //DateTime startDate = DateTime.Now;
            var payments = PaymentViewService.GetPayments().ToList();
            return View("PaymentIndex", payments);
        }
        public ActionResult New()
        {
            var paymentModes = PaymentModeService.GetActivePaymentModes().ToList();
            var paymentTypes = PaymentTypeService.GetActivePaymentTypes().ToList();
            var currencies = CurrencyService.GetActiveCurrencies().ToList();
            var paymentViewModel = new PaymentFormViewModel
            {
                Payment = new Payment(),
                ModeOfPayments = paymentModes,
                Currencies = currencies,
                PaymentTypes = paymentTypes
            };
            return View("PaymentForm", paymentViewModel);
        }
        public ActionResult Edit(int id)
        {
            var payment = PaymentService.GetPayment(id);
            var paymentModes = PaymentModeService.GetActivePaymentModes().ToList();
            var paymentTypes = PaymentTypeService.GetActivePaymentTypes().ToList();
            var currencies = CurrencyService.GetActiveCurrencies().ToList();
            var paymentViewModel = new PaymentFormViewModel
            {
                Payment = payment,
                ModeOfPayments = paymentModes,
                Currencies = currencies,
                PaymentTypes = paymentTypes
            };
            if (payment == null) return HttpNotFound();
            return View("PaymentForm", paymentViewModel);
        }
        public ActionResult Save(Payment payment)
        {
            if (!ModelState.IsValid)
            {
                var paymentModes = PaymentModeService.GetActivePaymentModes().ToList();
                var paymentTypes = PaymentTypeService.GetActivePaymentTypes().ToList();
                var currencies = CurrencyService.GetActiveCurrencies().ToList();
                var paymentViewModel = new PaymentFormViewModel
                {
                    ModeOfPayments = paymentModes,
                    PaymentTypes = paymentTypes,
                    Currencies = currencies,
                    Payment = payment
                };
                return View("PaymentForm", paymentViewModel);
            }
            if (payment.PaymentId == 0)
            {
                payment.ApprovedFlag = "N";
                //payment.ApprovedBy = "NA";
                payment.CreatedBy = User.Identity.Name;
                payment.RecordDate = DateTime.Now;
                payment.PostedDate = DateTime.Now;
                
                var result = PaymentService.Save(payment);
            }
            else
            {
                var paymentInDb = PaymentService.GetPayment(payment.PaymentId);
                if (paymentInDb == null) return HttpNotFound();
                paymentInDb.Narration = payment.Narration;
                var result = PaymentService.Update(paymentInDb);
            }
            return RedirectToAction("Index", "Payment");
        }
        public ActionResult List()
        {
            var payments = PaymentViewService.GetPayments().ToList();
            return View("ReadOnlyList", payments);
        }
        public ActionResult Manage()
        {
            //DateTime startDate = DateTime.Now;
            var payments = PaymentViewService.GetPayments().ToList();
            return View("ManagePayment", payments);
        }
        public ActionResult List2(string startDate)
        {
            DateTime startDateTemp = DateTime.Now;
            if (string.IsNullOrEmpty(startDate)) startDateTemp = DateTime.Now;
            var payments = PaymentViewService.GetPaymentsByDate(startDateTemp).ToList();
            return View("ReadOnlyList");
        }
    }
}