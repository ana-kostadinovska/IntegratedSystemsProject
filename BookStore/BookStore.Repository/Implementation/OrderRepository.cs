using BookStore.Domain.Models;
using BookStore.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Repository.Implementation
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<Order> entities;

        public OrderRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<Order>();
        }
        public List<Order> GetAllOrders()
        {
            return entities
                .Include(z => z.BooksInOrder)
                .Include(z => z.User)
                .Include("BooksInOrder.Book")
                .Include("BooksInOrder.Book.Author")
                .Include("BooksInOrder.Book.Publisher")
                .ToList();
        }

        public Order GetDetailsForOrder(BaseEntity id)
        {
            return entities
                .Include(z => z.BooksInOrder)
                .Include(z => z.User)
                .Include("BooksInOrder.Book")
                .Include("BooksInOrder.Book.Author")
                .Include("BooksInOrder.Book.Publisher")
                .SingleOrDefaultAsync(z => z.Id == id.Id).Result;
        }
    }
}
