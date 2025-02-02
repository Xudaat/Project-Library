using Project___ConsoleApp.AllExceptions;
using Project___ConsoleApp.DTOs.AuthorDTO;
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
        private readonly IAuthorRepository _authorRepository;

        public AuthorService()
        {
          _authorRepository = new AuthorRepository();
        }
        public void Add(CreateAuthorDTO createAuthorDTO)
        {
            var author = new Author { Name = createAuthorDTO.Name, Authors = new List<Book>() };
            _authorRepository.Create(author);
            _authorRepository.Commit();
        }

        public void Delete(int id)
        {
            var author = _authorRepository.GetAll().FirstOrDefault(x=>x.Id==id);
            if (author is null)
            {
                throw new InvalidIdException("Author not found");
            }
            _authorRepository.Delete(author);
            _authorRepository.Commit();
        }

        public List<GetAllAuthorDTO> GetAllAuthors()
        {
            var author= _authorRepository.GetAll();
            if (author is null)
            {
                throw new InvalidInputException("There is no such author");
            }
            return _authorRepository.GetAll().Select(author => new GetAllAuthorDTO
            {
                Id = author.Id,
                Name = author.Name,
                BookTitle = author.Authors.Select(x => x.Title).ToList()
            }).ToList();
        }

        public void Update(int Id, UpdateAuthorDTO updateAuthorDTO)
        {
            var author = _authorRepository.GetAll().FirstOrDefault(x => x.Id == Id);
            if (author is null)
            {
                throw new InvalidIdException("Author ID not found to Update");
            }
            author.Name = updateAuthorDTO.Name;
            _authorRepository.Commit();
        }
    }
}
