using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GDHOTE.Hub.Core.Models;
using GDHOTE.Hub.Core.Services;
using GDHOTE.Hub.Core.ViewModels;

namespace GDHOTE.Hub.Mvc.Controllers
{
    public class SubMenuController : Controller
    {
        // GET: SubMenu
        public ActionResult Index()
        {
            var subMenus = SubMenuService.GetSubMenus().ToList();
            return View("SubMenuIndex", subMenus);
        }
        public ActionResult New()
        {
            var statuses = StatusService.GetStatus().ToList();
            var mainMenus = MainMenuService.GetMainMenus().ToList();
            var viewModel = new SubMenuFormViewModel
            {
                Status = statuses,
                MainMenu = mainMenus,
                SubMenu = new SubMenu()
            };
            return View("SubMenuForm", viewModel);
        }
        public ActionResult Edit(string id)
        {
            var subMenu = SubMenuService.GetSubMenu(id);
            if (subMenu == null) return HttpNotFound();
            var statuses = StatusService.GetStatus().ToList();
            var mainMenus = MainMenuService.GetMainMenus().ToList();
            var viewModel = new SubMenuFormViewModel
            {
                Status = statuses,
                MainMenu = mainMenus,
                SubMenu = subMenu
            };
            return View("SubMenuForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(SubMenu subMenu)
        {
            if (!ModelState.IsValid)
            {
                var statuses = StatusService.GetStatus().ToList();
                var mainMenus = MainMenuService.GetMainMenus().ToList();
                var viewModel = new SubMenuFormViewModel
                {
                    Status = statuses,
                    MainMenu = mainMenus,
                    SubMenu = subMenu
                };
                return View("SubMenuForm", viewModel);
            }
            if (subMenu.SubMenuId == null)
            {
                subMenu.SubMenuId = Guid.NewGuid().ToString();
                subMenu.CreatedDate = DateTime.Now;
                subMenu.CreatedBy = 0;
                var result = SubMenuService.Save(subMenu);
            }
            else
            {
                var subMenuInDb = SubMenuService.GetSubMenu(subMenu.SubMenuId);
                if (subMenuInDb == null) return HttpNotFound();
                subMenuInDb.Name = subMenu.Name;
                subMenuInDb.Url = subMenu.Url;
                subMenuInDb.Name = subMenu.Name;
                subMenuInDb.ParentId = subMenu.ParentId;
                subMenuInDb.DisplaySequence = subMenu.DisplaySequence;
                var result = SubMenuService.Update(subMenuInDb);
            }

            return RedirectToAction("Index");
        }
    }
}