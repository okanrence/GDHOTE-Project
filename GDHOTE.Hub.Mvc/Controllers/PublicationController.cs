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
            var publications = PortalPublicationService.GetAllPublications(SetToken());
            return View("PublicationIndex", publications);
        }

        public ActionResult New()
        {
            return View("PublicationForm", ReturnViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(CreatePublicationRequest createRequest, HttpPostedFileBase uploadFile,
            HttpPostedFileBase displayImageFile)
        {
            if (!ModelState.IsValid)
            {

                var viewModelTemp = ReturnViewModel();
                var item = JsonConvert.SerializeObject(createRequest);
                var viewModel = JsonConvert.DeserializeObject<PublicationFormViewModel>(item);
                viewModel.PublicationAccessRights = viewModelTemp.PublicationAccessRights;
                viewModel.PublicationCategories = viewModelTemp.PublicationCategories;
                return View("PublicationForm", viewModel);
            }

            if (uploadFile != null)
            {
                createRequest.UploadFile = uploadFile.FileName;
                createRequest.UploadFileContent = new byte[uploadFile.ContentLength];
                uploadFile.InputStream.Read(createRequest.UploadFileContent, 0, uploadFile.ContentLength);

            }
            if (displayImageFile != null)
            {
                createRequest.DisplayImageFile = displayImageFile.FileName;
                createRequest.DisplayImageFileContent = new byte[displayImageFile.ContentLength];
                displayImageFile.InputStream.Read(createRequest.DisplayImageFileContent, 0, displayImageFile.ContentLength);
            }

            var result = PortalPublicationService.CreatePublication(createRequest, SetToken());
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
            return View("PublicationForm", ReturnViewModel());
        }

        public ActionResult Edit(string id)
        {
            var publication = PortalPublicationService.GetPublication(id, SetToken());
            var viewModelTemp = ReturnViewModel();
            var item = JsonConvert.SerializeObject(publication);
            var viewModel = JsonConvert.DeserializeObject<PublicationFormViewModel>(item);
            viewModel.PublicationAccessRights = viewModelTemp.PublicationAccessRights;
            viewModel.PublicationCategories = viewModelTemp.PublicationCategories;
            return View("PublicationForm", viewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DeletePublication(string id)
        {
            var result = PortalPublicationService.DeletePublication(id, SetToken());
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult GetPublicationsByCategoryId(string id)
        {
            var result = PortalPublicationService.GetPublicationsByCategoryId(id, SetToken());
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private PublicationFormViewModel ReturnViewModel()
        {

            var categories = PortalPublicationCategoryService.GetActivePublicationCategories(SetToken());
            var accessrights = PortalPublicationAccessRightService.GetActivePublicationAccessRights(SetToken());
            var viewModel = new PublicationFormViewModel
            {
                PublicationCategories = categories,
                PublicationAccessRights = accessrights,
                DatePublished = DateTime.Now
            };
            return viewModel;
        }
    }
}