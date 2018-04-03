using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using GDHOTE.Hub.PortalCore.Services;
using Newtonsoft.Json;

namespace GDHOTE.Hub.Mvc.Controllers
{
    public class CountryController : BaseController
    {
        // GET: Country
        public ActionResult Index()
        {
            var countries = PortalCountryService.GetAllCountries().ToList();
            return View("CountryIndex", countries);
        }
        public ActionResult New()
        {
            var viewModel = new CreateCountryRequest();
            return View("CountryForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(CreateCountryRequest createRequest)
        {
            if (!ModelState.IsValid)
            {
                return View("CountryForm", createRequest);
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
            return View("CountryForm", createRequest);
        }

        public ActionResult Edit(string id)
        {
            var country = PortalCountryService.GetCountry(id);
            var viewModel = new CreateCountryRequest();
            var item = JsonConvert.SerializeObject(country);
            viewModel = JsonConvert.DeserializeObject<CreateCountryRequest>(item);
            return View("CountryForm", viewModel);

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