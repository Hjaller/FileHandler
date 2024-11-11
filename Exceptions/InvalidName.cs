using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileHandler.Exceptions
{
    public class InvalidNameException(string message) : Exception(message)
    {
    }
}
