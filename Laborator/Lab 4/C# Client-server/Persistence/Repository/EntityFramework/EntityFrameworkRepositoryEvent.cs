using log4net;
using Model;
using Persistence.Repository.EntityFramework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repository.EntityFramework
{
    public class EntityFrameworkRepositoryEvent : IRepositoryEvent<int, Event>
    {
        private static ILog logger = LogManager.GetLogger(typeof(EntityFrameworkRepositoryEvent));
        private AthletesDbContext context;

        public EntityFrameworkRepositoryEvent()
        {
            context = new AthletesDbContext();
            logger.Info("Created new EntityFrameworkRepositoryEvent instance");
        }

        public Event Find(int id)
        {
            logger.Debug("Finding event with id " + id + "...");
            return Model.Utils.GetEvent(context.probes.Find(id));
        }

        public List<Event> FindAll()
        {
            var x = context.probes.ToList();
            return x.Select(p => Model.Utils.GetEvent(p)).ToList();
        }
    }
}
