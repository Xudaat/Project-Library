using Project___ConsoleApp.AllExceptions;
using Project___ConsoleApp.DTOs.AuthorDTO;
using Project___ConsoleApp.DTOs.BookDTO;
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
    public class AuthorService : IAuthorService
    {
      
        private readonly IAuthorRepository _authorRepocitory;

        public AuthorService()
        {
            _authorRepocitory = new AuthorRepository();
        }

        public void Add(CreateAuthorDTO createAuthorDTO)
        {
            if (string.IsNullOrWhiteSpace(createAuthorDTO.Name)) throw new InvalidInputException("Author name cannot be empty.");
            var author = new Author
            {
                Name = createAuthorDTO.Name,
                Created = DateTime.UtcNow.AddHours(4),
                Books = new List<Book>()
            };
            _authorRepocitory.Create(author);
            _authorRepocitory.Commit();
        }


        public List<GetAllAuthorDTO> GetAllAuthors()
        {
            var author = _authorRepocitory.GetAll();
            if (!author.Any())
            {
                throw new InvalidInputException("There is no Author!");
            }
            return _authorRepocitory.GetAll().Select(author => new GetAllAuthorDTO
            {
                Id = author.Id,
                Name = author.Name,
                BookTitles = author.Books != null ? author.Books.Select(b => b.Title).ToList() : new List<string>()

            }).ToList();

        }

        public void Delete(int Id)
        {
            var author = _authorRepocitory.GetAll().FirstOrDefault(x => x.Id == Id);
            if (author is null)
            {
                throw new InvalidIdException("Author not found!");
            }
            _authorRepocitory.Delete(author);
            _authorRepocitory.Commit();
        }

        public void Update(int Id, UpdateAuthorDTO updateAuthorDTO)
        {
            var author = _authorRepocitory.GetAll().FirstOrDefault(x => x.Id == Id);
            if (author is null || string.IsNullOrWhiteSpace(author.Name))
            {
                throw new InvalidIdException("Author not found!");
            }
            author.Name = updateAuthorDTO.Name;
            author.Update = DateTime.UtcNow.AddHours(4);
            _authorRepocitory.Commit();
        }
    }
}

