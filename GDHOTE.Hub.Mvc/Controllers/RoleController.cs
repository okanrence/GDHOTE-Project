using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GDHOTE.Hub.BusinessCore.BusinessLogic;
using GDHOTE.Hub.CoreObject.Models;
using GDHOTE.Hub.BusinessCore.Services;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using GDHOTE.Hub.CoreObject.ViewModels;
using GDHOTE.Hub.PortalCore.Services;
using Newtonsoft.Json;

namespace GDHOTE.Hub.Mvc.Controllers
{
    public class RoleController : BaseController
    {
        // GET: Role
        public ActionResult Index()
        {
            var roles = PortalRoleService.GetAllRoles().ToList();
            return View("RoleIndex", roles);
        }
        public ActionResult New()
        {
            return View("RoleForm", ReturnViewModel());
        }
        public ActionResult Edit(string id)
        {
            var role = PortalRoleService.GetRole(id);
            var viewModel = ReturnViewModel();
            var item = JsonConvert.SerializeObject(role);
            viewModel = JsonConvert.DeserializeObject<RoleFormViewModel>(item);

            return View("RoleForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(CreateRoleRequest createRequest)
        {
            if (!ModelState.IsValid)
            {
                return View("RoleForm");
            }
            var result = PortalRoleService.CreateRole(createRequest);
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
            return View("RoleForm");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult GetRolesByRoleTypeId(string id)
        {
            var result = PortalRoleService.GetRolesByRoleType(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteRole(string id)
        {
            var result = PortalRoleService.DeleteRole(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        private static RoleFormViewModel ReturnViewModel()
        {
            var statuses = PortalStatusService.GetStatuses().ToList();
            var viewModel = new RoleFormViewModel
            {
                Statuses = statuses
            };
            return viewModel;
        }
    }
}