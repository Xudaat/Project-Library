using Project___ConsoleApp.AllExceptions;
using Project___ConsoleApp.DTOs.LoanDTO;
using Project___ConsoleApp.Models;
using Project___ConsoleApp.Repository.Implementation;
using Project___ConsoleApp.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project___ConsoleApp.Services.Implementation
{
    public class LoanService : ILoanService
    {
        private readonly LoanRepository _loanRepocitory;
        private readonly BorrowerRepository _borrowerRepocitory;
        private readonly BookRepository _bookRepocitory;

        public LoanService()
        {
            _loanRepocitory = new LoanRepository();
            _borrowerRepocitory = new BorrowerRepository();
            _bookRepocitory = new BookRepository();
        }

        public void Add(CreateLoanDTO createLoanDTO)
        {
            var loan = new Loan { BorrowerId = createLoanDTO.BorrowerId };
            foreach (var bookId in createLoanDTO.BooksId)
            {
                loan.LoanItems.Add(new LoanItem { BookId = bookId });
            }
            if (createLoanDTO == null) throw new InvalidInputException("Input cannot be null");
            if (createLoanDTO.LoanDate > DateTime.UtcNow.AddHours(4))
                throw new InvalidInputException("Invalid loan date! You cannot borrow a book in the future.");
            Loan loans = new Loan
            {
                BorrowerId = createLoanDTO.BorrowerId,
                LoanDate = createLoanDTO.LoanDate,
                MustReturnDate = createLoanDTO.LoanDate.AddDays(15),
                LoanItems = createLoanDTO.BooksId.Select(bookId => new LoanItem { BookId = bookId }).ToList()
            };

            _loanRepocitory.Create(loans);
            _loanRepocitory.Commit();
        }

        public void BorrowBook(int borrowerId, int bookId)
        {
            var borrower = _borrowerRepocitory.GetById(borrowerId);
            if (borrower == null)
            {
                throw new InvalidIdException("Borrower not found!");
            }

            var book = _bookRepocitory.GetById(bookId);
            if (book == null)
            {
                throw new InvalidIdException("Book not found!");
            }


            var currentLoan = _loanRepocitory.GetAll()
                .FirstOrDefault(x => x.BorrowerId == borrowerId && x.ReturnTime == null);

            if (currentLoan != null)
            {
                throw new InvalidOperationException("This book is already borrower by this borrower and has't been returned.");
            }


            var newLoan = new Loan
            {
                BorrowerId = borrowerId,
                BorrowDate = DateTime.UtcNow.AddHours(4),
                ReturnTime = null
            };

            _loanRepocitory.Create(newLoan);
            _loanRepocitory.Commit();
        }

        public List<GetAllLOanDTO> GetAllLoan()
        {

            var loans = _loanRepocitory.GetAll();
            if (!loans.Any())
            {
                throw new InvalidInputException("There is no loan books!");
            }
            return loans.Select(loans => new GetAllLOanDTO
            {
                BorrowerId = loans.BorrowerId,
                LoanDate = loans.LoanDate,
                MustReturnDate = loans.MustReturnDate,
                BookTitles = loans.LoanItems.Select(item => item.Book.Title).ToList()
            }).ToList();
        }

        public void Delete(int Id)
        {
            var loans = _loanRepocitory.GetAll().FirstOrDefault(x => x.Id == Id);
            if (loans == null)
                throw new InvalidIdException("Id not found!");
            _loanRepocitory.Delete(loans);
            _loanRepocitory.Commit();
        }

        public void Update(int Id, UpdateLoanDTO updateLoanDTO)
        {
            var loans = _loanRepocitory.GetAll().FirstOrDefault(x => x.Id == Id);
            if (loans == null)
                throw new InvalidIdException("Id not found to update!");
            loans.ReturnTime = DateTime.UtcNow.AddHours(4);
            _loanRepocitory.Commit();
        }

        public void ReturnBook(int loanId)
        {
            var loan = _loanRepocitory.GetById(loanId);
            if (loan == null)
            {
                throw new InvalidIdException("Loan not found!");
            }

            loan.ReturnTime = DateTime.UtcNow.AddHours(4);
            _loanRepocitory.Commit();
        }

        public Book GetMostBorrowerBook()
        {
            var mostBorrowerBook = _loanRepocitory.GetAll()
                .FirstOrDefault();

            if (mostBorrowerBook == null)
            {
                throw new InvalidOperationException("No borrower books found.");
            }

            var book = _bookRepocitory.GetById(mostBorrowerBook.BorrowerId);
            return book;
        }

        public List<Borrower> GetOverdueBorrowers()
        {
            var overdueLoans = _loanRepocitory.GetAll()
                .Where(x => x.MustReturnDate < DateTime.UtcNow.AddHours(4) && x.ReturnTime == null)
                .ToList();

            var overdueBorrowers = overdueLoans
                .Select(x => _borrowerRepocitory.GetById(x.BorrowerId))
                .Where(x => x != null)
                .ToList();

            return overdueBorrowers;
        }

        public List<BorrowerHistoryDTO> GetBorrowerHistory(int borrowerId)
        {
            var borrowerLoans = _loanRepocitory.GetAll()
                .Where(x => x.BorrowerId == borrowerId)
                .ToList();

            var history = borrowerLoans
                .Select(x => new BorrowerHistoryDTO
                { 
                   BorrowDate = x.BorrowDate,
                   ReturnDate = x.ReturnTime
                })
                .ToList();

            return history;
        }
    }
}