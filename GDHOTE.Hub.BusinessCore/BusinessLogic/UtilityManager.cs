using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.BusinessCore.Services;

namespace GDHOTE.Hub.BusinessCore.BusinessLogic
{
    public class UtilityManager
    {
        public static string DeployedAppName()
        {
            string appName = "";
            //appName = BaseService.UseLive() == "Y" ? BaseService.Get("settings.deployed.app.path") : "";
            appName = BaseService.Get("settings.deployed.app.path");
            return appName;
        }
    }
}
