namespace BookStoreAdminApplication.Models
{
    public class Publisher
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public ICollection<Book>? Books { get; set; }
    }
}
