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
    public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
    {
        AppDbContext _appDbContext;

        public AuthorRepository()
        {
            _appDbContext = new AppDbContext();
        }

        public List<Author> GetAllAuthors()
        {
            return _appDbContext.Set<Author>().Include(x => x.Books).ToList();
        }

        public Author GetAuthorById(int id)
        {
            return _appDbContext.Set<Author>().Include(x => x.Books).FirstOrDefault(x => x.Id == id);
        }
    }
}
