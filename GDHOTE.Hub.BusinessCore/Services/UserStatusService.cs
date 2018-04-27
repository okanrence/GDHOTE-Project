using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using GDHOTE.Hub.CoreObject.Models;
using Newtonsoft.Json;

namespace GDHOTE.Hub.BusinessCore.Services
{
    public class UserStatusService : BaseService
    {

        public static string Save(UserStatus userStatus)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Insert(userStatus);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
               LogService.LogError(ex.Message);
                return ex.Message.Contains("The duplicate key") ? "Cannot Insert duplicate record" : "Error occured while trying to insert UserStatus";
            }
        }

        public static List<UserStatus> GetUserStatuses()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var userStatuses = db.Fetch<UserStatus>();
                    return userStatuses;
                }
            }
            catch (Exception ex)
            {
               LogService.LogError(ex.Message);
                return new List<UserStatus>();
            }
        }

        public static List<UserStatusResponse> GetActiveUserStatuses()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var userStatuses = db.Fetch<UserStatus>()
                        .ToList();
                    var item = JsonConvert.SerializeObject(userStatuses);
                    var response = JsonConvert.DeserializeObject<List<UserStatusResponse>>(item);
                    return response;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new List<UserStatusResponse>();
            }
        }
        public static UserStatus GetUserStatus(int id)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var userStatus = db.Fetch<UserStatus>().SingleOrDefault(s => s.Id == id);
                    return userStatus;
                }
            }
            catch (Exception ex)
            {
               LogService.LogError(ex.Message);
                return new UserStatus();
            }
        }

        public static string Update(UserStatus userStatus)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Update(userStatus);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
               LogService.LogError(ex.Message);
                return "Error occured while trying to update UserStatus";
            }
        }
        public static string Delete(int id)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Delete<UserStatus>(id);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
               LogService.LogError(ex.Message);
                return "Error occured while trying to delete record";
            }
        }
    }
}
