using Microsoft.EntityFrameworkCore;
using Project___ConsoleApp.Data;
using Project___ConsoleApp.Models;
using Project___ConsoleApp.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project___ConsoleApp.Repository.Implementation
{
    public class LoanRepository : GenericRepository<Loan>, ILoanRepesitory
    {

        AppDbContext _appDbContext;

        public LoanRepository()
        {
            _appDbContext = new AppDbContext();
        }

        public List<Loan> GetAllLoan()
        {
            return _appDbContext.Set<Loan>().Include(x => x.LoanItems).Include(x => x.Borrower).ToList();
        }

        public Loan GetLoanByID(int Id)
        {
            return _appDbContext.Set<Loan>().Include(x => x.LoanItems).Include(x => x.Borrower).FirstOrDefault(x => x.Id == Id);
        }
    }
}
