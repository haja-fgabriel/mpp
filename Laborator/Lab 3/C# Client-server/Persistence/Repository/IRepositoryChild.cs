using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repository
{
    public interface IRepositoryChild<ID, E>
    {
        E Save(E entity);
        List<E> Filter(int idEv, int ageMin, int ageMax);
        int CountByEvent(int idEv);
    }
}
