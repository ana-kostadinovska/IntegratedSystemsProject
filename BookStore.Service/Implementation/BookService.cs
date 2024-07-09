using BookStore.Domain.Models;
using BookStore.Repository.Interface;
using BookStore.Service.Interface;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service.Implementation
{
    public class BookService : IBookService
    {
        private readonly IRepository<Book> _bookRepository;
        private readonly IRepository<Author> _authorRepository;
        private readonly IRepository<Publisher> _publisherRepository;

        public BookService(IRepository<Book> bookRepository, IRepository<Author> authorRepository, IRepository<Publisher> publisherRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _publisherRepository = publisherRepository;
        }



        /*public BookService(IRepository<Book> bookRepository)
        {
            _bookRepository = bookRepository;
        }*/

        public void CreateNewBook(Book book)
        {
            _bookRepository.Insert(book);
        }

        public void DeleteBook(Guid id)
        {
            Book bookToDelete = _bookRepository.Get(id);
            _bookRepository.Delete(bookToDelete);
        }

        public void EditExistingBook(Book book)
        {
            _bookRepository.Update(book);
        }

        public List<Book> GetAllBooks()
        {
            return _bookRepository.GetAll().ToList();
        }

        public Book GetDetailsForBook(Guid? id)
        {
            /*Book book = _bookRepository.Get(id);
            if(book != null)
            {
                book.Author = _authorRepository.Get(book.AuthorId);
                book.Publisher = _publisherRepository.Get(book.PublisherId);
            }*/

            return _bookRepository.Get(id);
            //return book;
        }
    }
}
