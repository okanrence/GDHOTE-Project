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
            return new Database(ConfigurationManager.ConnectionStrings["GdhoteConnection"].ConnectionString, DatabaseType.SqlServer2012, SqlClientFactory.Instance);
        }
    }
}
