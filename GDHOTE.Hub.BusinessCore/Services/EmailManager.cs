using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using GDHOTE.Hub.CoreObject.Models;
using GDHOTE.Hub.BusinessCore.Services;
using Postal;

namespace GDHOTE.Hub.BusinessCore.Services
{
    public class EmailManager : BaseService
    {
        public static void SendForEmailConfirmation(EmailRequest emailRequest)
        {
            //var viewsPath = HttpContext.Current.Server.MapPath(BaseService.Get("settings.email.template.folder"));
            var viewsPath = Path.GetFullPath(Get("settings.email.template.folder"));
            var engines = new ViewEngineCollection {new FileSystemRazorViewEngine(viewsPath)};
            var service = new EmailService(engines);
            dynamic email = new Email("RegistrationConfirmation");
            email.To = emailRequest.RecipientEmailAddress;
            email.Subject = emailRequest.Data["Subject"];
            email.FirstName = emailRequest.Data["FirstName"];
            email.LastName = emailRequest.Data["LastName"];
            service.Send(email);
        }
    }
}