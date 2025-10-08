namespace LibraryManagement.Areas.Admin.Models
{
    public class BorrowRequest
    {
        public int ReaderId { get; set; }
        public List<int> Books { get; set; }
    }
}
