using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using GDHOTE.Hub.BusinessCore.Services;
using GDHOTE.Hub.PortalCore.Integrations;
using GDHOTE.Hub.CoreObject.Models;

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
        public ActionResult Login(LoginRequest loginRequest, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(loginRequest);
            }

            //var integration = new LoginIntegration(loginRequest.UserName, loginRequest.Password);
            //TokenResponse token = integration.Invoke();

            var result = UserService.AuthenticateUser(loginRequest);
            if (result != null)
            {
                if (!string.IsNullOrEmpty(result.UserName))
                {
                    FormsAuthentication.SetAuthCookie(result.UserName, false);
                    string roles = result.RoleId + "," + result.UserId;
                    var authTicket = new FormsAuthenticationTicket(1, result.UserName, DateTime.Now, DateTime.Now.AddMinutes(5), false, roles);
                    string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                    var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    HttpContext.Response.Cookies.Add(authCookie);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.LoginError = "Invalid username or password";
                }
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
            return RedirectToAction("Login", "Account");
        }
    }
}