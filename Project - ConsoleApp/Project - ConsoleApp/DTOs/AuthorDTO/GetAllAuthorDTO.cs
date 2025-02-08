using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project___ConsoleApp.DTOs.AuthorDTO
{
    public class GetAllAuthorDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<string> BookTitles { get; set; }
    }
}
