using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using GDHOTE.Hub.PortalCore.Services;

namespace GDHOTE.Hub.Mvc.Controllers
{
    public class TransactionController : BaseController
    {
        // GET: Transaction
        public ActionResult Index()
        {
            string startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("dd-MMM-yyyy");
            string endDate = DateTime.Now.ToString("dd-MMM-yyyy");
            var transactions = PortalTransactionService.GetTransactions(startDate, endDate, SetToken());
            return View("TransactionIndex", transactions);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DeletePayment(ConfirmPaymentRequest confirmRequest)
        {
            var result = PortalPaymentService.DeletePayment(confirmRequest, SetToken());
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ConfirmPayment(ConfirmPaymentRequest confirmRequest)
        {
            var result = PortalPaymentService.ConfirmPayment(confirmRequest, SetToken());
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult FetchReportByDate(string startDate, string endDate)
        {
            var transactions = PortalTransactionService.GetTransactions(startDate, endDate, SetToken());
            return PartialView("_TransactionReport", transactions);
        }

    }
}