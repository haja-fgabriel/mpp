using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Networking.Utils
{
    public class Parser
    {
        public static string ToString(int idEv, int ageMin, int ageMax)
        {
            return idEv + " " + ageMin + " " + ageMax;
        }

        public static IList<int> ToList(string str)
        {
            string[] fields = str.Split(' ');
            IList<int> result = new List<int>();

            result.Add(Int32.Parse(fields[0]));
            result.Add(Int32.Parse(fields[1]));
            result.Add(Int32.Parse(fields[2]));

            return result;
        }
    }
}
