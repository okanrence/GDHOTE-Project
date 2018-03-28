using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GDHOTE.Hub.BusinessCore.BusinessLogic;
using GDHOTE.Hub.CoreObject.Models;
using GDHOTE.Hub.BusinessCore.Services;
using GDHOTE.Hub.CoreObject.ViewModels;

namespace GDHOTE.Hub.Mvc.Controllers
{
    public class PaymentModeController : BaseController
    {
        // GET: PaymentMode
        public ActionResult Index()
        {
            var paymentModes = PaymentModeService.GetPaymentModes().ToList();
            return View("PaymentModeIndex", paymentModes);
        }
        public ActionResult New()
        {
            var statuses = StatusService.GetStatuses().ToList();
            var viewModel = new PaymentModeFormViewModel
            {
                Status = statuses,
                PaymentMode = new PaymentMode()
            };
            return View("PaymentModeForm", viewModel);
        }
        public ActionResult Edit(int id)
        {
            var paymentMode = PaymentModeService.GetPaymentMode(id);
            if (paymentMode == null) return HttpNotFound();
            var statuses = StatusService.GetStatuses().ToList();
            var viewModel = new PaymentModeFormViewModel
            {
                Status = statuses,
                PaymentMode = paymentMode
            };
            return View("PaymentModeForm", viewModel);
        }
        public ActionResult Save(PaymentMode paymentMode)
        {
            if (!ModelState.IsValid)
            {
                var statuses = StatusService.GetStatuses().ToList();
                var viewModel = new PaymentModeFormViewModel
                {
                    Status = statuses,
                    PaymentMode = paymentMode
                };
                return View("PaymentModeForm", viewModel);
            }
            paymentMode.RecordDate = DateTime.Now;
            paymentMode.Description = StringCaseManager.TitleCase(paymentMode.Description);
            if (paymentMode.PaymentModeId == 0)
            {
                var result = PaymentModeService.Save(paymentMode);
            }
            else
            {
                var paymentModeIndDb = PaymentModeService.GetPaymentMode(paymentMode.PaymentModeId);
                if (paymentModeIndDb == null) return HttpNotFound();
                paymentModeIndDb.Status = paymentMode.Status;
                paymentModeIndDb.Description = paymentMode.Description;
                var result = PaymentModeService.Update(paymentMode);
            }
            return RedirectToAction("Index", "PaymentMode");
        }
    }
}