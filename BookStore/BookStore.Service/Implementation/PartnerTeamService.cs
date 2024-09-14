using BookStore.Domain.Models;
using BookStore.Service.Interface;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service.Implementation
{
    public class PartnerTeamService : IPartnerTeamService
    {
        private readonly string _connectionString;

        public PartnerTeamService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("PartnerTeamConnection") ?? throw new System.ArgumentNullException(nameof(configuration));        
        }

        public IEnumerable<Book> GetPartnerBooks()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var books = connection.Query("SELECT * FROM Books");

                List<Book> localBooks = new List<Book>();

                foreach (var book in books)
                {
                    var kniga = new Book
                    {
                        Id = book.Id,
                        Title = book.Title,
                        CoverImage = book.ImageUrl,
                        ISBN = book.ISBN
                    };
                    localBooks.Add(kniga);
                }

                return localBooks;
            }
        }
    }
}
