namespace BookStoreAdminApplication.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public BookStoreUser User { get; set; }
        public string Address { get; set; }
        public double TotalPrice { get; set; }
        public IEnumerable<BookInOrder> BooksInOrder { get; set; }
    }
}
