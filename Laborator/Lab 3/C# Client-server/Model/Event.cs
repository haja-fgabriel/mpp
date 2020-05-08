using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable]
    public class Event : Entity<int>
    {
        public int AgeMin { get; set; }
        public int AgeMax { get; set; }
        public int Distance { get; set; }
        public int No { get; set; }

        public Event()
        {
        }

        public Event(int id, int ageMin, int ageMax, int distance, int no)
        {
            ID = id;
            AgeMin = ageMin;
            AgeMax = ageMax;
            Distance = distance;
            No = no;
        }

        public override string ToString()
        {
            return ID + ": min: " + AgeMin + ", max: " + AgeMax;
        }
    }
}
