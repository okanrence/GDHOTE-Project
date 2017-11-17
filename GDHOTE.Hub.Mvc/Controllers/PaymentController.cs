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
    public class PaymentController : Controller
    {
        // GET: Payment
        public ActionResult Index()
        {
            //DateTime startDate = DateTime.Now;
            var payments = PaymentViewService.GetPayments().ToList();
            return View(payments);
        }
        public ActionResult New()
        {
            var paymentModes = PaymentModeService.GetActivePaymentModes().ToList();
            var paymentTypes = PaymentTypeService.GetActivePaymentTypes().ToList();
            var paymentViewModel = new PaymentFormViewModel
            {
                Payment = new Payment(),
                ModeOfPayments = paymentModes,
                PaymentTypes = paymentTypes
            };
            return View("PaymentForm", paymentViewModel);
        }
        public ActionResult Edit(int id)
        {
            var payment = PaymentService.GetPayment(id);
            var paymentModes = PaymentModeService.GetActivePaymentModes().ToList();
            var paymentTypes = PaymentTypeService.GetActivePaymentTypes().ToList();
            var paymentViewModel = new PaymentFormViewModel
            {
                Payment = payment,
                ModeOfPayments = paymentModes,
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
                var paymentViewModel = new PaymentFormViewModel
                {
                    ModeOfPayments = paymentModes,
                    PaymentTypes = paymentTypes,
                    Payment = payment
                };
                return View("PaymentForm", paymentViewModel);
            }
            payment.CreatedBy = 0;
            payment.RecordDate = DateTime.Now;
            payment.PostedDate = DateTime.Now;
            if (payment.PaymentId == 0)
            {
                //paymentType.Status = "A";
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
        public ActionResult List2(string startDate)
        {
            DateTime startDateTemp = DateTime.Now;
            if (string.IsNullOrEmpty(startDate)) startDateTemp = DateTime.Now;
            var payments = PaymentViewService.GetPaymentsByDate(startDateTemp).ToList();
            return View("ReadOnlyList");
        }
    }
}