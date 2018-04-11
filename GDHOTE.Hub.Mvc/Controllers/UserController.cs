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
    public class UserController : BaseController
    {
        // GET: User
        public ActionResult Index()
        {
            var users = PortalUserService.GetAllUsers().ToList();
            return View("UserIndex", users);
        }

        //GET: User/Details/5
        public ActionResult Details(string id)
        {
            return View("UserIndex");
        }

        // GET: User/Create
        public ActionResult New()
        {
            return View("UserForm", ReturnViewModel());
        }
        // GET: User/Edit/5
        public ActionResult Edit(string id)
        {
            var user = PortalUserService.GetUser(id);
            if (user == null) return HttpNotFound();
            var viewModel = ReturnViewModel();
            var item = JsonConvert.SerializeObject(user);
            viewModel = JsonConvert.DeserializeObject<AdminUserFormViewModel>(item);
            return View("UserForm", viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(CreateAdminUserRequest createRequest)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = ReturnViewModel();
                return View("UserForm", viewModel);
            }
            var result = PortalUserService.CreateUser(createRequest);
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
            return View("UserForm", ReturnViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteUser(string id)
        {
            var result = PortalUserService.DeleteUser(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private static AdminUserFormViewModel ReturnViewModel()
        {
            var roleTypes = PortalRoleTypeService.GetRoleTypes().ToList();
            var roles = new List<Role>(); // PortalRoleService.GetActiveRoles().ToList();
            var userStatues = PortalUserStatusService.GetActiveUserStatuses().ToList();
            var viewModel = new AdminUserFormViewModel
            {
                RoleTypes = roleTypes,
                Roles = roles,
                UserStatuses = userStatues
            };
            return viewModel;
        }
    }
}
