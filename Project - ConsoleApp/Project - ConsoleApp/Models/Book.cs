using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project___ConsoleApp.Models
{
   public class Book : BaseEntity
    {
        public string Title { get; set; }
        public string Desc { get; set; }
        public int PublishedYear { get; set; }
        public List<Author> Authors { get; set; }
        public LoanItem LoanItem { get; set; }

    }
}
