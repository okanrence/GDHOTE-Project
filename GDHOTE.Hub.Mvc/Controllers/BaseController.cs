using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using GDHOTE.Hub.Core.Services;

namespace GDHOTE.Hub.Mvc.Controllers
{
    public class BaseController : Controller
    {
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            //var service = new MenuService();
            var userService = new UserService();

            //Get UserMenu

            if (string.IsNullOrEmpty(User.Identity.Name))
            {
                //FormsAuthentication.SignOut();
                RedirectToAction("Login", "Account");
            }
            ViewBag.LayoutModel = "";//service.getMenuByUsername(User.Identity.Name);
            var adUser = "";//userService.RetrieveUserData(User.Identity.Name);

            ViewBag.UserData = adUser;
        }

    }
}