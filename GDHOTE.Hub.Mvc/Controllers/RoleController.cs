using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GDHOTE.Hub.CoreObject.Models;
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
            var viewModelTemp = ReturnViewModel();
            var item = JsonConvert.SerializeObject(role);
            var viewModel = JsonConvert.DeserializeObject<RoleFormViewModel>(item);
            viewModel.RoleTypes = viewModelTemp.RoleTypes;
            viewModel.Statuses = viewModelTemp.Statuses;
            return View("RoleForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(CreateRoleRequest createRequest)
        {
            if (!ModelState.IsValid)
            {
                var item = JsonConvert.SerializeObject(createRequest);
                var viewModelTemp = ReturnViewModel();
                var viewModel = JsonConvert.DeserializeObject<RoleFormViewModel>(item);
                viewModel.RoleTypes = viewModelTemp.RoleTypes;
                viewModel.Statuses = viewModelTemp.Statuses;
                return View("RoleForm", viewModel);
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
            return View("RoleForm", ReturnViewModel());
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
            var roleTypes = PortalRoleTypeService.GetActiveRoleTypes().ToList();
            var viewModel = new RoleFormViewModel
            {
                Statuses = statuses,
                RoleTypes = roleTypes
            };
            return viewModel;
        }
    }
}