using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using GDHOTE.Hub.CoreObject.Enumerables;
using System.Reflection;

namespace GDHOTE.Hub.BusinessCore.Services
{
    public class LogService
    {
        public string Message { get; set; }
        protected ILog Logger;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
         

        //protected LogService(string name)
        //{
        //    //Logger = LogManager.GetLogger(name);
        //    Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        //}
        public static void Log(string message)
        {
            log.Info(message);
        }

        //public void Log(LogType type, Exception ex, object message)
        //{
        //    Logger.Info("============ Logging from Service ================");
        //    if (type == LogType.Info) Logger.Info(message, ex);
        //    if (type == LogType.Fatal) Logger.Fatal(message, ex);
        //    if (type == LogType.Error) Logger.Error(message, ex);
        //    if (type == LogType.Warning) Logger.Warn(message, ex);
        //}

        //public static void Log(LogType type, string callerFormName, string methodName, Exception ex)
        //{
        //    if (type == LogType.Info) log.Info(methodName, ex);
        //    if (type == LogType.Fatal) log.Fatal(methodName, ex);
        //    if (type == LogType.Error) log.Error(methodName, ex);
        //    if (type == LogType.Warning) log.Warn(methodName, ex);
        //    //ILog myLog = LogManager.GetLogger(callerFormName + "|" + methodName);
        //    //myLog.Info("============ Logging from Service ================");
        //    //if (type == LogType.Info) myLog.Info("Message: " + ex.Message + "|StackTrace: " + ex.StackTrace);
        //    //if (type == LogType.Fatal) myLog.Fatal("Message: " + ex.Message + "|StackTrace: " + ex.StackTrace);
        //    //if (type == LogType.Error) myLog.Error(ex);
        //    //if (type == LogType.Warning) myLog.Warn("Message: " + ex.Message + "|StackTrace: " + ex.StackTrace);
        //}
        //public static void Log(LogType type, string callerFormName, string methodName, string message)
        //{
        //    if (type == LogType.Info) log.Info(message);
        //    //ILog myLog = LogManager.GetLogger(callerFormName + "|" + methodName);
        //    //myLog.Info("============ Logging from Service ================");
        //    //if (type == LogType.Info) myLog.Info(message);
        //    //if (type == LogType.Fatal) myLog.Fatal(message);
        //    //if (type == LogType.Error) myLog.Error(message);
        //    //if (type == LogType.Warning) myLog.Warn(message);
        //}
    }
}
