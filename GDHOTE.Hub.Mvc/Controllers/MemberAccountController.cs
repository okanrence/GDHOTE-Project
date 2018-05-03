using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GDHOTE.Hub.BusinessCore.Services;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using GDHOTE.Hub.CoreObject.ViewModels;
using GDHOTE.Hub.PortalCore.Services;
using Newtonsoft.Json;

namespace GDHOTE.Hub.Mvc.Controllers
{
    public class MemberAccountController : BaseController
    {
        // GET: MemberAccount
        public ActionResult Index()
        {
            var accounts = PortalAccountService.GetActiveAccounts();
            return View("AccountIndex", accounts);
        }
        public ActionResult New()
        {
            var viewModel = ReturnViewModel();
            return View("AccountForm", viewModel);
        }
        public ActionResult Edit(string id)
        {
            var account = PortalAccountService.GetAccount(id);
            var viewModelTemp = ReturnViewModel();
            var item = JsonConvert.SerializeObject(account);
            var viewModel = JsonConvert.DeserializeObject<AccountFormViewModel>(item);
            viewModel.AccountTypes = viewModelTemp.AccountTypes;
            return View("AccountForm", viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(CreateAccountRequest createRequest)
        {
            if (!ModelState.IsValid)
            {
                return View("AccountForm", ReturnViewModel());
            }
            var result = PortalAccountService.CreateAccount(createRequest);
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
            return View("AccountForm", ReturnViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteAccount(string id)
        {
            var result = PortalAccountService.DeleteAccount(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        private static AccountFormViewModel ReturnViewModel()
        {
            var accountTypes = PortalAccountTypeService.GetActiveAccountTypes();
            var viewModel = new AccountFormViewModel
            {
                AccountTypes = accountTypes 
            };
            return viewModel;
        }
    }
}