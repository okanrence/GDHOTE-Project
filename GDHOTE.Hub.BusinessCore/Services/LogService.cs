using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using GDHOTE.Hub.CoreObject.Enumerables;
using System.Reflection;
using log4net.Repository.Hierarchy;

namespace GDHOTE.Hub.BusinessCore.Services
{
    public class LogService : BaseService
    {
        //private static ILog myLogger { get; set; }
        //private static ILog myLogger { get; set; }
        private static readonly ILog myLogger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        //static LogService()
        //{
        //    myLogger = LogManager.GetLogger(typeof(Logger));
        //}

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

        public static void Info(object msg)
        {
            myLogger.Info(msg);
        }

        public static void LogError(object msg)
        {
            myLogger.Info(msg);

            //var log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            //log.Info(msg);
        }

        public static void LogError(Exception ex)
        {
            myLogger.Info(MethodBase.GetCurrentMethod().DeclaringType, ex);

            var log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            log.Info(ex);
        }
    }
}
