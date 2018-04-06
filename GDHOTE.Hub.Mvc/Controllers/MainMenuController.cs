using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GDHOTE.Hub.CoreObject.Models;
using GDHOTE.Hub.BusinessCore.Services;
using GDHOTE.Hub.CoreObject.ViewModels;
using GDHOTE.Hub.PortalCore.Services;

namespace GDHOTE.Hub.Mvc.Controllers
{
    public class MainMenuController : BaseController
    {
        // GET: MainMenu
        public ActionResult Index()
        {
            var mainMenus = MainMenuService.GetMainMenus().ToList();
            return View("MainMenuIndex", mainMenus);
        }
        public ActionResult New()
        {
            var statuses = StatusService.GetStatuses().ToList();
            var viewModel = new MainMenuViewModel
            {
                Status = statuses,
                MainMenu = new MainMenu()
            };
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
        public ActionResult Save(MainMenu mainMenu)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = ReturnViewModel();
                return View("MainMenuForm", viewModel);
            }
            if (mainMenu.MenuId == null)
            {
                mainMenu.MenuId = Guid.NewGuid().ToString();
                mainMenu.CreatedDate = DateTime.Now;
                mainMenu.CreatedBy = User.Identity.Name;
                var result = MainMenuService.Save(mainMenu);
            }
            else
            {
                var mainMenuInDb = MainMenuService.GetMainMenu(mainMenu.MenuId);
                if (mainMenuInDb == null) return HttpNotFound();
                mainMenuInDb.Name = mainMenu.Name;
                mainMenuInDb.Status = mainMenu.Status;
                mainMenuInDb.DisplaySequence = mainMenu.DisplaySequence;
                var result = MainMenuService.Update(mainMenuInDb);
            }
            return RedirectToAction("Index", "MainMenu");
        }

        private static MainMenuViewModel ReturnViewModel()
        {
            var statuses = PortalStatusService.GetStatuses();
            var viewModel = new MainMenuViewModel
            {
                Status = statuses
            };
            return viewModel;
        }
    }
}