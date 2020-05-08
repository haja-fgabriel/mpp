using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repository
{
    public interface IRepositoryEvent<ID, E>
    {
        List<E> FindAll();
        E Find(ID id);
    }
}
