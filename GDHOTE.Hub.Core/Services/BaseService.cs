using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPoco;

namespace GDHOTE.Hub.Core.Services
{
    public class BaseService
    {
        public static IDatabase GdhoteConnection()
        {
            string dbConnection = "Data Source=192.99.150.165;Initial Catalog=GDHOTE;user Id=gdhote;password=HolyOrder@123#";
            return new Database(dbConnection, DatabaseType.SqlServer2012, SqlClientFactory.Instance);
            //return new Database(ConfigurationManager.ConnectionStrings["GdhoteConnection"].ConnectionString, DatabaseType.SqlServer2012, SqlClientFactory.Instance);
        }
       
    }
}
