using Model;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence.Repository.EntityFramework.Model;
using log4net;

namespace Persistence.Repository.EntityFramework
{
    public class EntityFrameworkRepositoryAdmin : IRepositoryAdmin<string, Admin>
    {
        private static ILog logger = LogManager.GetLogger(typeof(EntityFrameworkRepositoryAdmin));
        private AthletesDbContext context;

        public EntityFrameworkRepositoryAdmin()
        {
            context = new AthletesDbContext();
            logger.Info("Created new EntityFrameworkRepositoryAdmin instance");
        }

        public Admin Find(string username)
        {
            logger.Debug("Finding admin with username " + username);
            registereduser user = context.registeredusers.Find(username);
            if (user != null)
            {
                logger.Debug("Successfully found admin with username " + username);
                return Model.Utils.GetAdmin(user);
            }

            logger.Debug("No admin found with username " + username);
            return null;
        }
    }
}
