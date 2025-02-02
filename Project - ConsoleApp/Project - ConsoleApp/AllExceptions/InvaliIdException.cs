using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project___ConsoleApp.AllExceptions
{
    public class InvalidIdException:Exception
    {
        public InvalidIdException() { }
        public InvalidIdException(string message) : base(message) { }
    }
}
