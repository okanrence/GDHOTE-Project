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
    public class PublicationController : Controller
    {
        // GET: Publication
        public ActionResult Index()
        {
            var publications = PortalPublicationService.GetAllPublications().ToList();
            return View("PublicationIndex", publications);
        }
        public ActionResult New()
        {
            var viewModel = new CreatePublicationRequest(); 
            return View("PublicationForm", viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(CreatePublicationRequest createRequest)
        {
            if (!ModelState.IsValid)
            {
                return View("PublicationForm");
            }
            var result = PortalPublicationService.CreatePublication(createRequest);
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
            return View("PublicationForm");
        }

        public ActionResult Edit(string id)
        {
            var Publication = PortalPublicationService.GetPublication(id);
            var viewModel = new CreatePublicationRequest();
            var item = JsonConvert.SerializeObject(Publication);
            viewModel = JsonConvert.DeserializeObject<CreatePublicationRequest>(item);
            return View("PublicationForm", viewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DeletePublication(string id)
        {
            var result = PortalPublicationService.DeletePublication(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}