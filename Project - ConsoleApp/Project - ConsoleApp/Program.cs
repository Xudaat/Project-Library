using Project___ConsoleApp.AllExceptions;
using Project___ConsoleApp.DTOs.AuthorDTO;
using Project___ConsoleApp.DTOs.BookDTO;
using Project___ConsoleApp.DTOs.BorrowerDTO;
using Project___ConsoleApp.Services.Implementation;
using Project___ConsoleApp.Services.Interface;

namespace Project___ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IBookService bookService = new BookService();
            IAuthorService authorService = new AuthorService();
            IBorrowerService borrowerService = new BorrowerService();
            ILoanService loanService = new LoanService();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Library System");
                Console.WriteLine("1 - Author Actions");
                Console.WriteLine("2 - Book Actions");
                Console.WriteLine("3 - Borrower Actions");
                Console.WriteLine("4 - Borrow Book");
                Console.WriteLine("5 - Return Book");
                Console.WriteLine("6 - Most BorroweR Book");
                Console.WriteLine("7 - Overdue Borrowers");
                Console.WriteLine("8 - Borrower History");
                Console.WriteLine("9 - Filter Books by Title");
                Console.WriteLine("10 - Filter Books by Author");
                Console.WriteLine("0 - Exit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": AuthorActions(authorService); break;
                    case "2": BookActions(bookService); break;
                    case "3": BorrowerActions(borrowerService); break;
                    case "4": BorrowBook(loanService, bookService, borrowerService); break;
                    case "5": ReturnBook(loanService); break;
                    case "6": GetMostBorrowedBook(loanService); break;
                    case "7": GetOverdueBorrowers(loanService); break;
                    case "8": GetBorrowerHistory(loanService); break;
                    case "9": FilterBooksByTitle(bookService); break;
                    case "10": FilterBooksByAuthor(bookService); break;
                    case "0": return;
                    default: Console.WriteLine("Invalid option, try again."); break;
                }
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
            }
        }

        static void AuthorActions(IAuthorService authorService)
        {
            Console.Clear();
            Console.WriteLine("1 - List Authors");
            Console.WriteLine("2 - Create Author");
            Console.WriteLine("3 - Edit Author");
            Console.WriteLine("4 - Delete Author");
            Console.WriteLine("0 - Exit");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    var authors = authorService.GetAllAuthors();
                    if (authors.Count == 0)
                    {
                        Console.WriteLine("No authors found.");
                    }
                    else
                    {
                        authors.ForEach(x => Console.WriteLine($"{x.Id} - {x.Name}"));
                    }
                    break;

                case "2":
                    Console.Write("Enter author name: ");
                    string authorName = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(authorName) || authorName.Any(char.IsDigit))
                    {
                        Console.WriteLine("Invalid author name.");
                    }
                    else
                    {
                        authorService.Add(new CreateAuthorDTO { Name = authorName });
                        Console.WriteLine("Author added!");
                    }
                    break;

                case "3":
                    Console.Write("Enter author ID to update: ");
                    if (int.TryParse(Console.ReadLine(), out int updateAuthorId))
                    {
                        Console.Write("Enter new name: ");
                        string newName = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(newName) && !newName.Any(char.IsDigit))
                        {
                            authorService.Update(updateAuthorId, new UpdateAuthorDTO { Name = newName });
                            Console.WriteLine("Author updated!");
                        }
                        else
                        {
                            Console.WriteLine("Invalid author name.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid author ID.");
                    }
                    break;

                case "4":
                    Console.Write("Enter author ID: ");
                    if (int.TryParse(Console.ReadLine(), out int deleteAuthorId))
                    {
                        authorService.Delete(deleteAuthorId);
                        Console.WriteLine("Author deleted!");
                    }
                    else
                    {
                        Console.WriteLine("Invalid author ID.");
                    }
                    break;

                case "0": return;

                default: Console.WriteLine("Invalid option"); break;
            }
        }
        static void BookActions(IBookService bookService)
        {
            Console.Clear();
            Console.WriteLine("1 - List Books");
            Console.WriteLine("2 - Create Book");
            Console.WriteLine("3 - Edit Book");
            Console.WriteLine("4 - Delete Book");
            Console.WriteLine("0 - Exit");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    var books = bookService.GetAllBooks();
                    if (books.Count == 0)
                    {
                        throw new InvalidInputException("There is no book!");
                    }
                    else
                    {
                        books.ForEach(book =>
                        {
                            string authorNames = book.Authors != null ? string.Join(", ", book.Authors) : "No Author";
                            Console.WriteLine($"{book.Title} - {book.Description} - {book.PublishedYear} - Authors: {authorNames}");
                        });
                    }
                    break;

                case "2":
                    Console.WriteLine("Enter book name: ");
                    string title = Console.ReadLine();
                    Console.WriteLine("Enter book description: ");
                    string desc = Console.ReadLine();
                    Console.WriteLine("Enter publish year: ");
                    int publishYear;
                    while (!int.TryParse(Console.ReadLine(), out publishYear))
                    {
                        Console.WriteLine("Invalid input!Enter a valid year:");
                    }

                    Console.WriteLine("Enter Author IDs: ");
                    string authorsInput = Console.ReadLine();

                    List<int> authorIds = authorsInput.Split(',')
                                                      .Select(id => int.TryParse(id.Trim(), out int parsedId) ? parsedId : -1)
                                                      .Where(id => id > 0)
                                                      .ToList();

                    if (!authorIds.Any())
                    {
                        Console.WriteLine("Invalid authors!.");
                        return;
                    }

                    bookService.Add(new CreateBookDTO
                    {
                        Title = title,
                        Description = desc,
                        PublishedYear = publishYear,
                        AuthorsId = authorIds
                    });
                    Console.WriteLine("Book added!");
                    break;

                case "3":
                    Console.Write("Enter book ID : ");
                    int bookId;
                    while (!int.TryParse(Console.ReadLine(), out bookId))
                    {
                        Console.WriteLine("Invalid input!Enter a valid Book ID:");
                    }

                    Console.Write("Enter new title: ");
                    string newTitle = Console.ReadLine();
                    Console.WriteLine("Enter new description: ");
                    string newDesc = Console.ReadLine();
                    Console.WriteLine("Enter new publish year: ");
                    int newPublishYear;
                    while (!int.TryParse(Console.ReadLine(), out newPublishYear))
                    {
                        Console.WriteLine("Invalid input! Enter a valid year:");
                    }
                    try
                    {
                        bookService.Update(bookId, new UpdateBookDTO
                        {
                            Title = newTitle,
                            Description = newDesc,
                            PublishedYear = newPublishYear
                        });

                        Console.WriteLine("Book name, Description and Published Year updated!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred: {ex.Message}");
                    }
                    break;

                case "4":
                    Console.WriteLine("Enter Book ID : ");
                    int deleteBookId;
                    while (!int.TryParse(Console.ReadLine(), out deleteBookId))
                    {
                        Console.WriteLine("Invalid input! Please enter a valid Book ID:");
                    }
                    try
                    {
                        bookService.Delete(deleteBookId);
                        Console.WriteLine("Book deleted succesfully!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred: {ex.Message}");
                    }
                    break; ;

                case "0":
                    return;

                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
        }
        static void BorrowBook(ILoanService loanService, IBookService bookService, IBorrowerService borrowerService)
        {
            Console.Clear();
            Console.WriteLine("Borrow a Book");

            Console.Write("Enter Borrower ID: ");
            if (!int.TryParse(Console.ReadLine(), out int borrowerId))
            {
                Console.WriteLine("Invalid borrower ID.");
                return;
            }

            Console.Write("Enter Book ID: ");
            if (!int.TryParse(Console.ReadLine(), out int bookId))
            {
                Console.WriteLine("Invalid book ID.");
                return;
            }

            try
            {
                loanService.BorrowBook(borrowerId, bookId);
                Console.WriteLine("Book borrowed successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void BorrowerActions(IBorrowerService borrowerService)
        {
            Console.Clear();
            Console.WriteLine("1 - List Borrowers");
            Console.WriteLine("2 - Create Borrower");
            Console.WriteLine("3 - Edit Borrower");
            Console.WriteLine("4 - Delete Borrower");
            Console.WriteLine("0 - Exit");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    var borrowers = borrowerService.GetAllBorrowers();
                    if (borrowers.Count == 0)
                    {
                        Console.WriteLine("No borrowers found.");
                    }
                    else
                    {
                        borrowers.ForEach(b => Console.WriteLine($"{b.Id} - {b.Name} - {b.Email}"));
                    }
                    break;

                case "2":
                    Console.Write("Enter borrower name: ");
                    string borrowerName = Console.ReadLine();
                    Console.Write("Enter borrower email: ");
                    string borrowerEmail = Console.ReadLine();

                    if (!string.IsNullOrWhiteSpace(borrowerName))
                    {
                        borrowerService.Add(new CreateBorrowerDTO { Name = borrowerName, Email = borrowerEmail });
                        Console.WriteLine("Borrower created!");
                    }
                    else
                    {
                        Console.WriteLine("Invalid name or email.");
                    }
                    break;

                case "0": return;

                default: Console.WriteLine("Invalid option"); break;
            }
        }

        static void ReturnBook(ILoanService loanService)
        {
            Console.Clear();
            Console.Write("Enter Loan ID: ");
            int loanId = int.Parse(Console.ReadLine());
            loanService.ReturnBook(loanId);
            Console.WriteLine("Book returned.");
        }

        static void GetMostBorrowedBook(ILoanService loanService)
        {
            var book = loanService.GetMostBorrowerBook();
            Console.WriteLine($"Most borrower book: {book.Title} - Borrower {book.LoanItem} times");
        }

        static void GetOverdueBorrowers(ILoanService loanService)
        {
            var overdueBorrowers = loanService.GetOverdueBorrowers();
            Console.WriteLine("Overdue Borrowers:");
            overdueBorrowers.ForEach(b => Console.WriteLine($"{b.Id} - {b.Name}"));
        }

        static void GetBorrowerHistory(ILoanService loanService)
        {
            Console.Clear();
            Console.Write("Enter Borrower ID: ");
            int borrowerId = int.Parse(Console.ReadLine());
            var history = loanService.GetBorrowerHistory(borrowerId);
            Console.WriteLine("Borrower History:");
            history.ForEach(h => Console.WriteLine($"Book: {h.BookTitle}, Borrower On: {h.BorrowDate}, Returned On: {h.ReturnDate}"));
        }

        static void FilterBooksByTitle(IBookService bookService)
        {
            Console.Clear();
            Console.Write("Search by title: ");
            var keyWord = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(keyWord) || keyWord.Any(char.IsDigit))
            {
                Console.WriteLine("Search keyword cannot be empty. Try again.");
                return;
            }

            var datas = bookService.GetAllBooks()
                                   .Where(b => b.Title.Trim().ToLower()
                                   .Contains(keyWord.Trim().ToLower()))
                                   .ToList();

            if (datas.Count == 0)
            {
                Console.WriteLine("Book not found.");
                return;
            }

            Console.Clear();
            foreach (var data in datas)
            {
                Console.WriteLine($"{data.Title} - {data.PublishedYear} - {string.Join(", ", data.Authors)}");
            }
        }

        static void FilterBooksByAuthor(IBookService bookService)
        {
            Console.Clear();
            Console.Write("Search by author name: ");
            var keyWord = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(keyWord) || keyWord.Any(char.IsDigit))
            {
                Console.WriteLine("Author name cannot be empty.Try again.");
                keyWord = Console.ReadLine();
            }

            var books = bookService.GetAllBooks()
                                   .Where(b => b.Authors.Any(a => a.Trim().ToLower()
                                   .Contains(keyWord.Trim().ToLower())))
                                   .ToList();

            if (books.Count == 0)
            {
                Console.WriteLine("No books found for this author.");
                return;
            }

            Console.Clear();
            foreach (var book in books)
            {
                Console.WriteLine($"{book.Title} - {book.PublishedYear} - {string.Join(", ", book.Authors)}");
            }
        }
    }
}