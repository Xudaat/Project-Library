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
    public class BorrowerRepository : GenericRepository<Borrower>, IBorrowerRepository
    {
        AppDbContext _appDbContext;

        public BorrowerRepository()
        {
            _appDbContext = new AppDbContext();
        }

        public List<Borrower> GetAllBorrower()
        {
            return _appDbContext.Set<Borrower>().Include(x => x.Loans).ToList();
        }


        public Borrower GetBorrowerByID(int Id)
        {
            return _appDbContext.Set<Borrower>().Include(x => x.Loans).FirstOrDefault(x => x.Id == Id);
        }
    }
}
