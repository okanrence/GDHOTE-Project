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
    public class UserController : BaseController
    {
        private static IEnumerable<Role> _roles = null;
        private static IEnumerable<UserStatus> _userStatuses = null;
        public UserController()
        {
            _roles = RoleService.GetActiveRoles();
            _userStatuses = UserStatusService.GetUserStatuses();
        }
        // GET: User
        public ActionResult Index()
        {
            //var users = UserService.GetUsers().ToList();
            var users = UserViewService.GetUsers().ToList();
            return View("UserIndex", users);
        }

        // GET: User/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View("UserIndex");
        //}

        // GET: User/Create
        public ActionResult New()
        {
            var viewModel = new UserFormViewModel
            {
                Role = _roles,
                UserStatus = _userStatuses,
                User = new User(),
            };
            return View("UserForm", viewModel);
        }
        // GET: User/Edit/5
        public ActionResult Edit(string id)
        {
            var user = UserService.GetUser(id);
            if (user == null) return HttpNotFound();
            var viewModel = new UserFormViewModel
            {
                Role = _roles,
                UserStatus = _userStatuses,
                User = user,
            };
            return View("UserForm", viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(User user)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new UserFormViewModel
                {
                    Role = _roles,
                    UserStatus = _userStatuses,
                    User = user,
                };
                return View("UserForm", viewModel);
            }
            if (user.UserId == null)
            {
                user.UserId = Guid.NewGuid().ToString();
                user.CreatedDate = DateTime.Now;
                user.Password = PasswordManager.ReturnHashPassword(user.Password);
                user.CreatedBy = 0;
                user.LastUpdatedBy = 0;
                var result = UserService.Save(user);
            }
            else
            {
                var userInDb = UserService.GetUser(user.UserId);
                if (userInDb == null) return HttpNotFound();
                userInDb.UserStatusId = user.UserStatusId;
                userInDb.RoleId = user.RoleId;
                userInDb.EmailAddress = user.EmailAddress;
                userInDb.LastUpdatedBy = user.LastUpdatedBy;
                userInDb.LastUpdatedTime = DateTime.Now;
                var result = UserService.Update(userInDb);
            }
            return RedirectToAction("Index", "User");
        }


      
        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View("UserIndex");
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View("UserIndex");
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View("UserIndex");
            }
        }
    }
}
