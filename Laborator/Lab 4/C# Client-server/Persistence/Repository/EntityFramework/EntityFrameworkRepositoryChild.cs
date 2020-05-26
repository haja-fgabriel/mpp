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
    public class EntityFrameworkRepositoryChild : IRepositoryChild<int, Child>
    {
        private static ILog logger = LogManager.GetLogger(typeof(EntityFrameworkRepositoryChild));
        private AthletesDbContext context;

        public EntityFrameworkRepositoryChild()
        {
            context = new AthletesDbContext();
            logger.Info("Created new EntityFrameworkRepositoryChild instance");
        }

        public int CountByEvent(int idEv)
        {
            logger.Debug("Counting children at the event with id " + idEv + "...");
            return context.people.Count(p => p.Probe1 == idEv || p.Probe2 == idEv);
        }

        public List<Child> Filter(int idEv, int ageMin, int ageMax)
        {
            logger.Debug("Finding children at the event with id " + idEv + " with ageMin "
                + ageMin + " and ageMax " + ageMax + "...");

            var x = context.people
                .Where(p => (p.Probe1 == idEv || p.Probe2 == idEv) && (p.Age >= ageMin && p.Age <= ageMax))
                // Query running on server, LINQ to Entities does not support method calling from DB context
                .ToList();
            
            return x.Select(p => Model.Utils.GetChild(p)).ToList();
        }

        public Child Save(Child entity)
        {
            person p = context.people.Add(Model.Utils.CreateDbPerson(entity));
            context.SaveChanges();
            return Model.Utils.GetChild(p);
        }
    }
}
