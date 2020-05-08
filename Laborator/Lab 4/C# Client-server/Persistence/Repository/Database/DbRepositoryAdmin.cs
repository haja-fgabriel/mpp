using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence.Utils;
using System.Collections.Specialized;
using log4net;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;

namespace Persistence.Repository.Database
{
    public class DbRepositoryAdmin : IRepositoryAdmin<string, Admin>
    {
        private DbUtils dbUtils;
        private static ILog logger = LogManager.GetLogger(typeof(DbRepositoryAdmin));


        public DbRepositoryAdmin(NameValueCollection properties)
        {
            dbUtils = new DbUtils(properties);

            logger.Info("Created new DbRepositoryAdmin instance");
        }
        
        public Admin Find(string username)
        {
            logger.Debug("Finding admin with username " + username);
            MySqlConnection connection = (MySqlConnection)dbUtils.GetConnection();

            MySqlCommand selectCommand = new MySqlCommand("select * from RegisteredUsers where Username=@username", connection);
            selectCommand.Parameters.Add("@username", MySqlDbType.VarChar).Value = username;
            MySqlDataAdapter adapter = new MySqlDataAdapter(selectCommand);
            DataSet dataSet = new DataSet();
            
            adapter.Fill(dataSet);

            if (dataSet.Tables[0].Rows.Count == 0)
            {
                logger.Debug("No admin found with username " + username);
                return null;
            }

            logger.Debug("Successfully found admin with username " + username);

            return new Admin()
            {
                ID = (string)dataSet.Tables[0].Rows[0]["Username"],
                Name = (string)dataSet.Tables[0].Rows[0]["Name"],
                Password = (string)dataSet.Tables[0].Rows[0]["Password"]
            };
        }
    }
}
