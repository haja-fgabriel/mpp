using log4net;
using Model;
using MySql.Data.MySqlClient;
using Persistence.Utils;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repository.Database
{
    public class DbRepositoryChild : IRepositoryChild<int, Child>
    {
        private DbUtils dbUtils;
        private static ILog logger = LogManager.GetLogger(typeof(DbRepositoryChild));

        public DbRepositoryChild(NameValueCollection properties)
        {
            dbUtils = new DbUtils(properties);
            logger.Info("Created new DbRepositoryChild instance");
        }

        public int CountByEvent(int idEv)
        {
            logger.Debug("Counting children at the event with id " + idEv + "...");

            MySqlConnection connection = (MySqlConnection)dbUtils.GetConnection();

            MySqlCommand selectCommand = new MySqlCommand("select count(*) from People where " +
                "(Probe1=@idEv or Probe2=@idEv)", connection);
            selectCommand.Parameters.Add("@idEv", MySqlDbType.Int32).Value = idEv;

            MySqlDataAdapter adapter = new MySqlDataAdapter(selectCommand);
            DataSet dataSet = new DataSet();

            adapter.Fill(dataSet);

            logger.Debug("Successfully counted children at the event with id " + idEv + "...");
            return Convert.ToInt32((long)dataSet.Tables[0].Rows[0][0]);
        }

        public List<Child> Filter(int idEv, int ageMin, int ageMax)
        {
            logger.Debug("Finding children at the event with id " + idEv + " with ageMin " 
                + ageMin + " and ageMax " + ageMax + "...");
            MySqlConnection connection = (MySqlConnection)dbUtils.GetConnection();

            MySqlCommand selectCommand = new MySqlCommand("select * from People where " +
                "(Probe1=@idEv or Probe2=@idEv) and Age>=@ageMin and Age <= @ageMax", connection);
            selectCommand.Parameters.Add("@idEv", MySqlDbType.Int32).Value = idEv;
            selectCommand.Parameters.Add("@ageMin", MySqlDbType.Int32).Value = ageMin;
            selectCommand.Parameters.Add("@ageMax", MySqlDbType.Int32).Value = ageMax;

            MySqlDataAdapter adapter = new MySqlDataAdapter(selectCommand);
            DataSet dataSet = new DataSet();

            adapter.Fill(dataSet);

            var children = from DataRow rows in dataSet.Tables[0].Rows
                    select new Child()
                    {
                        ID = (int)rows["Id"],
                        Name = (string)rows["Name"],
                        IdEvent1 = (int)rows["Probe1"],
                        IdEvent2 = (int)rows["Probe2"],
                        Age = (int)rows["Age"]
                    };

            logger.Debug("Successfully found children at the event with id " + idEv + " with ageMin "
                + ageMin + "and ageMax " + ageMax + "...");
            return children.ToList();
        }

        public Child Save(Child entity)
        {
            logger.Debug("Saving child with id " + entity.ID);
            int modified = 0;
            MySqlConnection connection = (MySqlConnection)dbUtils.GetConnection();

            MySqlCommand selectCommand = new MySqlCommand("insert into People(Id, Name, Age, Probe1, Probe2) values " +
                "(@id, @name, @age, @probe1, @probe2)", connection);
            selectCommand.Parameters.Add("@id", MySqlDbType.Int32).Value = entity.ID;
            selectCommand.Parameters.Add("@name", MySqlDbType.VarChar).Value = entity.Name;
            selectCommand.Parameters.Add("@age", MySqlDbType.Int32).Value = entity.Age;
            selectCommand.Parameters.Add("@probe1", MySqlDbType.Int32).Value = entity.IdEvent1;
            selectCommand.Parameters.Add("@probe2", MySqlDbType.Int32).Value = entity.IdEvent2;

            try
            {
                logger.Debug("Opening connection");
                connection.Open();
                modified = selectCommand.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                logger.Info(e.Message);
            }
            finally
            {
                logger.Debug("Closing connection");
                connection.Close();
            }

            return modified > 0 ? entity : null;
        }
    }
}
