using Project___ConsoleApp.DTOs.BookDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project___ConsoleApp.Services.Interface
{
    public interface ILoanItemService
    {
        void Add(CreateLoanItemDTO createLoanItemDTO);
        void Update(int Id, UpdateLoanItemDTO updateLoanItemDTO);
        void Delete(int id);
        List<GetAllLoanItemDTO> GetAllLoanItems();
    }
}
