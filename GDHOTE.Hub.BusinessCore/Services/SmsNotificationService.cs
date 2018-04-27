using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.BusinessCore.Exceptions;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using GDHOTE.Hub.CoreObject.Enumerables;
using GDHOTE.Hub.CoreObject.Models;

namespace GDHOTE.Hub.BusinessCore.Services
{
    public class SmsNotificationService : BaseService
    {
        public static Response SendMessage(SmsMessageRequest request, string currentUser)
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


                var webClient = new WebClient();
                var url = Get("settings.notification.sms.baseurl") +
                          "username=" + Get("settings.notification.sms.username") +
                          "&password=" + Get("settings.notification.sms.password") +
                          "&sender=" + Get("settings.notification.sms.sender") +
                          "&recipient=" + request.MobileNumber +
                          "&message=" + request.Message;

                webClient.DownloadString(url);

                //Log Notification
                new Task(() =>
                {
                    using (var db = GdhoteConnection())
                    {
                        var notification = new Notification
                        {
                            Recipient = request.MobileNumber,
                            NotificationTypeId = (int)NotificationType.Sms,
                            ContentBody = request.Message,
                            Status = 'S',
                            CreatedById = user.Id,
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
    }
}
