using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GDHOTE.Hub.CoreObject.Models;
using GDHOTE.Hub.BusinessCore.Services;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using GDHOTE.Hub.CoreObject.ViewModels;
using GDHOTE.Hub.PortalCore.Services;

namespace GDHOTE.Hub.Mvc.Controllers
{
    public class MainMenuController : BaseController
    {
        // GET: MainMenu
        public ActionResult Index()
        {
            var mainMenus = MainMenuService.GetAllMainMenus().ToList();
            return View("MainMenuIndex", mainMenus);
        }
        public ActionResult New()
        {
            var viewModel = ReturnViewModel();
            return View("MainMenuForm", viewModel);
        }
        public ActionResult Edit(string id)
        {
            var mainMenu = MainMenuService.GetMainMenu(id);
            if (mainMenu == null) return HttpNotFound();
            var viewModel = ReturnViewModel();
            return View("MainMenuForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(CreateMainMenuRequest createRequest)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = ReturnViewModel();
                return View("MainMenuForm", viewModel);
            }
            var result = PortalMainMenuService.CreateMainMenu(createRequest);
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
            return View("MainMenuForm", ReturnViewModel());
        }

        private static MainMenuFormViewModel ReturnViewModel()
        {
            var statuses = PortalStatusService.GetStatuses();
            var viewModel = new MainMenuFormViewModel
            {
                Statuses = statuses
            };
            return viewModel;
        }
    }
}