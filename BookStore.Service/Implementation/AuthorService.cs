using BookStore.Domain.Models;
using BookStore.Repository.Interface;
using BookStore.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service.Implementation
{
    public class AuthorService : IAuthorService
    {
        private readonly IRepository<Author> _authorRepository;

        public AuthorService(IRepository<Author> authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public void CreateNewAuthor(Author author)
        {
            _authorRepository.Insert(author);
        }

        public void DeleteAuthor(Guid id)
        {
            Author authorToDelete = _authorRepository.Get(id);
            _authorRepository.Delete(authorToDelete);
        }

        public void EditExistingAuthor(Author author)
        {
            _authorRepository.Update(author);
        }

        public List<Author> GetAllAuthors()
        {
            return _authorRepository.GetAll().ToList();
        }

        public Author GetDetailsForAuthor(Guid? id)
        {
            return _authorRepository.Get(id);
        }
    }
}
