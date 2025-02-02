using Project___ConsoleApp.DTOs.BookDTO;
using Project___ConsoleApp.DTOs.BorrowerDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project___ConsoleApp.Services.Interface
{
   public interface IBorrowerService
    {
        void Add(CreateBorrowerDTO createBorrowerDTO);
        void Update(int Id, UpdateBorrowerDTO updateBorrowerDTO);
        void Delete(int id);
        List<GetAllBorrowerDTO> GetAllBorrowers();
    }
}
