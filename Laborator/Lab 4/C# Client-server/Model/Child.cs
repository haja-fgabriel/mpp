using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable]
    public class Child : Entity<int>
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public int IdEvent1 { get; set; }
        public int IdEvent2 { get; set; }

        public Child()
        {
        }

        public Child(int id)
        {
        }

        public Child(int id, string name, int age, int idEvent1, int idEvent2)
        {
            ID = id;
            Name = name;
            Age = age;
            IdEvent1 = idEvent1;
            IdEvent2 = idEvent2;
        }
    }
}
