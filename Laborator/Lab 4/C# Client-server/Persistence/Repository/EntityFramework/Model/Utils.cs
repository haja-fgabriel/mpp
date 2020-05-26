using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Persistence.Repository.EntityFramework.Model
{
    public class Utils
    {
        public static person CreateDbPerson(Child c)
        {
            return new person()
            {
                Id = c.ID,
                Age = c.Age,
                Name = c.Name,
                Probe1 = c.IdEvent1,
                Probe2 = c.IdEvent2
            };
        }

        public static Child GetChild(person p)
        {
            return new Child(p.Id)
            {
                Name = p.Name,
                Age = p.Age.HasValue ? p.Age.Value : 0,
                IdEvent1 = p.Probe1.HasValue ? p.Probe1.Value : 0,
                IdEvent2 = p.Probe2.HasValue ? p.Probe2.Value : 0
            };
        }

        public static Admin GetAdmin(registereduser u)
        {
            return new Admin(u.Username, u.Name, u.Password);
        }

        public static Event GetEvent(probe p)
        {
            return new Event()
            {
                ID = p.Id,
                AgeMin = p.AgeMin.HasValue ? p.AgeMin.Value : 0,
                AgeMax = p.AgeMax.HasValue ? p.AgeMax.Value : 0,
                Distance = p.Distance,
                No = 0
            };
        }
    }
}
