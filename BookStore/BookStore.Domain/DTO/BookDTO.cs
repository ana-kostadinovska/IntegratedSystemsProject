using BookStore.Domain.Enums;
using BookStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.DTO
{
    public class BookDTO
    {
        public string Title { get; set; }
        public string CoverImage { get; set; }
        public int ReleaseYear { get; set; }
        public string ISBN { get; set; }
        public double Price { get; set; }
        public int Edition { get; set; }
        public List<GenreEnum> Genres { get; set; }
        public Guid AuthorId { get; set; }
        public Guid PublisherId { get; set; }
    }
}
