using Project___ConsoleApp.DTOs.BookDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project___ConsoleApp.Services.Interface
{
    public interface IBookService
    {
        void Add(CreateBookDTO createBookDTO);
        void Update(int Id,UpdateBookDTO updateBookDTO);
        void Delete(int id);
        List<GetAllBookDTO> GetAllBooks();
    }
}
