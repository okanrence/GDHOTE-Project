using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
                    var result = DateTime.Now.ToString("ffffff"); 
                    return int.Parse(result);
                }
            }
            catch (Exception ex)
            {
                LogService.Log(EnumsService.LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                return 0;// "Error occured while trying to update Country";
            }
        }
        public static int GetNextFemaleSequence()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = DateTime.Now.ToString("fffff"); 
                    return int.Parse(result);
                }
            }
            catch (Exception ex)
            {
                LogService.Log(EnumsService.LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                return 0;// "Error occured while trying to update Country";
            }
        }
    }
}
