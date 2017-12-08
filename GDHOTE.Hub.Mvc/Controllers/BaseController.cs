using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
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


            if (string.IsNullOrEmpty(User.Identity.Name))
            {
                RedirectToAction("Login", "Account");
                return;
            }

            var currentUser = HttpContext.GetOwinContext().Authentication.User;
            var identity = (ClaimsIdentity)currentUser.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            string roleId = claims.SingleOrDefault(c => c.Type == identity.RoleClaimType)?.Value;

            //Get UserMenu
            var mainMenus = MainMenuService.GetMainMenus().ToList();
            var subMenus = RoleSubMenuViewService.GetRoleMenuByRole(roleId).ToList();

            //var subMenus = RoleSubMenuViewService.GetRoleMenu().ToList();

            ViewBag.MainMenu = mainMenus;
            ViewBag.SubMenu = subMenus;

        }

    }
}