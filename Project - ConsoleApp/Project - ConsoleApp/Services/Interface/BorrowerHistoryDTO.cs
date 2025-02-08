namespace Project___ConsoleApp.Services.Interface
{
    public class BorrowerHistoryDTO
    {
        public string BookTitle { get; set; }

        public DateTime BorrowDate { get; set; }

        public DateTime? ReturnDate { get; set; }
    }
}