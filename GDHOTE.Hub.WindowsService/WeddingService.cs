using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using GDHOTE.Hub.PortalCore.Services;

namespace GDHOTE.Hub.WindowsService
{
    public class WeddingService
    {
        public static void StartAnniversaryProcess()
        {
            try
            {
                string appId = "WeddingAnniversaryEmail";

                //Run Between Specific hours
                string serviceRunTime = "";//Get("settings.service.run.time");
                string startTime = serviceRunTime.Split('|')[0];
                string endTime = serviceRunTime.Split('|')[1];

                string currentDate = DateTime.Now.ToString("dd-MMM-yyy");

                DateTime startDate = DateTime.Parse(string.Format("{0} {1}", currentDate, startTime));
                DateTime endDate = DateTime.Parse(string.Format("{0} {1}", currentDate, endTime));
                DateTime currentTime = DateTime.Now;

                if (currentTime > startDate && currentTime < endDate)
                {
                    //Check if serivce has ran
                    var checker = PortalCheckerService.GetChecker(appId);

                    if (checker == null) return;

                    if (string.IsNullOrEmpty(checker.ApplicationId)) return;

                    if (checker.CheckDate.Date == DateTime.Now.Date) return;


                    string anniversaryDateString = DateTime.Now.ToString("dd-MMM-yyyy");

                    var memberList = PortalMemberService.GetMembersByWeddingAnniversary(anniversaryDateString);

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
                                //EmailNotificationService.SendWeddingAnniversaryNotificationEmail(emailRequest, "");
                            }

                        }
                    }

                    //Update DB
                    var result = PortalCheckerService.UpdateChecker(appId);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
