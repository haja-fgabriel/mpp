using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Networking.Dto
{
    [Serializable]
    public class EventDto
    {
        private int id;
        private int ageMin;
        private int ageMax;
        private int distance;
        private int no;

        public EventDto(int id, int ageMin, int ageMax, int distance, int no)
        {
            this.id = id;
            this.ageMin = ageMin;
            this.ageMax = ageMax;
            this.distance = distance;
            this.no = no;
        }

        public virtual int ID
        {
            get { return id; }
        }

        public virtual int AgeMin
        {
            get { return ageMin; }
        }

        public virtual int AgeMax
        {
            get { return ageMax; }
        }

        public virtual int Distance
        {
            get { return distance; }
        }

        public virtual int No
        {
            get { return no; }
        }
    }
}
