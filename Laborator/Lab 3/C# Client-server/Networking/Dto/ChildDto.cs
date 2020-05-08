using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Networking.Dto
{
    [Serializable]
    public class ChildDto
    {
        private int id;
        private string name;
        private int age;
        private int idEvent1;
        private int idEvent2;

        public ChildDto(int id, string name, int age, int idEvent1, int idEvent2)
        {
            this.id = id;
            this.name = name;
            this.age = age;
            this.idEvent1 = idEvent1;
            this.idEvent2 = idEvent2;
        }

        public ChildDto(int id, int age, int idEvent1, int idEvent2)
        {
            this.id = id;
            this.name = "";
            this.age = age;
            this.idEvent1 = idEvent1;
            this.idEvent2 = idEvent2;
        }

        public virtual int ID
        {
            get { return id; }
        }

        public virtual int IdEvent1
        {
            get { return idEvent1; }
        }

        public virtual int IdEvent2
        {
            get { return idEvent2; }
        }

        public virtual int Age
        {
            get { return age; }
        }

        public virtual string Name
        {
            get { return name; }
        }
    }
}
