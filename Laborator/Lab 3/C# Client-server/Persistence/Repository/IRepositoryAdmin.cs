using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repository
{
    public interface IRepositoryAdmin<ID, E>
    {
        E Find(ID username);
    }
}
