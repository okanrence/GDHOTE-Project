using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.CommonServices.BusinessLogic;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using GDHOTE.Hub.CoreObject.Models;
using GDHOTE.Hub.PortalCore.Integrations;
using GDHOTE.Hub.PortalCore.Models;
using GDHOTE.Hub.PortalCore.Services;

namespace GDHOTE.Hub.WindowsService
{
    public class BirthdayAnniversaryManager
    {
        private static readonly string Username = ConfigurationManager.AppSettings["settings.service.username"];
        private static readonly string Password = ConfigurationManager.AppSettings["settings.service.password"];

        public static void StartEmailProcess()
        {
            try
            {
                string appId = "BirthdayAnniversaryEmail";
                var token = new Token();

                //Run Between Specific hours
                string serviceRunTime = ConfigurationManager.AppSettings["settings.service.run.time"];
                string startTime = serviceRunTime.Split('|')[0];
                string endTime = serviceRunTime.Split('|')[1];

                string currentDate = DateTime.Now.ToString("dd-MMM-yyy");

                DateTime startDate = DateTime.Parse(string.Format("{0} {1}", currentDate, startTime));
                DateTime endDate = DateTime.Parse(string.Format("{0} {1}", currentDate, endTime));
                DateTime currentTime = DateTime.Now;

                if (currentTime > startDate && currentTime < endDate)
                {
                    //Authenticate user
                    var integration = new LoginIntegration(Username, Password);
                    TokenResponse tokenResponse = integration.Invoke();

                    if (tokenResponse != null)
                    {
                        if (!string.IsNullOrEmpty(tokenResponse.AccessToken))
                        {
                            ErrorLogManager.LogError(MethodBase.GetCurrentMethod().Name, "No Authentication response");
                        }
                    }

                    token.AuthToken = tokenResponse.AccessToken;
                    token.RefreshToken = tokenResponse.RefreshToken;


                    //Check if serivce has ran
                    var checker = PortalCheckerService.GetChecker(appId, token);

                    if (checker == null) return;

                    if (string.IsNullOrEmpty(checker.ApplicationId)) return;

                    if (checker.CheckDate.Date == DateTime.Now.Date) return;


                    string dateOfBirthString = DateTime.Now.ToString("dd-MMM-yyyy");

                    var memberList = PortalMemberService.GetMembersByBirthdayAnniversary(dateOfBirthString, token);

                    if (memberList != null)
                    {
                        if (memberList.Count > 0)
                        {
                            foreach (var member in memberList)
                            {
                                var emailRequest = new SendEmailRequest
                                {
                                    MemberId = member.MemberId,
                                    Subject = "Happy Birthday",
                                    Firstname = member.FirstName,
                                    Surname = member.Surname,
                                    MailBody = "",
                                    RecipientEmailAddress = member.EmailAddress

                                };
                                PortalNotificationService.SendBirthdayNotificationEmail(emailRequest, token);
                            }
                        }
                    }

                    //Update DB
                    var result = PortalCheckerService.UpdateChecker(appId, token);
                }

            }
            catch (Exception ex)
            {
                ErrorLogManager.LogError(MethodBase.GetCurrentMethod().Name, ex);
            }
        }


        public static void StartSmsProcess()
        {
            try
            {
                string appId = "BirthdayAnniversarySms";
                var token = new Token();

                //Run Between Specific hours
                string serviceRunTime = ConfigurationManager.AppSettings["settings.service.run.time"];
                string startTime = serviceRunTime.Split('|')[0];
                string endTime = serviceRunTime.Split('|')[1];

                string currentDate = DateTime.Now.ToString("dd-MMM-yyy");

                DateTime startDate = DateTime.Parse(string.Format("{0} {1}", currentDate, startTime));
                DateTime endDate = DateTime.Parse(string.Format("{0} {1}", currentDate, endTime));
                DateTime currentTime = DateTime.Now;

                if (currentTime > startDate && currentTime < endDate)
                {
                    //Authenticate user
                    var integration = new LoginIntegration(Username, Password);
                    TokenResponse tokenResponse = integration.Invoke();
                    token.AuthToken = tokenResponse.AccessToken;
                    token.RefreshToken = tokenResponse.RefreshToken;


                    //Check if serivce has ran
                    var checker = PortalCheckerService.GetChecker(appId, token);

                    if (checker == null) return;

                    if (string.IsNullOrEmpty(checker.ApplicationId)) return;

                    if (checker.CheckDate.Date == DateTime.Now.Date) return;


                    string dateOfBirthString = DateTime.Now.ToString("dd-MMM-yyyy");

                    var memberList = PortalMemberService.GetMembersByBirthdayAnniversary(dateOfBirthString, token);

                    if (memberList != null)
                    {
                        if (memberList.Count > 0)
                        {
                            foreach (var member in memberList)
                            {
                                if (!string.IsNullOrEmpty(member.MobileNumber))
                                {
                                    new Task(() =>
                                    {
                                        var req = new SendSmsRequest
                                        {
                                            Message = "Happy Birthday " + member.FirstName + " " + member.Surname + ". Have a wonderful day",
                                            MobileNumber = member.MobileNumber
                                        };
                                        PortalNotificationService.SendSms(req, token);
                                    }).Start();
                                }
                            }

                        }
                    }

                    //Update DB
                    var result = PortalCheckerService.UpdateChecker(appId, token);
                }

            }
            catch (Exception ex)
            {
                ErrorLogManager.LogError(MethodBase.GetCurrentMethod().Name, ex);
            }
        }
    }
}
