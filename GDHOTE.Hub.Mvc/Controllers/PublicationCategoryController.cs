using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using GDHOTE.Hub.CoreObject.Models;
using GDHOTE.Hub.PortalCore.Services;
using Newtonsoft.Json;

namespace GDHOTE.Hub.Mvc.Controllers
{
    public class PublicationCategoryController : BaseController
    {
        // GET: Publication Category
        public ActionResult Index()
        {
            var categories = PortalPublicationCategoryService.GetAllPublicationCategories().ToList();
            return View("PublicationCategoryIndex", categories);
        }
        public ActionResult New()
        {
            var viewModel = new CreatePublicationCategoryRequest();
            return View("PublicationCategoryForm", viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(CreatePublicationCategoryRequest createRequest)
        {
            if (!ModelState.IsValid)
            {
                return View("PublicationCategoryForm");
            }
            var result = PortalPublicationCategoryService.CreatePublicationCategory(createRequest);
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
            return View("PublicationCategoryForm");
        }

        public ActionResult Edit(string id)
        {
            var category = PortalPublicationCategoryService.GetPublicationCategory(id);
            var viewModel = new CreatePublicationCategoryRequest();
            var item = JsonConvert.SerializeObject(category);
            viewModel = JsonConvert.DeserializeObject<CreatePublicationCategoryRequest>(item);
            return View("PublicationCategoryForm", viewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DeletePublicationCategory(string id)
        {
            var result = PortalPublicationCategoryService.DeletePublicationCategory(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}