using Microsoft.EntityFrameworkCore;
using Project___ConsoleApp.Data;
using Project___ConsoleApp.Models;
using Project___ConsoleApp.Repository.Interface;

namespace Project___ConsoleApp.Repository.Implementation
{
    public class LoanItemRepository : GenericRepository<LoanItem>, ILoanItemrepository
    {

        AppDbContext _appDbContext;

        public LoanItemRepository()
        {
            _appDbContext = new AppDbContext();
        }

        public List<LoanItem> GetAllLoanItem()
        {
            return _appDbContext.Set<LoanItem>().Include(x => x.Loan).Include(x => x.Book).ToList();
        }

        public LoanItem GetLoanItemByID(int Id)
        {
            return _appDbContext.Set<LoanItem>().Include(x => x.Loan).Include(x => x.Book).FirstOrDefault(x => x.Id == Id);
        }
    }
}
