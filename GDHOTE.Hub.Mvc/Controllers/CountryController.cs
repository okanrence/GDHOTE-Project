using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GDHOTE.Hub.Core.BusinessLogic;
using GDHOTE.Hub.CoreObject.Models;
using GDHOTE.Hub.Core.Services;

namespace GDHOTE.Hub.Mvc.Controllers
{
    public class CountryController : BaseController
    {
        // GET: Country
        public ActionResult Index()
        {
            var countries = CountryService.GetCountries().ToList();
            return View("CountryIndex",countries);
        }
        public ActionResult New()
        {
            var country = new Country();
            return View("CountryForm", country);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Country country)
        {
            if (!ModelState.IsValid)
            {
                return View("CountryForm");
            }
            country.RecordDate = DateTime.Now;
            country.Status = "A";
            country.CountryCode = country.CountryCode.ToUpper();
            country.CountryName = StringCaseManager.TitleCase(country.CountryName);
            if (country.CountryId == 0)
            {
                var result = CountryService.Save(country);
            }
            else
            {
                var countryInDb = CountryService.GetCountry(country.CountryId);
                if (countryInDb == null) return HttpNotFound();
                countryInDb.CountryName = country.CountryName;
                var result = CountryService.Update(countryInDb);
            }
            return RedirectToAction("Index", "Country");
        }
        public ActionResult Edit(int id)
        {
            var country = CountryService.GetCountry(id);
            if (country == null) return HttpNotFound();
            return View("CountryForm", country);

        }
    }
}