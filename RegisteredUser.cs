using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileHandler
{
    internal class RegisteredUser : IComparable<RegisteredUser>
    {
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
        public int Age { get; init; }
        public string? Email { get; init; }

        public int CompareTo(RegisteredUser other)
        {
            if (other == null) return 1;
            return string.Compare(LastName, other.LastName, StringComparison.OrdinalIgnoreCase);
        }
        public override string ToString()
        {
            return $"{FirstName}, {LastName}, {Age}, {Email}";
        }
    }
}
