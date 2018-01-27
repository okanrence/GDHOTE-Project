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
    public class StateService : BaseService
    {
        public static string Save(State state)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Insert(state);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.Log(LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                if (ex.Message.Contains("The duplicate key")) return "Cannot Insert duplicate record";
                return "Error occured while trying to insert state";
            }
        }

        public static IEnumerable<State> GetStates()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var states = db.Fetch<State>().OrderBy(s => s.StateName);
                    //var states = db.Query<State>().Include(s => s.Country).ToList();//.OrderBy(s => s.StateName).ToList();
                    return states;
                }
            }
            catch (Exception ex)
            {
                LogService.Log(LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                return new List<State>();
            }
        }
        public static State GetState(int id)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var state = db.Fetch<State>().SingleOrDefault(s => s.StateId == id);
                    return state;
                }
            }
            catch (Exception ex)
            {
                LogService.Log(LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                return new State();
            }
        }
        public static string Update(State state)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Update(state);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.Log(LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                return "Error occured while trying to update state";
            }
        }
        public static string Delete(int id)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Delete<State>(id);
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
