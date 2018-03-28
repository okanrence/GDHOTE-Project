using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GDHOTE.Hub.BusinessCore.BusinessLogic;
using GDHOTE.Hub.CoreObject.Models;
using GDHOTE.Hub.BusinessCore.Services;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using GDHOTE.Hub.CoreObject.ViewModels;
using GDHOTE.Hub.PortalCore.Services;
using Newtonsoft.Json;

namespace GDHOTE.Hub.Mvc.Controllers
{
    public class PaymentTypeController : BaseController
    {
        // GET: PaymentType
        public ActionResult Index()
        {
            var paymentTypes = PortalPaymentTypeService.GetAllPaymentTypes().ToList();
            return View(paymentTypes);
        }
        public ActionResult New()
        {
            return View("PaymentTypeForm", ReturnViewModel());
        }
        public ActionResult Save(CreatePaymentTypeRequest createRequest)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = ReturnViewModel();
                var item = JsonConvert.SerializeObject(createRequest);
                viewModel = JsonConvert.DeserializeObject<PaymentTypeFormViewModel>(item);
                return View("PaymentTypeForm", viewModel);
            }
            var result = PortalPaymentTypeService.CreatePaymentType(createRequest);
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
            return View("PaymentTypeForm");
        }
        public ActionResult Edit(string id)
        {
            var paymentType = PortalPaymentTypeService.GetPaymentType(id);
            var viewModel = ReturnViewModel();
            var item = JsonConvert.SerializeObject(paymentType);
            viewModel = JsonConvert.DeserializeObject<PaymentTypeFormViewModel>(item);
            return View("PaymentTypeForm", viewModel);
        }

        private static PaymentTypeFormViewModel ReturnViewModel()
        {
            var statuses = PortalStatusService.GetStatuses().ToList();
            var viewModel = new PaymentTypeFormViewModel
            {
                Statuses = statuses
            };
            return viewModel;
        }
    }
}