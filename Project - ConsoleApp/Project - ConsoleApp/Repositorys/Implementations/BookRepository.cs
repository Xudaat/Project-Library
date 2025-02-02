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
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        AppDbContext  _appDbContext;

        public BookRepository()
        {
            _appDbContext = new AppDbContext();
        }

        public List<Book> GetAllBook()
        {
            return _appDbContext.Set<Book>().Include(x=>x.Authors).ToList();
        }


        public Book GetBookByID(int Id)
        {
            return _appDbContext.Set<Book>().Include(x => x.Authors).FirstOrDefault(x => x.Id == Id);
        }
    }
}
