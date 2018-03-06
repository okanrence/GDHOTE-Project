using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDHOTE.Hub.PortalCore.Services
{
    public class ConfigService
    {
        public static string ReturnBaseUrl()
        {
            return ConfigurationManager.AppSettings["settings.api.baseurl"];
        }
    }
}
