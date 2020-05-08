using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Odbc;
using System.Collections.Specialized;
using MySql.Data.MySqlClient;
using log4net;
using System.Configuration;

namespace Persistence.Utils
{
    public class DbUtils
    {
        private IDbConnection instance;
        private NameValueCollection properties;
        private static ILog logger = LogManager.GetLogger(typeof(DbUtils));

        public DbUtils(NameValueCollection properties)
        {
            this.properties = properties;
        }

        public IDbConnection GetConnection()
        {
            logger.Info("Getting connection...");
            if (instance == null || instance.State == ConnectionState.Closed)
            {
                logger.Info("Connection instance not found. Creating new instance...");
                instance = new MySqlConnection(properties["MySqlConnectionString"]);
            }
            logger.Info("Found connection " + instance);
            return instance;
        }
    }
}
