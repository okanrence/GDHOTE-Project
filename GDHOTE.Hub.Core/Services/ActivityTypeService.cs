using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.Core.Models;

namespace GDHOTE.Hub.Core.Services
{
    public class ActivityTypeService : BaseService
    {
        public static string Save(ActivityType activityType)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Insert(activityType);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.Log(EnumsService.LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                if (ex.Message.Contains("The duplicate key")) return "Cannot Insert duplicate record";
                return "Error occured while trying to insert ActivityType";
            }
        }
        public static IEnumerable<ActivityType> GetActivityTypes()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var countries = db.Fetch<ActivityType>().Where(c => c.Status == "A").OrderBy(c => c.Description);
                    return countries;
                }
            }
            catch (Exception ex)
            {
                LogService.Log(EnumsService.LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                return new List<ActivityType>();
            }
        }
        public static ActivityType GetActivityType(int id)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var activityType = db.Fetch<ActivityType>().SingleOrDefault(c => c.ActivityTypeId == id);
                    return activityType;
                }
            }
            catch (Exception ex)
            {
                LogService.Log(EnumsService.LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                return new ActivityType();
            }
        }
        public static string Update(ActivityType activityType)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Update(activityType);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.Log(EnumsService.LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                return "Error occured while trying to update ActivityType";
            }
        }
        public static string Delete(int id)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Delete<ActivityType>(id);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.Log(EnumsService.LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                return "Error occured while trying to delete record";
            }
        }
    }
}
