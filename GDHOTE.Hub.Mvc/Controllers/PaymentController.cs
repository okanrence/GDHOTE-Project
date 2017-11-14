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
            var payments = PaymentService.GetPayments().ToList();
            return View(payments);
        }
        public ActionResult New()
        {
            var paymentModes = PaymentModeService.GetPaymentModes().ToList();
            var paymentTypes = PaymentTypeService.GetPaymentTypes().ToList();
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
            var paymentModes = PaymentModeService.GetPaymentModes().ToList();
            var paymentTypes = PaymentTypeService.GetPaymentTypes().ToList();
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
                var paymentModes = PaymentModeService.GetPaymentModes().ToList();
                var paymentTypes = PaymentTypeService.GetPaymentTypes().ToList();
                var paymentViewModel = new PaymentFormViewModel
                {
                    Payment = new Payment(),
                    ModeOfPayments = paymentModes,
                    PaymentTypes = paymentTypes
                };
                return View("PaymentForm", paymentViewModel);
            }
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

    }
}