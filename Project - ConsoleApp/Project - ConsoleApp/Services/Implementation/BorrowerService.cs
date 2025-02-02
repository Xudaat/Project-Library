using Project___ConsoleApp.AllExceptions;
using Project___ConsoleApp.DTOs.BookDTO;
using Project___ConsoleApp.DTOs.BorrowerDTO;
using Project___ConsoleApp.Models;
using Project___ConsoleApp.Repository.Implementation;
using Project___ConsoleApp.Repository.Interface;
using Project___ConsoleApp.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project___ConsoleApp.Services.Implementation
{
    public class BorrowerService : IBorrowerService
    {
        private readonly IBorrowerRepository _borrowerRepository;

        public BorrowerService()
        {
            _borrowerRepository = new BorrowerRepository();
        }
        public void Add(CreateBorrowerDTO createBorrowerDTO)
        {
            var borrower = new Borrower { Name = createBorrowerDTO.Name, Loans = new List<Loan>() };
            _borrowerRepository.Create(borrower);
            _borrowerRepository.Commit();
        }



        public void Delete(int id)
        {
            var borrower = _borrowerRepository.GetAll().FirstOrDefault(x => x.Id == id);
            if (borrower is null)
            {
                throw new InvalidIdException("Borrower not found");
            }
            _borrowerRepository.Delete(borrower);
            _borrowerRepository.Commit();
        }

        public List<GetAllBorrowerDTO> GetAllBorrowers()
        {
            var borrower = _borrowerRepository.GetAll();
            if (borrower is null)
            {
                throw new InvalidInputException("There is no such borrower");
            }
            return _borrowerRepository.GetAll().Select(borrower => new GetAllBorrowerDTO
            {
                Id = borrower.Id,
                Name = borrower.Name,
                Email = borrower.Email,
            }).ToList();
        }



        public void Update(int Id, UpdateBorrowerDTO updateBorrowerDTO)
        {
            var borrower = _borrowerRepository.GetAll().FirstOrDefault(x => x.Id == Id);
            if (borrower is null)
            {
                throw new InvalidIdException("Borrower ID not found to Update");
            }
            borrower.Name = updateBorrowerDTO.Name;
            _borrowerRepository.Commit();
        }
    }
}
