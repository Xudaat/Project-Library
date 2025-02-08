using Project___ConsoleApp.AllExceptions;
using Project___ConsoleApp.DTOs.BookDTO;
using Project___ConsoleApp.Models;
using Project___ConsoleApp.Repository.Implementation;
using Project___ConsoleApp.Repository.Interface;
using Project___ConsoleApp.Services.Interface;

namespace Project___ConsoleApp.Services.Implementation
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;

        public BookService()
        {
            _bookRepository = new BookRepository();
            _authorRepository = new AuthorRepository();
        }
        public void Add(CreateBookDTO createBookDTO)
        {
            if (createBookDTO.AuthorsId == null || !createBookDTO.AuthorsId.Any())
            {
                throw new ArgumentException("Null");
            }
            if (string.IsNullOrWhiteSpace(createBookDTO.Title))
                throw new InvalidInputException("Cannot be empty.");

            var authors = _authorRepository.GetAll()
                .Where(a => createBookDTO.AuthorsId.Contains(a.Id))
                .ToList();
            if (!authors.Any())
            {
                throw new InvalidInputException("Author not found!");
            }
            var book = new Book
            {
                Title = createBookDTO.Title,
                Desc = createBookDTO.Description,
                PublishYear = createBookDTO.PublishedYear,
                Created = DateTime.UtcNow.AddHours(4),
                Authors = new List<Author>()
            };
            _bookRepository.Create(book);
            _bookRepository.Commit();
        }

        public void Delete(int id)
        {
            var book = _bookRepository.GetAll().FirstOrDefault(x => x.Id == id);
            if (book is null)
            {
                throw new InvalidIdException("Id not found!");
            }
            _bookRepository.Delete(book);
            _bookRepository.Commit();
        }

        public List<GetAllBookDTO> GetAllBooks()
        {
            var book = _bookRepository.GetAll();
            if (book is null)
            {
                throw new InvalidInputException("There is no such ");
            }
            return _bookRepository.GetAll().Select(book => new GetAllBookDTO
            {
                IsBorow = book.LoanItem != null,
                Title = book.Title,
                PublishedYear = book.PublishYear,
                Description = book.Desc,
                Authors = book.Authors != null ? book.Authors.Select(a => a.Name).ToList() : new List<string>(),
            }).ToList();
        }

        public void Update(int Id, UpdateBookDTO updateBookDTO)
        {
            var book = _bookRepository.GetAll().FirstOrDefault(x => x.Id == Id);
            if (book is null || string.IsNullOrWhiteSpace(book.Title))
            {
                throw new InvalidIdException("Id not found!");
            }

            var authors = _authorRepository.GetAll().Where(a => updateBookDTO.AuthorsId.Contains(a.Id)).ToList();

            book.Title = updateBookDTO.Title;

            book.Desc = updateBookDTO.Description;

            book.Authors = authors;

            book.PublishYear = updateBookDTO.PublishedYear;

            book.Update = DateTime.UtcNow.AddHours(4);

            _bookRepository.Commit();
        }
    }
}
