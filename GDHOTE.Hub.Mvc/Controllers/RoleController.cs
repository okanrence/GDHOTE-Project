using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GDHOTE.Hub.Core.BusinessLogic;
using GDHOTE.Hub.Core.Models;
using GDHOTE.Hub.Core.Services;
using GDHOTE.Hub.Core.ViewModels;

namespace GDHOTE.Hub.Mvc.Controllers
{
    public class RoleController : Controller
    {
        // GET: Role
        public ActionResult Index()
        {
            var roles = RoleService.GetRoles().ToList();
            return View("RoleIndex", roles);
        }
        public ActionResult Edit(string id)
        {
            var role = RoleService.GetRole(id);
            if (role == null) return HttpNotFound();
            var viewModel = new RoleFormViewModel
            {
                Role = role
            };
            return View("RoleForm", viewModel);
        }
        public ActionResult New()
        {
            var viewModel = new RoleFormViewModel
            {
                Role = new Role()
            };
            return View("RoleForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Role role)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new RoleFormViewModel
                {
                    Role = role
                };
                return View("RoleForm", viewModel);
            }
            role.Name = StringCaseManager.TitleCase(role.Name);
            if (role.RoleId == null)
            {
                role.RoleId = Guid.NewGuid().ToString();
                role.CreatedDate = DateTime.Now;
                var result = RoleService.Save(role);
            }
            else
            {
                var roleInDb = RoleService.GetRole(role.RoleId);
                if (roleInDb == null) return HttpNotFound();
                roleInDb.Name = role.Name;
                var result = RoleService.Update(roleInDb);
            }
            return RedirectToAction("Index");
        }
    }
}