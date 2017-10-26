using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.Core.Models;

namespace GDHOTE.Hub.Core.Services
{
    public class StateService : BaseService
    {
        public void SaveState(State state)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Insert(state);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static IEnumerable<State> GetStates()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var states = db.Query<State>("select * from GDH_States");
                    return states;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
