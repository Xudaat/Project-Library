using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project___ConsoleApp.DTOs.BorrowerDTO
{
    public class UpdateBorrowerDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
