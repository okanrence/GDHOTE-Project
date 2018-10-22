using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDHOTE.Hub.CommonServices.BusinessLogic
{
    public class UtilityManager
    {
        public static string DeployedAppName()
        {
            string appName = "";
            appName = ConfigurationManager.AppSettings["settings.deployed.app.path"];
            return appName;
        }
    }
}
