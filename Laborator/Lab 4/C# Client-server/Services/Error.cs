using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class Error : Exception
    {
        public Error()
        {

        }

        public Error(string message)
            : base(message) {}
    }
}
