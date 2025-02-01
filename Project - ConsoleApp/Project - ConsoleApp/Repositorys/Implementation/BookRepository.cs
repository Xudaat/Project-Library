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
    }
}
