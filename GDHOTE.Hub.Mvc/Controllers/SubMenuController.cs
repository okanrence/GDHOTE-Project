using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GDHOTE.Hub.CoreObject.Models;
using GDHOTE.Hub.BusinessCore.Services;
using GDHOTE.Hub.CoreObject.ViewModels;
using GDHOTE.Hub.PortalCore.Services;
using Newtonsoft.Json;

namespace GDHOTE.Hub.Mvc.Controllers
{
    public class SubMenuController : BaseController
    {
        // GET: SubMenu
        public ActionResult Index()
        {

            var subMenus = PortalSubMenuService.GetSubMenus().ToList();
            return View("SubMenuIndex", subMenus);
        }
        public ActionResult New()
        {
            var viewModel = ReturnViewModel();
            return View("SubMenuForm", viewModel);
        }
        public ActionResult Edit(string id)
        {
            var subMenu = SubMenuService.GetSubMenu(id);
            if (subMenu == null) return HttpNotFound();
            var viewModelTemp = ReturnViewModel();
            var item = JsonConvert.SerializeObject(subMenu);
            var viewModel = JsonConvert.DeserializeObject<SubMenuFormViewModel>(item);
            viewModel.Statuses = viewModelTemp.Statuses;
            viewModel.MainMenus = viewModelTemp.MainMenus;
            return View("SubMenuForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(CreateSubMenuRequest createRequest)
        {
            if (!ModelState.IsValid)
            {
                var viewModelTemp = ReturnViewModel();
                var item = JsonConvert.SerializeObject(createRequest);
                var viewModel = JsonConvert.DeserializeObject<SubMenuFormViewModel>(item);
                viewModel.Statuses = viewModelTemp.Statuses;
                viewModel.MainMenus = viewModelTemp.MainMenus;
                return View("SubMenuForm", viewModel);
            }
            var result = PortalSubMenuService.CreateSubMenu(createRequest);
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
            return View("SubMenuForm", ReturnViewModel());
        }
        private static SubMenuFormViewModel ReturnViewModel()
        {
            var mainMenus = PortalMainMenuService.GetActiveMainMenus().ToList();
            var statuses = PortalStatusService.GetStatuses();
            var viewModel = new SubMenuFormViewModel
            {
                Statuses = statuses,
                MainMenus = mainMenus
            };
            return viewModel;
        }
    }
}