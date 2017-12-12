using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.Core.Services;

namespace GDHOTE.Hub.Core.BusinessLogic
{
    public class UtilityManager
    {
        public static string DeployedAppName()
        {
          
            string appName = "";
            appName = BaseService.UseLive() == "Y" ? "/gdhote" : "";
            return appName;
        }
    }
}
