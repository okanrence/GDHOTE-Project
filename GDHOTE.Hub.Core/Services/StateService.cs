using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.Core.Models;

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
                if (ex.Message.Contains("The duplicate key")) throw new Exception("Cannot Insert duplicate record");
                throw new Exception("Error occured while trying to insert state");
            }
        }

        public static IEnumerable<State> GetStates()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var states = db.Fetch<State>();
                    return states;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static State GetState(int id)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var state = db.Fetch<State>().SingleOrDefault(s => s.Id == id);
                    return state;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static int Update(State state)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Update(state);
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static int Delete(int id)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Delete<State>(id);
                    return result;
                }
            }
            catch (Exception ex)
            {
                LogService.Log(EnumsService.LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                throw new Exception("Error occured while trying to delete record");
            }
        }
    }
}
