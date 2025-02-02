using Project___ConsoleApp.DTOs.AuthorDTO;
using Project___ConsoleApp.DTOs.BookDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project___ConsoleApp.Services.Interface
{
   public interface IAuthorService
    {
        void Add(CreateAuthorDTO createAuthorDTO);
        void Update(int Id, UpdateAuthorDTO updateAuthorDTO);
        void Delete(int id);
        List<GetAllAuthorDTO> GetAllAuthors();
    }
}
