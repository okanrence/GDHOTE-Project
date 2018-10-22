using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Repository.Hierarchy;
using NPoco;

namespace GDHOTE.Hub.BusinessCore.Services
{
    public class BaseService
    {
        public static IDatabase GdhoteConnection()
        {
            string dbConnection = "";
            dbConnection = UseLive() == "Y"
                ? "Data Source=192.99.150.165;Initial Catalog=GDHOTE;user Id=gdhote;password=HolyOrder@123#"
                : "Data Source=.;Initial Catalog=GDHOTE;user Id=sa;password=password10$";
            return new Database(dbConnection, DatabaseType.SqlServer2012, SqlClientFactory.Instance);
            //ConfigurationManager.ConnectionStrings["GdhoteConnection"].ConnectionString;
        }

        public static string UseLive()
        {
            return Get("settings.config.use.live");// "Y";
        }
        public static string Get(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
        public static string ReturnFileUploadPath()
        {
            return AppDomain.CurrentDomain.BaseDirectory + Get("settings.file.upload.folder");
        }
        public static string ReturnBaseUrl()
        {
            return Get("settings.base.url");
        }
    }
}
