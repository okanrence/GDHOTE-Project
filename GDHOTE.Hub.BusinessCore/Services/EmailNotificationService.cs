using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using GDHOTE.Hub.BusinessCore.Exceptions;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using GDHOTE.Hub.CoreObject.Models;
using GDHOTE.Hub.CoreObject.Enumerables;

using Postal;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.WebPages;

namespace GDHOTE.Hub.BusinessCore.Services
{
    public class EmailNotificationService : BaseService
    {
        private static readonly string EmailTemplatePath = AppDomain.CurrentDomain.BaseDirectory + Get("settings.email.template.folder");
        private static readonly string BlindCopy = Get("settings.email.blind.copy");

        private static Response SendNotificationEmail(EmailRequest emailRequest, string currentUser)
        {
            try
            {
                var response = new Response();


                //Get User Initiating Creation Request
                var user = UserService.GetUserByUserName(currentUser);

                if (user == null)
                {
                    return new Response
                    {
                        ErrorCode = "01",
                        ErrorMessage = "Unable to validate User"
                    };
                }


                var viewsPath = Path.GetFullPath(EmailTemplatePath);
                var engines = new ViewEngineCollection { new FileSystemRazorViewEngine(viewsPath) };
                var service = new EmailService(engines);
                dynamic email = new Email(emailRequest.Type.ToString());
                email.To = emailRequest.RecipientEmailAddress;
                email.Bcc = BlindCopy;
                email.Subject = emailRequest.Subject;
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
                            Recipient = emailRequest.RecipientEmailAddress,
                            NotificationTypeId = (int)NotificationType.Email,
                            ContentBody = emailRequest.Type.ToString(),
                            Status = 'S',
                            CreatedById = user.UserId,
                            DateCreated = DateTime.Now,
                            RecordDate = DateTime.Now
                        };
                        db.Insert(notification);
                    }

                }).Start();

                response = new Response
                {
                    ErrorCode = "00",
                    ErrorMessage = "Successful"
                };
                return response;
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                var response = new Response
                {
                    ErrorCode = "01",
                    ErrorMessage = "Error occured Sending message"
                };
                return response;
            }

        }


        public static Response SendRegistrationConfirmationEmail(EmailRequest emailRequest, string currentUser)
        {
            try
            {
                var response = new Response();


                //Get User Initiating Creation Request
                var user = UserService.GetUserByUserName(currentUser);

                if (user == null)
                {
                    return new Response
                    {
                        ErrorCode = "01",
                        ErrorMessage = "Unable to validate User"
                    };
                }


                var viewsPath = Path.GetFullPath(EmailTemplatePath);
                var engines = new ViewEngineCollection { new FileSystemRazorViewEngine(viewsPath) };
                var service = new EmailService(engines);
                dynamic email = new Email("RegistrationConfirmation");
                email.To = emailRequest.RecipientEmailAddress;
                email.Subject = emailRequest.Subject;
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
                            Recipient = emailRequest.RecipientEmailAddress,
                            NotificationTypeId = (int)NotificationType.Email,
                            ContentBody = emailRequest.Type.ToString(),
                            Status = 'S',
                            CreatedById = user.UserId,
                            DateCreated = DateTime.Now,
                            RecordDate = DateTime.Now
                        };
                        db.Insert(notification);
                    }

                }).Start();

                response = new Response
                {
                    ErrorCode = "00",
                    ErrorMessage = "Successful"
                };
                return response;
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                var response = new Response
                {
                    ErrorCode = "01",
                    ErrorMessage = "Error occured Sending message"
                };
                return response;
            }
        }

        public static Response SendNewUserEmail(EmailRequest emailRequest, string currentUser)
        {
            try
            {
                var response = new Response();


                //Get User Initiating Creation Request
                var user = UserService.GetUserByUserName(currentUser);

                if (user == null)
                {
                    return new Response
                    {
                        ErrorCode = "01",
                        ErrorMessage = "Unable to validate User"
                    };
                }


                var viewsPath = Path.GetFullPath(EmailTemplatePath);
                var engines = new ViewEngineCollection { new FileSystemRazorViewEngine(viewsPath) };
                var service = new EmailService(engines);
                dynamic email = new Email("NewAdminUser");
                email.To = emailRequest.RecipientEmailAddress;
                email.Subject = emailRequest.Subject;
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
                            Recipient = emailRequest.RecipientEmailAddress,
                            NotificationTypeId = (int)NotificationType.Email,
                            ContentBody = emailRequest.Type.ToString(),
                            Status = 'S',
                            CreatedById = user.UserId,
                            DateCreated = DateTime.Now,
                            RecordDate = DateTime.Now
                        };
                        db.Insert(notification);
                    }

                }).Start();

                response = new Response
                {
                    ErrorCode = "00",
                    ErrorMessage = "Successful"
                };
                return response;
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                var response = new Response
                {
                    ErrorCode = "01",
                    ErrorMessage = "Error occured Sending message"
                };
                return response;
            }
        }
        public static Response SendPasswordResetEmail(EmailRequest emailRequest, string currentUser)
        {
            try
            {
                var response = new Response();

                //Get User Initiating Creation Request
                var user = UserService.GetUserByUserName(currentUser);

                if (user == null)
                {
                    return new Response
                    {
                        ErrorCode = "01",
                        ErrorMessage = "Unable to validate User"
                    };
                }


                var viewsPath = Path.GetFullPath(EmailTemplatePath);
                var engines = new ViewEngineCollection { new FileSystemRazorViewEngine(viewsPath) };
                var service = new EmailService(engines);
                dynamic email = new Email("PasswordReset");
                email.To = emailRequest.RecipientEmailAddress;
                email.Subject = emailRequest.Subject;
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
                            Recipient = emailRequest.RecipientEmailAddress,
                            NotificationTypeId = (int)NotificationType.Email,
                            ContentBody = emailRequest.Type.ToString(),
                            Status = 'S',
                            CreatedById = user.UserId,
                            DateCreated = DateTime.Now,
                            RecordDate = DateTime.Now
                        };
                        db.Insert(notification);
                    }

                }).Start();

                response = new Response
                {
                    ErrorCode = "00",
                    ErrorMessage = "Successful"
                };
                return response;
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                var response = new Response
                {
                    ErrorCode = "01",
                    ErrorMessage = "Error occured Sending message"
                };
                return response;
            }
        }

        private static Response SendGenericEmail(EmailRequest emailRequest, string currentUser)
        {
            try
            {
                var response = new Response();


                //Get User Initiating Creation Request
                var user = UserService.GetUserByUserName(currentUser);

                if (user == null)
                {
                    return new Response
                    {
                        ErrorCode = "01",
                        ErrorMessage = "Unable to validate User"
                    };
                }


                var viewsPath = Path.GetFullPath(EmailTemplatePath);
                var engines = new ViewEngineCollection { new FileSystemRazorViewEngine(viewsPath) };
                var service = new EmailService(engines);
                dynamic email = new Email("GenericEmail");
                email.To = emailRequest.RecipientEmailAddress;
                email.Subject = emailRequest.Subject;
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
                            Recipient = emailRequest.RecipientEmailAddress,
                            NotificationTypeId = (int)NotificationType.Email,
                            ContentBody = emailRequest.Type.ToString(),
                            Status = 'S',
                            CreatedById = user.UserId,
                            DateCreated = DateTime.Now,
                            RecordDate = DateTime.Now
                        };
                        db.Insert(notification);
                    }

                }).Start();

                response = new Response
                {
                    ErrorCode = "00",
                    ErrorMessage = "Successful"
                };
                return response;
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                var response = new Response
                {
                    ErrorCode = "01",
                    ErrorMessage = "Error occured Sending message"
                };
                return response;
            }
        }

        public static Response SendBirthdayNotificationEmail(SendEmailRequest sendRequest, string currentUser)
        {
            var response = new Response();

            var emailRequest = new EmailRequest
            {
                RecipientEmailAddress = sendRequest.RecipientEmailAddress,
                Subject = "Happy Birthday",
                Type = EmailType.BirthdayNotification,
                Data = new Hashtable
                {
                    //["Subject"] = "Welcome to " + Get("settings.organisation.name"),
                    ["FirstName"] = sendRequest.Firstname,
                    ["LastName"] = sendRequest.Surname,
                }
            };
            response = SendNotificationEmail(emailRequest, currentUser);
            return response;
        }
        public static Response SendWeddingAnniversaryNotificationEmail(SendEmailRequest sendRequest, string currentUser)
        {
            var response = new Response();

            var emailRequest = new EmailRequest
            {
                RecipientEmailAddress = sendRequest.RecipientEmailAddress,
                Subject = "Happy Wedding Anniversary",
                Type = EmailType.WeddingAnniversaryNotification,
                Data = new Hashtable
                {
                    //["Subject"] = "Welcome to " + Get("settings.organisation.name"),
                    ["FirstName"] = sendRequest.Firstname,
                    ["LastName"] = sendRequest.Surname,
                }
            };
            response = SendNotificationEmail(emailRequest, currentUser);
            return response;
        }

        public static Response SendMail(SendEmailRequest sendRequest, string currentUser)
        {
            var response = new Response();

            var emailRequest = new EmailRequest
            {
                RecipientEmailAddress = sendRequest.RecipientEmailAddress,
                Subject = sendRequest.Subject,
                Type = EmailType.GenericEmail,
                Data = new Hashtable
                {
                    ["Content"] = sendRequest.MailBody
                }
            };
            response = SendGenericEmail(emailRequest, currentUser);
            return response;
        }
    }
}