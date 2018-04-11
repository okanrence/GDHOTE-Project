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
        public ActionResult Save(CreatePublicationRequest createRequest, HttpPostedFileBase uploadFile,
            HttpPostedFileBase coverPageImage)
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
            if (coverPageImage != null)
            {
                createRequest.CoverPageImage = coverPageImage.FileName;
                createRequest.CoverPageImageContent = new byte[coverPageImage.ContentLength];
                coverPageImage.InputStream.Read(createRequest.CoverPageImageContent, 0, coverPageImage.ContentLength);
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
            return View("PublicationForm", ReturnViewModel());
        }

        public ActionResult Edit(string id)
        {
            var publication = PortalPublicationService.GetPublication(id);
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
                PublicationAccessRights = accessrights,
                DatePublished = DateTime.Now
            };
            return viewModel;
        }

        //public FileContentResult GetImageFile(int productId)
        //{
        //    //var myByte = new byte[];
        //    //return File(new byte[], "");
        //}
    }
}