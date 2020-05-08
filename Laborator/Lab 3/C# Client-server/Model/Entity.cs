using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable]
    public class Entity<Id>
    {
        public Id ID { get; set; }

        public override string ToString()
        {
            return "Entity: {id = " + ID + "}";
        }
    }
}
