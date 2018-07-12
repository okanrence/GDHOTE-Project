using System;
using System.Configuration;
using System.Reflection;
using System.Threading.Tasks;
using GDHOTE.Hub.CommonServices.BusinessLogic;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using GDHOTE.Hub.CoreObject.Models;
using GDHOTE.Hub.PortalCore.Integrations;
using GDHOTE.Hub.PortalCore.Models;
using GDHOTE.Hub.PortalCore.Services;

namespace GDHOTEindowsForm
{
    public class WeddingAnniversaryManager
    {
        private static readonly string Username = ConfigurationManager.AppSettings["settings.service.username"];
        private static readonly string Password = ConfigurationManager.AppSettings["settings.service.password"];
        public static void StartEmailProcess()
        {
            try
            {
                string appId = "WeddingAnniversaryEmail";
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


                    string anniversaryDateString = DateTime.Now.ToString("dd-MMM-yyyy");

                    var memberList = PortalMemberService.GetMembersByWeddingAnniversary(anniversaryDateString, token);

                    if (memberList != null)
                    {
                        if (memberList.Count > 0)
                        {
                            foreach (var member in memberList)
                            {
                                var emailRequest = new SendEmailRequest
                                {
                                    MemberId = member.MemberId,
                                    Subject = "Happy Wedding Anniversary",
                                    Firstname = member.FirstName,
                                    Surname = member.Surname,
                                    MailBody = "",
                                    RecipientEmailAddress = member.EmailAddress

                                };
                                PortalNotificationService.SendWeddingAnniversaryNotificationEmail(emailRequest);
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
                string appId = "WeddingAnniversarySms";
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

                    string anniversaryDateString = DateTime.Now.ToString("dd-MMM-yyyy");

                    var memberList = PortalMemberService.GetMembersByWeddingAnniversary(anniversaryDateString, token);
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
                                        var req = new SmsMessageRequest
                                        {
                                            Message = "Happy Wedding Annivesary " + member.Surname + ". God bless you both",
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
