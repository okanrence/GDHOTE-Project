using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace GDHOTE.Hub.WindowsService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            var logPath = AppDomain.CurrentDomain.BaseDirectory + @"log4net.config";
            log4net.Config.XmlConfigurator.Configure(new FileInfo(logPath));

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new NotificationService()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
