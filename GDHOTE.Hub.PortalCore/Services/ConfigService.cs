using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace GDHOTE.Hub.PortalCore.Services
{
    public class ConfigService
    {
        public static string ReturnBaseUrl()
        {
            return ConfigurationManager.AppSettings["settings.api.baseurl"] + "/api/v1";
        }
    }
}
