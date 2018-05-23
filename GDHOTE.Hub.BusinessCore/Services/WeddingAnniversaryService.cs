using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using GDHOTE.Hub.CoreObject.Enumerables;
using GDHOTE.Hub.CoreObject.Models;

namespace GDHOTE.Hub.BusinessCore.Services
{
    public class WeddingAnniversaryService : BaseService
    {
        public static void StartEmailProcess()
        {
            try
            {
                string appId = "WeddingAnniversaryEmail";

                //Run Between Specific hours
                string serviceRunTime = Get("settings.service.run.time");
                string startTime = serviceRunTime.Split('|')[0];
                string endTime = serviceRunTime.Split('|')[1];

                string currentDate = DateTime.Now.ToString("dd-MMM-yyy");

                DateTime startDate = DateTime.Parse(string.Format("{0} {1}", currentDate, startTime));
                DateTime endDate = DateTime.Parse(string.Format("{0} {1}", currentDate, endTime));
                DateTime currentTime = DateTime.Now;

                if (currentTime > startDate && currentTime < endDate)
                {
                    //Check if serivce has ran
                    var checker = CheckerService.GetCheckerByAppId(appId);


                    if (checker == null) return;

                    if (string.IsNullOrEmpty(checker.ApplicationId)) return;

                    if (checker.CheckDate.Date == DateTime.Now.Date) return;


                    string anniversaryDateString = DateTime.Now.ToString("dd-MMM-yyyy");

                    var memberList = MemberInfoService.GetMembersByWeddingAnniversary(anniversaryDateString);
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
                                EmailNotificationService.SendWeddingAnniversaryNotificationEmail(emailRequest, "");
                            }

                        }
                    }

                    //Update DB
                    checker.CheckDate = DateTime.Now;
                    checker.LastCheckDate = DateTime.Now;
                    CheckerService.Update(checker);
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
            }
        }


        public static void StartSmsProcess()
        {
            try
            {
                string appId = "WeddingAnniversarySms";
                string currentUser = "";

                //Run Between Specific hours
                string serviceRunTime = Get("settings.service.run.time");
                string startTime = serviceRunTime.Split('|')[0];
                string endTime = serviceRunTime.Split('|')[1];

                string currentDate = DateTime.Now.ToString("dd-MMM-yyy");

                DateTime startDate = DateTime.Parse(string.Format("{0} {1}", currentDate, startTime));
                DateTime endDate = DateTime.Parse(string.Format("{0} {1}", currentDate, endTime));
                DateTime currentTime = DateTime.Now;

                if (currentTime > startDate && currentTime < endDate)
                {
                    //Check if serivce has ran
                    var checker = CheckerService.GetCheckerByAppId(appId);


                    if (checker == null) return;

                    if (string.IsNullOrEmpty(checker.ApplicationId)) return;

                    if (checker.CheckDate.Date == DateTime.Now.Date) return;


                    string anniversaryDateString = DateTime.Now.ToString("dd-MMM-yyyy");

                    var memberList = MemberInfoService.GetMembersByWeddingAnniversary(anniversaryDateString);
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
                                            Message = "Dear Mr & Mrs " + member.Surname + ", your details has been successfully submitted.",
                                            MobileNumber = member.MobileNumber
                                        };
                                        SmsNotificationService.SendMessage(req, currentUser);
                                    }).Start();
                                }
                            }

                        }
                    }

                    //Update DB
                    checker.CheckDate = DateTime.Now;
                    checker.LastCheckDate = DateTime.Now;
                    CheckerService.Update(checker);

                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
            }
        }
    }
}
