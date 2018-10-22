using System;
using System.Linq;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using GDHOTE.Hub.CoreObject.Models;

namespace GDHOTE.Hub.BusinessCore.Services
{
    public class ServiceLogService : BaseService
    {
        public static string Save(ServiceLog request)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Insert(request);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                if (ex.Message.Contains("The duplicate key")) return "Cannot Insert duplicate record";
                return "Error occured while trying to insert ServiceLog";
            }
        }
        public static Response CreateServiceLog(CreateServiceLogRequest request, string currentUser)
        {
            try
            {
                using (var db = GdhoteConnection())
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

                    var serviceLog = new ServiceLog
                    {
                        ServiceId = request.ServiceId,
                        RecordCount = request.RecordCount,
                        RecordDate = DateTime.Now
                    };

                    db.Insert(serviceLog);
                    response = new Response
                    {
                        ErrorCode = "00",
                        ErrorMessage = "Successful"
                    };
                    return response;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                var response = new Response
                {
                    ErrorCode = "01",
                    ErrorMessage = "Error occured while trying to insert record"
                };
                return response;
            }

        }
    }
}
