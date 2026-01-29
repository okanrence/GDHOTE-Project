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
    public class PublicationAccessRightController : BaseController
    {
        // GET: ReportAccessRight
        public ActionResult Index()
        {
            var accessRights = PortalPublicationAccessRightService.GetAllPublicationAccessRights(SetToken());
            return View("AccessRightIndex", accessRights);
        }
        public ActionResult New()
        {
            var viewModel = new CreatePublicationAccessRightRequest();
            return View("AccessRightForm", viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(CreatePublicationAccessRightRequest createRequest)
        {
            if (!ModelState.IsValid)
            {
                return View("AccessRightForm", createRequest);
            }
            var result = PortalPublicationAccessRightService.CreatePublicationAccessRight(createRequest, SetToken());
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
            return View("AccessRightForm", createRequest);
        }

        public ActionResult Edit(string id)
        {
            var accessRight = PortalPublicationAccessRightService.GetPublicationAccessRight(id, SetToken());
            var viewModel = new CreatePublicationAccessRightRequest();
            var item = JsonConvert.SerializeObject(accessRight);
            viewModel = JsonConvert.DeserializeObject<CreatePublicationAccessRightRequest>(item);
            return View("AccessRightForm", viewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DeletePublicationAccessRight(string id)
        {
            var result = PortalPublicationAccessRightService.DeletePublicationAccessRight(id, SetToken());
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}