using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.CoreObject.Enumerables;

namespace GDHOTE.Hub.Core.Services
{
    public class SequenceService : BaseService
    {
        public static int GetNextMaleSequence()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.ExecuteScalar<int>("sp_HUB_ReturnNextMaleSequence");
                    return result;
                }
            }
            catch (Exception ex)
            {
                LogService.Log(LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                return 0;
            }
        }
        public static int GetNextFemaleSequence()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.ExecuteScalar<int>("EXEC sp_HUB_ReturnNextFemaleSequence");
                    return result;
                }
            }
            catch (Exception ex)
            {
                LogService.Log(LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                return 0;
            }
        }
    }
}
