using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.Core.Models;

namespace GDHOTE.Hub.Core.Services
{
    public class YearGroupService : BaseService
    {
        public static string Save(YearGroup yearGroup)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Insert(yearGroup);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.Log(EnumsService.LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                if (ex.Message.Contains("The duplicate key")) return "Cannot Insert duplicate record";
                return "Error occured while trying to insert YearGroup";
            }
        }
        public static IEnumerable<YearGroup> GetYearGroups()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var countries = db.Fetch<YearGroup>().Where(c => c.Status == "A").OrderBy(c => c.Name);
                    return countries;
                }
            }
            catch (Exception ex)
            {
                LogService.Log(EnumsService.LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                return new List<YearGroup>();
            }
        }
        public static YearGroup GetYearGroup(int id)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var yearGroup = db.Fetch<YearGroup>().SingleOrDefault(c => c.Id == id);
                    return yearGroup;
                }
            }
            catch (Exception ex)
            {
                LogService.Log(EnumsService.LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                return new YearGroup();
            }
        }
        public static string Update(YearGroup yearGroup)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Update(yearGroup);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.Log(EnumsService.LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                return "Error occured while trying to update YearGroup";
            }
        }
        public static string Delete(int id)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Delete<YearGroup>(id);
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
