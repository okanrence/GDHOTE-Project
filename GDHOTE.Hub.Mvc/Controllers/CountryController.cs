using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GDHOTE.Hub.BusinessCore.BusinessLogic;
using GDHOTE.Hub.CoreObject.Models;
using GDHOTE.Hub.BusinessCore.Services;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using GDHOTE.Hub.PortalCore.Services;

namespace GDHOTE.Hub.Mvc.Controllers
{
    public class CountryController : BaseController
    {
        // GET: Country
        public ActionResult Index()
        {
            var countries = CountryService.GetAllCountries().ToList();
            return View("CountryIndex", countries);
        }
        public ActionResult New()
        {
            var country = new Country();
            return View("CountryForm", country);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(CreateCountryRequest createRequest)
        {
            if (!ModelState.IsValid)
            {
                return View("CountryForm");
            }
            var result = PortalCountryService.CreateCountry(createRequest);
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
            return View("CountryForm");
        }

        public ActionResult Edit(int id)
        {
            var country = CountryService.GetCountry(id);
            if (country == null) return HttpNotFound();
            return View("CountryForm", country);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteCountry(string id)
        {
            var result = PortalCountryService.DeleteCountry(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}