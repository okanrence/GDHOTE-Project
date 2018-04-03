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
using GDHOTE.Hub.CoreObject.Enumerables;
using Postal;

namespace GDHOTE.Hub.BusinessCore.Services
{
    public class EmailNotificationService : BaseService
    {
        private static readonly string EmailTemplatePath = Get("settings.email.template.folder");
        public static void SendEmailNotification(EmailRequest emailRequest)
        {
            var viewsPath = Path.GetFullPath(EmailTemplatePath);
            var engines = new ViewEngineCollection { new FileSystemRazorViewEngine(viewsPath) };
            var service = new EmailService(engines);
            dynamic email = new Email(emailRequest.Type.ToString());
            email.To = emailRequest.RecipientEmailAddress;
            email.Subject = emailRequest.Data["Subject"];
            email.FirstName = emailRequest.Data["FirstName"];
            email.LastName = emailRequest.Data["LastName"];
            service.Send(email);


            //Log Notification
            new Task(() =>
            {
                using (var db = GdhoteConnection())
                {
                    var notification = new Notification
                    {
                        MemberId = emailRequest.MemberId,
                        NotificationTypeId = (int)NotificationType.Email,
                        ContentBody = emailRequest.Type.ToString(),
                        Status = 'S',
                        DateCreated = DateTime.Now
                    };
                    db.Insert(notification);
                }

            }).Start();
        }
        public static void SendEmailConfirmation(EmailRequest emailRequest)
        {
            var viewsPath = Path.GetFullPath(EmailTemplatePath);
            var engines = new ViewEngineCollection { new FileSystemRazorViewEngine(viewsPath) };
            var service = new EmailService(engines);
            dynamic email = new Email("RegistrationConfirmation");
            email.To = emailRequest.RecipientEmailAddress;
            email.Subject = emailRequest.Data["Subject"];
            email.FirstName = emailRequest.Data["FirstName"];
            email.LastName = emailRequest.Data["LastName"];
            service.Send(email);

            //Log Notification
            new Task(() =>
            {
                using (var db = GdhoteConnection())
                {
                    var notification = new Notification
                    {
                        MemberId = emailRequest.MemberId,
                        NotificationTypeId = (int)NotificationType.Email,
                        ContentBody = emailRequest.Type.ToString(),
                        Status = 'S',
                        DateCreated = DateTime.Now
                    };
                    db.Insert(notification);
                }

            }).Start();

        }
    }
}