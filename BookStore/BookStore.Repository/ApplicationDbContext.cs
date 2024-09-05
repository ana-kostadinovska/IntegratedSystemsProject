using BookStore.Domain;
using BookStore.Domain.Identity;
using BookStore.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public class ApplicationDbContext : IdentityDbContext<BookStoreUser>
    {
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Publisher> Publishers { get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public virtual DbSet<BookInShoppingCart> BookInShoppingCarts { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<BookInOrder> BookInOrders { get; set; }

        public virtual DbSet<EmailMessage> EmailMessages { get; set; }
        // TODO: Implement Stripe
        /*public virtual DbSet<EmailMessage> EmailMessages { get; set; }*/
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
