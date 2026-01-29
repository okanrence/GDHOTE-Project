using System;
using System.Collections.Generic;
using System.Text;
using log4net;

namespace GDHOTE.Hub.BusinessCore.Services
{
    public class LogService1
    {
        private static readonly ILog log
            = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static void Log(string message, string location)
        {
             log.Info(location + " | " + message);
            //log.Info(location + " | " + message);

            //log.Info(location);

            //ILog myLog = LogManager.GetLogger(location);
            //myLog.Info(message);

        }
    }
}
