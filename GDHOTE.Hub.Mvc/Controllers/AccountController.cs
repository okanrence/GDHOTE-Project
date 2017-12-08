using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using GDHOTE.Hub.Core.Models;
using GDHOTE.Hub.Core.Services;
using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using GDHOTE.Hub.Mvc.Models;

namespace GDHOTE.Hub.Mvc.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var authenticatedUser = new User();
            var result = UserService.LoginUser(model.UserName, model.Password, out authenticatedUser);
            if (result == EnumsService.SignInStatus.Success)
            {


                var myIdentity = new ClaimsIdentity(new[]
                    {
                        // adding following 2 claim just for supporting default antiforgery provider
                        new Claim(ClaimTypes.NameIdentifier, authenticatedUser.UserName),
                        //new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", 
                        //"ASP.NET Identity", "http://www.w3.org/2001/XMLSchema#string"),
                        
                        new Claim(ClaimTypes.Name, authenticatedUser.UserName),
                        new Claim(ClaimTypes.Role, authenticatedUser.RoleId),
                    }, DefaultAuthenticationTypes.ApplicationCookie
                   );
                HttpContext.GetOwinContext().Authentication.SignIn(new AuthenticationProperties { IsPersistent = false }, myIdentity);
                if (User.Identity.IsAuthenticated) return RedirectToAction("Index", "Home");

                //FormsAuthentication.SetAuthCookie(authenticatedUser.UserName, false);
                //var authTicket = new FormsAuthenticationTicket(1, authenticatedUser.UserName, DateTime.Now, DateTime.Now.AddMinutes(5), false, "");
                //string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                //var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                //HttpContext.Response.Cookies.Add(authCookie);
                //HttpContext..User = new System.Security.Principal.GenericPrincipal(new FormsIdentity(authTicket), "");
            }
            else
            {
                ViewBag.LoginError = "Login details are wrong.";
            }
            return View();
        }



        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }



        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            HttpContext.GetOwinContext().Authentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
    }
}