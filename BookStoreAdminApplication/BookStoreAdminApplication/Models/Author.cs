namespace BookStoreAdminApplication.Models
{
    public class Author
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Biography { get; set; }
        // 1:M so Book
        public ICollection<Book>? Books { get; set; }
    }
}
