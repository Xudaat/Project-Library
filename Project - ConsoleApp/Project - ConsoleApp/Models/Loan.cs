using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project___ConsoleApp.Models
{
    public class Loan : BaseEntity
    {
        public int BorrowerId { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime MustReturnDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public List<LoanItem> LoanItems { get; set; }
        public Borrower Borrower { get; set; }
        public DateTime? ReturnTime { get; set; }
        public DateTime BorrowDate { get; set; }
    }
}
