using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GDHOTE.Hub.CoreObject.Models;
using GDHOTE.Hub.CoreObject.ViewModels;

namespace GDHOTE.Hub.BusinessCore.Services
{
    public class CheckerService : BaseService
    {
        public static string Save(Checker checker)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Insert(checker);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                if (ex.Message.Contains("The duplicate key")) return "Cannot Insert duplicate record";
                return "Error occured while trying to insert Checker";
            }
        }

        public static List<Checker> GetActiveCheckers()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var checkers = db.Fetch<Checker>()
                        .Where(c => c.StatusId == (int)CoreObject.Enumerables.Status.Active)
                        .OrderBy(c => c.ApplicationId)
                        .ToList();
                    return checkers;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new List<Checker>();
            }
        }
        public static Checker GetCheckerByAppId(string applicationId)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var checker = db.Fetch<Checker>()
                        .SingleOrDefault(c => c.ApplicationId == applicationId);
                    return checker;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new Checker();
            }
        }
        public static string Update(Checker checker)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Update(checker);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                if (ex.Message.Contains("The duplicate key")) return "Cannot update duplicate record";
                return "Error occured while trying to update Checker";
            }
        }
    }
}
