using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileHandler.Exceptions
{
    public class InvalidNameException : Exception
    {
        public InvalidNameException(string message) : base(message)
        {
        }

        public InvalidNameException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
