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
    public class RoleMenuController : BaseController
    {
        // GET: RoleMenu
        public ActionResult Index()
        {
            var roleMenus = PortalRoleMenuService.GetRoleMenus().ToList();
            return View("RoleMenuIndex", roleMenus);
        }
        public ActionResult New()
        {
            var viewModel = ReturnViewModel();
            return View("RoleMenuForm", viewModel);
        }
        public ActionResult Edit(string id)
        {
            var roleMenu = PortalRoleMenuService.GetRoleMenu(id);
            if (roleMenu == null) return HttpNotFound();
            var viewModelTemp = ReturnViewModel();
            var item = JsonConvert.SerializeObject(roleMenu);
            var viewModel = JsonConvert.DeserializeObject<RoleMenuFormViewModel>(item);
            viewModel.Statuses = viewModelTemp.Statuses;
            viewModel.MainMenus = viewModelTemp.MainMenus;
            viewModel.Roles = viewModelTemp.Roles;
            return View("RoleMenuForm", viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(CreateRoleMenuRequest createRequest)
        {

            if (!ModelState.IsValid)
            {
                var viewModelTemp = ReturnViewModel();
                var item = JsonConvert.SerializeObject(createRequest);
                var viewModel = JsonConvert.DeserializeObject<RoleMenuFormViewModel>(item);
                viewModel.Statuses = viewModelTemp.Statuses;
                viewModel.MainMenus = viewModelTemp.MainMenus;
                viewModel.Roles = viewModelTemp.Roles;
                return View("RoleMenuForm", viewModel);
            }

            var result = PortalRoleMenuService.CreateRoleMenu(createRequest);
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
            return View("RoleMenuForm", ReturnViewModel());
        }

        private static RoleMenuFormViewModel ReturnViewModel()
        {
            var statuses = PortalStatusService.GetStatuses();
            var mainMenus = PortalMainMenuService.GetActiveMainMenus().ToList();
            var roles = PortalRoleService.GetActiveRoles().ToList();
            var viewModel = new RoleMenuFormViewModel
            {
                Statuses = statuses,
                SubMenus = new List<SubMenuResponse>(),
                MainMenus = mainMenus,
                Roles = roles
            };
            return viewModel;
        }
    }
}