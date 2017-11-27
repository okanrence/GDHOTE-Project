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
    public class UserController : Controller
    {
        private static IEnumerable<Role> _roles =null;
        private static IEnumerable<UserStatus> _userStatuses =null;
        public UserController()
        {
            _roles = RolesService.GetRoles();
            _userStatuses = UserStatusService.GetUserStatuses();
        }
        // GET: User
        public ActionResult Index()
        {
            var users = UserService.GetUsers().ToList();

            foreach (var user in users)
            {
               
            }
            return View(users);
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View("Index");
        }

        // GET: User/Create
        public ActionResult NewUser()
        {
           

            var viewModel = new UserFormViewModel
            {
                 Roles = _roles,
                 UserStatus = _userStatuses,
                User = new User()
            };

            return View("UserForm", viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(User user)
        {
            if (!ModelState.IsValid)
            {

                var viewModel = new UserFormViewModel()
                {
                    Roles = _roles,
                    UserStatus = _userStatuses,
                    
                    User = user,
                };
                return View("UserForm", viewModel);
            }
            if (user.UserId == 0)
            {
                user.CreatedDate = DateTime.Now;
                user.Password = CommonServices.CreateHash(user.Password, EnumsService.HashTypes.Sha256,
                    EnumsService.HashEncoding.Hex);
                UserService.Save(user);
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
                UserService.Update(userInDb);
            }
            return RedirectToAction("Index", "User");
        }
    

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            return View("Index");
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
                return View("Index");
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View("Index");
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
                return View("Index");
            }
        }
    }
}
