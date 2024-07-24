using BookStore.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Models
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }
        public string CoverImage { get; set; }
        public int ReleaseYear { get; set; }
        public string ISBN { get; set; }
        public double Price { get; set; }
        public int Edition { get; set; }
        public List<GenreEnum> Genres { get; set; }
        public Guid AuthorId { get; set; }
        public Author? Author { get; set; }
        public Guid PublisherId { get; set; }
        public Publisher? Publisher { get; set; }
    }
}
