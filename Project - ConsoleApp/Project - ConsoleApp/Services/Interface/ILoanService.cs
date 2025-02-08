using Project___ConsoleApp.DTOs.LoanDTO;
using Project___ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project___ConsoleApp.Services.Interface
{
    public interface ILoanService
    {
        void Add(CreateLoanDTO createLoanDTO);

        void Delete(int Id);

        void Update(int Id, UpdateLoanDTO updateLoanDTO);

        List<GetAllLOanDTO> GetAllLoan();

        void BorrowBook(int borrowerId, int bookId);
        
        void ReturnBook(int loanId);

        Book GetMostBorrowerBook();

        List<Borrower> GetOverdueBorrowers();

        List<BorrowerHistoryDTO> GetBorrowerHistory(int borrowerId);
    }
}
