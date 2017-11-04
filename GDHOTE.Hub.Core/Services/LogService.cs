﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using NPoco;

namespace GDHOTE.Hub.Core.Services
{
    public class LogService
    {
        public string Message { get; set; }
        protected ILog Logger;

        protected LogService(string name)
        {
            Logger = LogManager.GetLogger(name);
        }
        public   void Log(EnumsService.LogType type, Exception ex, object message)
        {
            Logger.Info("============ Logging from Service ================");
            if (type == EnumsService.LogType.Info) Logger.Info(message, ex);
            if (type == EnumsService.LogType.Fatal) Logger.Fatal(message, ex);
            if (type == EnumsService.LogType.Error) Logger.Error(message, ex);
            if (type == EnumsService.LogType.Warning) Logger.Warn(message, ex);
        }

        public static void Log(EnumsService.LogType type, string callerFormName, string methodName, Exception ex)
        {
            ILog myLog = LogManager.GetLogger(callerFormName + "|" + methodName);
            myLog.Info("============ Logging from Service ================");
            if (type == EnumsService.LogType.Info) myLog.Info("Message: " + ex.Message + "|StackTrace: " + ex.StackTrace);
            if (type == EnumsService.LogType.Fatal) myLog.Fatal("Message: " + ex.Message + "|StackTrace: " + ex.StackTrace);
            if (type == EnumsService.LogType.Error) myLog.Error("Message: " + ex.Message + "|StackTrace: " + ex.StackTrace);
            if (type == EnumsService.LogType.Warning) myLog.Warn("Message: " + ex.Message + "|StackTrace: " + ex.StackTrace);
        }
        public static void Log(EnumsService.LogType type, string callerFormName, string methodName, string message)
        {
            ILog myLog = LogManager.GetLogger(callerFormName + "|" + methodName);
            myLog.Info("============ Logging from Service ================");
            if (type == EnumsService.LogType.Info) myLog.Info(message);
            if (type == EnumsService.LogType.Fatal) myLog.Fatal(message);
            if (type == EnumsService.LogType.Error) myLog.Error(message);
            if (type == EnumsService.LogType.Warning) myLog.Warn(message);
        }
    }
}