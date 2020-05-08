using log4net;
using Model;
using Persistence.Utils;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using MySql.Data.MySqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Persistence.Repository.Database
{
    public class DbRepositoryEvent : IRepositoryEvent<int, Event>
    {

        private DbUtils dbUtils;
        private static ILog logger = LogManager.GetLogger(typeof(DbRepositoryEvent));

        public DbRepositoryEvent(NameValueCollection properties)
        {
            dbUtils = new DbUtils(properties);
            logger.Info("Created new DbRepositoryEvent instance");
        }

        // TODO implement code
        public Event Find(int id)
        {
            logger.Debug("Finding event with id " + id + "...");

            MySqlConnection connection = (MySqlConnection)dbUtils.GetConnection();

            MySqlCommand selectCommand = new MySqlCommand("select * from Probes where Id=@idEv", connection);
            selectCommand.Parameters.Add("@idEv", MySqlDbType.Int32).Value = id;
            MySqlDataAdapter adapter = new MySqlDataAdapter(selectCommand);
            DataSet dataSet = new DataSet();

            adapter.Fill(dataSet);

            if (dataSet.Tables[0].Rows.Count == 0)
            {
                logger.Info("No event found with id " + id);
                return null;
            }

            logger.Debug("Event found with id " + id);
            return new Event()
            {
                ID = (int)dataSet.Tables[0].Rows[0]["Id"],
                Distance = (int)dataSet.Tables[0].Rows[0]["Distance"],
                AgeMin = (int)dataSet.Tables[0].Rows[0]["AgeMin"],
                AgeMax = (int)dataSet.Tables[0].Rows[0]["AgeMax"],
                No = 0
            };
        }

        public List<Event> FindAll()
        {
            logger.Debug("Finding all events...");

            MySqlConnection connection = (MySqlConnection)dbUtils.GetConnection();

            MySqlCommand selectCommand = new MySqlCommand("select * from Probes", connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(selectCommand);
            DataSet dataSet = new DataSet();

            adapter.Fill(dataSet);

            var events = from DataRow row in dataSet.Tables[0].Rows
                    select new Event()
                    {
                        ID = (int)row["Id"],
                        Distance = (int)row["Distance"],
                        AgeMin = (int)row["AgeMin"],
                        AgeMax = (int)row["AgeMax"]
                    };


            logger.Debug("Successfully found all events");

            return events.ToList();
        }
    }
}
