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
        private readonly IBorrowerRepository _borrowerRepocitory;

        public BorrowerService()
        {
            _borrowerRepocitory = new BorrowerRepository();
        }


        public void Add(CreateBorrowerDTO createBorrowerDTO)
        {
            if (string.IsNullOrWhiteSpace(createBorrowerDTO.Name))
                throw new InvalidInputException("Borrower name cannot be empty.");

            if (string.IsNullOrWhiteSpace(createBorrowerDTO.Email))
                throw new InvalidInputException("Email cannot be empty.");

            var borrower = new Borrower
            {
                Name = createBorrowerDTO.Name,
                Email = createBorrowerDTO.Email
            };

            _borrowerRepocitory.Create(borrower);
            _borrowerRepocitory.Commit();
        }

        public List<GetAllBorrowerDTO> GetAllBorrowers()
        {
            var borrower = _borrowerRepocitory.GetAll();
            if (!borrower.Any())
            {
                throw new InvalidInputException("There is no borrower!");
            }
            return _borrowerRepocitory.GetAll().Select(borrower => new GetAllBorrowerDTO
            {
                Id = borrower.Id,
                Email = borrower.Email,
                Name = borrower.Name

            }).ToList();
        }

        public void Delete(int Id)
        {
            var borrower = _borrowerRepocitory.GetAll().FirstOrDefault(x => x.Id == Id);
            if (borrower is null)
            {
                throw new InvalidIdException("Borrower ID not found!");
            }
            _borrowerRepocitory.Delete(borrower);
            _borrowerRepocitory.Commit();
        }

        public void Update(int Id, UpdateBorrowerDTO updateBorrowerDTO)
        {
            var borrower = _borrowerRepocitory.GetAll().FirstOrDefault(b => b.Id == Id);
            if (borrower is null || string.IsNullOrWhiteSpace(borrower.Name))
            {
                throw new InvalidIdException("Borrower ID not found!");
            }
            borrower.Name = updateBorrowerDTO.Name;
            borrower.Email = updateBorrowerDTO.Email;
            borrower.Update = DateTime.UtcNow.AddHours(4);
            _borrowerRepocitory.Commit();
        }



    }
}
