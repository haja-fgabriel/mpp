using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable]
    public class Admin : Entity<string>
    {
        public String Name { get; set; }
        public String Password { get; set; }

        public Admin()
        {
        }

        public Admin(string id, string password)
            : this(id, "", password) { }

        public Admin(string id, string name, string password)
        {
            ID = id;
            Name = name;
            Password = password;
        }
    }
}
