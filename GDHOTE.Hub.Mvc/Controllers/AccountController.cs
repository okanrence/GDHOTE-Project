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
using GDHOTE.Hub.PortalCore.Services;

namespace GDHOTE.Hub.Mvc.Controllers
{
    //[Authorize]
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
        public ActionResult Login(AdminLoginRequest loginRequest, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(loginRequest);
            }

            var integration = new LoginIntegration(loginRequest.EmailAddress, loginRequest.Password);
            TokenResponse result = integration.Invoke();
            if (result != null)
            {
                if (!string.IsNullOrEmpty(result.AccessToken))
                {

                    FormsAuthentication.SetAuthCookie(loginRequest.EmailAddress, false);
                    string roles = result.RoleId;
                    Session["RefreshToken"] = result.RefreshToken;
                    Session["AccessToken"] = result.AccessToken;

                    var authTicket = new FormsAuthenticationTicket(1, loginRequest.EmailAddress, DateTime.Now,
                        DateTime.Now.AddMinutes(30), false, roles);
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

        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(PasswordResetRequest resetRequest)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            resetRequest.ChannelId = (int)CoreObject.Enumerables.Channel.Web;
            var result = PortalUserService.StartPasswordReset(resetRequest);
            if (result != null)
            {
                //Successful
                if (result.ErrorCode == "00")
                {
                    return RedirectToAction("ForgotPasswordConfirmation");

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