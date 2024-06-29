using BookStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BookStore.Domain.Identity
{
    // IdentityUser
    public class BookStoreUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public virtual ShoppingCart ShoppingCart { get; set; }
        public virtual ICollection<Order>? Order { get; set; }
    }
}
