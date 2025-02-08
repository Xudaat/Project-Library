using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project___ConsoleApp.DTOs.LoanDTO
{
     public class CreateLoanDTO
    {
        public int BorrowerId { get; set; }

        public List<int> BooksId { get; set; } 

        public DateTime LoanDate { get; set; }

        public DateTime MustReturnDate { get; set; }
    }

}
