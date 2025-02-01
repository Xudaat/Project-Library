using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project___ConsoleApp.DTOs.BookDTO
{
    public class GetAllBookDTO
    {
        public List<string>Authors { get; set; }
        public bool isDeleted {  get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public int PublishedYear { get; set; }
    }
}
