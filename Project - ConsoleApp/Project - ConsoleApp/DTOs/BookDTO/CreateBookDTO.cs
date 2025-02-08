using Project___ConsoleApp.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project___ConsoleApp.DTOs.BookDTO
{
    public class CreateBookDTO
    {
        public string Description { get; set; }
        public string Title { get; set; }
        public int PublishedYear {  get; set; }
        public List<int> AuthorsId { get; set; }
    }
        
    
}
