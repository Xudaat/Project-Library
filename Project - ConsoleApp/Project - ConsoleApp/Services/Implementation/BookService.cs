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
            var book = new Book { Title = createBookDTO.Title, Authors = new List<Author>() };
            _bookRepository.Create(book);
            _bookRepository.Commit();
        }



        public void Delete(int id)
        {
            var book = _bookRepository.GetAll().FirstOrDefault(x => x.Id == id);
            if (book is null)
            {
                throw new InvalidIdException("Book not found");
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
                Title = book.Title,
                PublishedYear = book.PublishedYear,
                Description = book.Desc,
                Authors = book.Authors.Select(x => x.Name).ToList()
            }).ToList();
        }



        public void Update(int Id, UpdateBookDTO updateBookDTO)
        {
            var book = _bookRepository.GetAll().FirstOrDefault(x => x.Id == Id);
            if (book is null)
            {
                throw new InvalidIdException("Book ID not found to Update");
            }
            book.Title = updateBookDTO.Title;
            _bookRepository.Commit();
        }
    }
}
