using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GDHOTE.Hub.Core.Models;
using GDHOTE.Hub.Core.Services;

namespace GDHOTE.Hub.Mvc.Controllers
{
    public class PaymentTypeController : Controller
    {
        // GET: PaymentType
        public ActionResult Index()
        {
            var paymentTypes = PaymentTypeService.GetPaymentTypes().ToList();
            return View(paymentTypes);
        }
        public ActionResult New()
        {
            var paymentType = new PaymentType();
            return View("PaymentTypeForm", paymentType);
        }
        public ActionResult Save(PaymentType paymentType)
        {
            if (!ModelState.IsValid)
            {
                return View("PaymentTypeForm", paymentType);
            }
            if (paymentType.PaymentTypeId == 0)
            {
                paymentType.Status = "A";
                var result = PaymentTypeService.Save(paymentType);
            }
            else
            {
                var paymentTypeInDb = PaymentTypeService.GetPaymentType(paymentType.PaymentTypeId);
                if (paymentTypeInDb == null) return HttpNotFound();
                paymentTypeInDb.Description = paymentType.Description;
                var result = PaymentTypeService.Update(paymentTypeInDb);
            }
            return RedirectToAction("Index", "PaymentType");
        }
        public ActionResult Edit(int id)
        {
            var paymentType = PaymentTypeService.GetPaymentType(id);
            if (paymentType == null) return HttpNotFound();
            return View("PaymentTypeForm", paymentType);
        }
    }
}