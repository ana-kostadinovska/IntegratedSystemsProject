using BookStore.Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Models
{
    public class Order : BaseEntity
    {
        public string UserId { get; set; }
        public BookStoreUser User { get; set; }


        public string? Address { get; set; }
        public double TotalPrice { get; set; }
        public IEnumerable<BookInOrder> BooksInOrder { get; set; }
    }
}
