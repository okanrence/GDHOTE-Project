﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using GDHOTE.Hub.CommonServices.BusinessLogic;
using GDHOTE.Hub.PortalCore.Models;
using GDHOTE.Hub.PortalCore.Services;

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
            IEnumerable<Claim> claims = currentUser.Claims;
            //var roles = claims.ToList().Where(c => c.Type == ClaimTypes.Role).ToList();
            var userClaims = claims.Where(c => c.Type == ClaimTypes.Role).Select(c => new { c.Value }).ToArray();
            string roleId = "";
            if (userClaims != null)
            {
                roleId = claims.SingleOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            }


            //Get UserMenu
            var mainMenus = PortalMainMenuService.GetActiveMainMenus(SetToken());
            var subMenus = PortalRoleMenuService.GetRoleMenuByRole(roleId, SetToken()).ToList();

            ViewBag.MainMenu = mainMenus;
            ViewBag.SubMenu = subMenus;
            ViewBag.DeployedAppName = UtilityManager.DeployedAppName();

        }

        public Token SetToken()
        {
            string refreshToken = "";
            string authToken = "";
            if (Session["RefreshToken"] != null) refreshToken = Session["RefreshToken"].ToString();
            if (Session["AccessToken"] != null) authToken = Session["AccessToken"].ToString();

            var token = new Token
            {
                AuthToken = authToken,
                RefreshToken = refreshToken
            };
            return token;
        }
    }
}