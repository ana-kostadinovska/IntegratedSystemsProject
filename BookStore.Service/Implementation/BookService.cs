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

        public BookService(IRepository<Book> bookRepository)
        {
            _bookRepository = bookRepository;
        }

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
            return _bookRepository.Get(id);
        }
    }
}
