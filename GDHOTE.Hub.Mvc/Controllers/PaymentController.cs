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
            var paymentTypes = PaymentTypeService.GetPaymentTypes().ToList();
            var paymentViewModel = new PaymentFormViewModel
            {
                Payment = new Payment(),
                PaymentTypes = paymentTypes
            };
            return View("PaymentForm", paymentViewModel);
        }
    }
}