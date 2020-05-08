using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Utils
{
    public class NumberUtils
    {
        private static Random random = new Random();

        public static int GenerateNumber(int left, int right)
        {
            return random.Next(left, right);
        }
    }
}
