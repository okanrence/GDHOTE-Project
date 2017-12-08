using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GDHOTE.Hub.Core.BusinessLogic;
using GDHOTE.Hub.Core.Models;
using GDHOTE.Hub.Core.Services;
using GDHOTE.Hub.Core.ViewModels;

namespace GDHOTE.Hub.Mvc.Controllers
{
    public class PaymentTypeController : BaseController
    {
        // GET: PaymentType
        public ActionResult Index()
        {
            var paymentTypes = PaymentTypeService.GetPaymentTypes().ToList();
            return View(paymentTypes);
        }
        public ActionResult New()
        {
            var statuses = StatusService.GetStatus().ToList();
            var viewModel = new PaymentTypeFormViewModel
            {
                Status = statuses,
                PaymentType = new PaymentType()
            };
            return View("PaymentTypeForm", viewModel);
        }
        public ActionResult Save(PaymentType paymentType)
        {
            if (!ModelState.IsValid)
            {
                var statuses = StatusService.GetStatus().ToList();
                var viewModel = new PaymentTypeFormViewModel
                {
                    Status = statuses,
                    PaymentType = paymentType
                };
                return View("PaymentTypeForm", viewModel);
            }
            paymentType.Description = StringCaseManager.TitleCase(paymentType.Description);
            if (paymentType.PaymentTypeId == 0)
            {
                var result = PaymentTypeService.Save(paymentType);
            }
            else
            {
                var paymentTypeInDb = PaymentTypeService.GetPaymentType(paymentType.PaymentTypeId);
                if (paymentTypeInDb == null) return HttpNotFound();
                paymentTypeInDb.Status = paymentType.Status;
                paymentTypeInDb.Description = paymentType.Description;
                var result = PaymentTypeService.Update(paymentTypeInDb);
            }
            return RedirectToAction("Index", "PaymentType");
        }
        public ActionResult Edit(int id)
        {
            var paymentType = PaymentTypeService.GetPaymentType(id);
            if (paymentType == null) return HttpNotFound();
            var statuses = StatusService.GetStatus().ToList();
            var viewModel = new PaymentTypeFormViewModel
            {
                Status = statuses,
                PaymentType = paymentType
            };
            return View("PaymentTypeForm", viewModel);
        }
    }
}