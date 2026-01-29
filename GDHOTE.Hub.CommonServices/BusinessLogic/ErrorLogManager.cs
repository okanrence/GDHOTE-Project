using System;
using System.Reflection;
using log4net;

namespace GDHOTE.Hub.CommonServices.BusinessLogic
{
    public class ErrorLogManager
    {
        private static readonly ILog myLogger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        //private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void Error(object msg)
        {
            myLogger.Error(msg);
        }

        public static void Error(object msg, Exception ex)
        {
            myLogger.Error(msg, ex);
        }

        public static void Error(Exception ex)
        {
            myLogger.Error(ex.Message, ex);
        }

        //public static void Info(object msg)
        //{
        //    myLogger.Info(msg);
        //}

        //public static void LogError(object msg)
        //{
        //    myLogger.Info(msg);

        //    //var log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        //    //log.Info(msg);
        //}

        public static void LogError(string callerFormName, Exception ex)
        {
            myLogger.Error(callerFormName, ex);
        }

        public static void LogError(string callerFormName, string message)
        {
            myLogger.Info(callerFormName + "|" + message);
        }
        public static void LogError(string callerFormName, string computerDetails, string methodName, string errorMessage)
        {
            //ILog myLog = LogManager.GetLogger(callerFormName + "|" + methodName);
            ILog myLog = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

            myLog.Info(callerFormName + "|" + errorMessage);
        }
    }
}
