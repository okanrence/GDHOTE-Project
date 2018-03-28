using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GDHOTE.Hub.CoreObject.Models;
using GDHOTE.Hub.BusinessCore.Services;
using GDHOTE.Hub.CoreObject.ViewModels;

namespace GDHOTE.Hub.Mvc.Controllers
{
    public class RoleMenuController : BaseController
    {
        // GET: RoleMenu
        public ActionResult Index()
        {
            //var roleMenus = RoleMenuService.GetRoleMenus().ToList();
            var roleMenus = RoleSubMenuViewService.GetRoleMenu().ToList();
            return View("RoleMenuIndex", roleMenus);
        }
        public ActionResult New()
        {
            var statuses = StatusService.GetStatuses().ToList();
            var subMenus = SubMenuService.GetSubMenus().ToList();
            var mainMenus = MainMenuService.GetMainMenus().ToList();
            var roles = RoleService.GetActiveRoles().ToList();
            var viewModel = new RoleMenuFormViewModel
            {
                Statuses = statuses,
                MainMenus = mainMenus,
                SubMenus = subMenus,
                Roles = roles,
                RoleMenu = new RoleMenu()

            };
            return View("RoleMenuForm", viewModel);
        }
        public ActionResult Edit(string id)
        {
            var roleMenu = RoleMenuService.GetRoleMenu(id);
            if (roleMenu == null) return HttpNotFound();
            var statuses = StatusService.GetStatuses().ToList();
            var subMenus = SubMenuService.GetSubMenus().ToList();
            var mainMenus = MainMenuService.GetMainMenus().ToList();
            var roles = RoleService.GetActiveRoles().ToList();
            var viewModel = new RoleMenuFormViewModel
            {
                Statuses = statuses,
                MainMenus = mainMenus,
                SubMenus = subMenus,
                Roles = roles,
                RoleMenu = roleMenu

            };
            return View("RoleMenuForm", viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(RoleMenu roleMenu)
        {
            var viewModel = new RoleMenuFormViewModel();
            if (!ModelState.IsValid)
            {
                var statuses = StatusService.GetStatuses().ToList();
                var subMenus = SubMenuService.GetSubMenus().ToList();
                var mainMenus = MainMenuService.GetMainMenus().ToList();
                var roles = RoleService.GetActiveRoles().ToList();
                viewModel = new RoleMenuFormViewModel
                {
                    Statuses = statuses,
                    MainMenus = mainMenus,
                    SubMenus = subMenus,
                    Roles = roles,
                    RoleMenu = roleMenu

                };
                return View("RoleMenuForm", viewModel);
            }
            if (roleMenu.RoleMenuId == null)
            {
                roleMenu.RoleMenuId = Guid.NewGuid().ToString();
                roleMenu.CreatedBy = User.Identity.Name; 
                roleMenu.CreatedDate = DateTime.Now;
                var result = RoleMenuService.Save(roleMenu);
                //Review this verbose code
                if (result != roleMenu.RoleMenuId)
                {
                    ViewBag.Error = result;
                    var statuses = StatusService.GetStatuses().ToList();
                    var subMenus = SubMenuService.GetSubMenus().ToList();
                    var mainMenus = MainMenuService.GetMainMenus().ToList();
                    var roles = RoleService.GetActiveRoles().ToList();
                    viewModel = new RoleMenuFormViewModel
                    {
                        Statuses = statuses,
                        MainMenus = mainMenus,
                        SubMenus = subMenus,
                        Roles = roles,
                        RoleMenu = roleMenu

                    };
                    return View("RoleMenuForm", viewModel);
                }
            }
            else
            {
                var roleMenuInDb = RoleMenuService.GetRoleMenu(roleMenu.RoleMenuId);
                if (roleMenuInDb == null) return HttpNotFound();
                roleMenuInDb.RoleId = roleMenu.RoleId;
                roleMenuInDb.SubMenuId = roleMenu.SubMenuId;
                roleMenuInDb.Status = roleMenu.Status;
                roleMenuInDb.LastUpdatedBy = User.Identity.Name;
                roleMenu.LastUpdatedDate = DateTime.Now;
                var result = RoleMenuService.Update(roleMenuInDb);
            }
            return RedirectToAction("Index");
        }
    }
}