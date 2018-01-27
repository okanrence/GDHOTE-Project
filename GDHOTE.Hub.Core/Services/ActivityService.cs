using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.Core.Models;
using GDHOTE.Hub.Core.Enumerables;
namespace GDHOTE.Hub.Core.Services
{
    public class ActivityService : BaseService
    {
        public static string Save(Activity activity)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Insert(activity);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.Log(LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                if (ex.Message.Contains("The duplicate key")) return "Cannot Insert duplicate record";
                return "Error occured while trying to insert Activity";
            }
        }
        public static IEnumerable<Activity> GetActivities()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var activities = db.Fetch<Activity>().OrderBy(a => a.RecordDate);
                    return activities;
                }
            }
            catch (Exception ex)
            {
                LogService.Log(LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                return new List<Activity>();
            }
        }
        public static IEnumerable<Activity> GetMemberActivities(int memberKey)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var activities = db.Fetch<Activity>().Where(a => a.MemberKey == memberKey).OrderBy(a => a.StartDate);
                    return activities;
                }
            }
            catch (Exception ex)
            {
                LogService.Log(LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                return new List<Activity>();
            }
        }
        public static Activity GetActivity(int id)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var activity = db.Fetch<Activity>().SingleOrDefault(c => c.ActivityId == id);
                    return activity;
                }
            }
            catch (Exception ex)
            {
                LogService.Log(LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                return new Activity();
            }
        }
        public static string Update(Activity activity)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Update(activity);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.Log(LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                return "Error occured while trying to update Activity";
            }
        }
        public static string Delete(int id)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Delete<Activity>(id);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.Log(LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                return "Error occured while trying to delete record";
            }
        }
    }
}
