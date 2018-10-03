using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GDHOTE.Hub.CommonServices.BusinessLogic;

namespace GDHOTE.Hub.WindowsService
{
    public partial class NotificationService : ServiceBase
    {
        private Timer _oTimer;
        private string _busyFlag = "";
        private long _runInterval = Int64.Parse(ConfigurationManager.AppSettings["settings.service.run.interval"]);
        private string _serviceName = "";// BaseService.Get("settings.service.service.name");
        private string _deployedServerIp = "";// BaseService.Get("settings.service.deployed.server");
        public NotificationService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            if (_busyFlag == "N" | string.IsNullOrEmpty(_busyFlag))
            {
                TimerCallback oCallback = new TimerCallback(OnTimedEvent);
                _oTimer = new Timer(oCallback, null, 0, _runInterval);
            }
        }

        protected override void OnStop()
        {
        }
        void StartProccess()
        {
            _busyFlag = "Y";

            ErrorLogManager.LogError("Service Process", "Started");

            WeddingAnniversaryManager.StartEmailProcess();

            ErrorLogManager.LogError("Service Process", "Ended");


            _busyFlag = "N";
        }
        private void OnTimedEvent(object state)
        {
            if (_busyFlag == "N" || string.IsNullOrEmpty(_busyFlag))
            {
                StartProccess();
            }
        }
    }
}
