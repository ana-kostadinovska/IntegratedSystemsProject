using BookStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service.Interface
{
    public interface IAuthorService
    {
        List<Author> GetAllAuthors();
        // Da gi lista i knigite
        Author GetDetailsForAuthor(Guid? id);
        void CreateNewAuthor(Author author);
        void EditExistingAuthor(Author author);
        void DeleteAuthor(Guid id);
    }
}
