using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project___ConsoleApp.AllExceptions
{
    public class InvalidInputException:Exception
    {
        public InvalidInputException() { }
        public InvalidInputException(string message) : base(message) { }
    }
}
