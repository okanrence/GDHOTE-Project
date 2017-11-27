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
    public class CurrencyController : Controller
    {
        // GET: Currency
        public ActionResult Index()
        {
            var currencies = CurrencyService.GetCurrencies().ToList();
            return View("CurrencyIndex", currencies);
        }
        public ActionResult New()
        {
            var statuses = StatusService.GetStatus().ToList();
            var viewModel = new CurrencyFormViewModel
            {
                Status = statuses,
                Currency = new Currency()
            };
            return View("CurrencyForm", viewModel);
        }
        public ActionResult Edit(int id)
        {
            var currency = CurrencyService.GetCurrency(id);
            if (currency == null) return HttpNotFound();
            var statuses = StatusService.GetStatus().ToList();
            var viewModel = new CurrencyFormViewModel
            {
                Status = statuses,
                Currency = currency
            };
            return View("CurrencyForm", viewModel);
        }
        public ActionResult Save(Currency currency)
        {
            if (!ModelState.IsValid)
            {
                var statuses = StatusService.GetStatus().ToList();
                var viewModel = new CurrencyFormViewModel
                {
                    Status = statuses,
                    Currency = currency
                };
                return View("CurrencyForm", viewModel);
            }
            currency.CurrencyCode = currency.CurrencyCode.ToUpper();
            currency.Description = StringCaseManager.TitleCase(currency.Description);
            currency.RecordDate = DateTime.Now;
            if (currency.CurrencyId == 0)
            {
                var result = CurrencyService.Save(currency);
            }
            else
            {
                var currencyInDb = CurrencyService.GetCurrency(currency.CurrencyId);
                if (currencyInDb == null) return HttpNotFound();
                currencyInDb.CurrencyCode = currency.CurrencyCode.ToUpper();
                currencyInDb.Description = StringCaseManager.TitleCase(currency.Description);
                currencyInDb.Status = currency.Status;
                var result = CurrencyService.Update(currencyInDb);
            }
            return RedirectToAction("Index", "Currency");
        }
    }
}