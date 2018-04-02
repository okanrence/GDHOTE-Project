using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using GDHOTE.Hub.CoreObject.ViewModels;
using GDHOTE.Hub.PortalCore.Services;
using Newtonsoft.Json;

namespace GDHOTE.Hub.Mvc.Controllers
{
    public class PublicationController : BaseController
    {
        // GET: Publication
        public ActionResult Index()
        {
            var publications = PortalPublicationService.GetAllPublications().ToList();
            return View("PublicationIndex", publications);
        }
        public ActionResult New()
        {
            return View("PublicationForm", ReturnViewModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(CreatePublicationRequest createRequest)
        {
            if (!ModelState.IsValid)
            {

                var viewModel = ReturnViewModel();
                var item = JsonConvert.SerializeObject(createRequest);
                viewModel = JsonConvert.DeserializeObject<PublicationFormViewModel>(item);
                return View("PublicationForm", viewModel);
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
            var publication = PortalPublicationService.GetPublication(id);
            var viewModel = new PublicationFormViewModel();
            var item = JsonConvert.SerializeObject(publication);
            viewModel = JsonConvert.DeserializeObject<PublicationFormViewModel>(item);
            return View("PublicationForm", viewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DeletePublication(string id)
        {
            var result = PortalPublicationService.DeletePublication(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult GetPublicationsByCategoryId(string id)
        {
            var result = PortalPublicationService.GetPublicationsByCategoryId(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        private static PublicationFormViewModel ReturnViewModel()
        {

            var categories = PortalPublicationCategoryService.GetActivePublicationCategories().ToList();
            var accessrights = PortalPublicationAccessRightService.GetActivePublicationAccessRights().ToList();
            var viewModel = new PublicationFormViewModel
            {
                PublicationCategories = categories,
                PublicationAccessRights = accessrights
            };
            return viewModel;
        }
    }
}