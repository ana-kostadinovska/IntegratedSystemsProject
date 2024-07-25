using BookStore.Domain.DTO;
using BookStore.Domain.Identity;
using BookStore.Domain.Models;
using BookStore.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BookStore.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly UserManager<BookStoreUser> _userManager;

        public AdminController(IBookService bookService, UserManager<BookStoreUser> userManager)
        {
            _bookService = bookService;
            _userManager = userManager;
        }


        /*[HttpPost("[action]")]
        public bool ImportAllBooks(List<BookDTO> model)
        {

            foreach (var item in model)
            {
                var book = new Book
                {
                    Title = item.Title,
                    CoverImage = item.CoverImage,
                    ReleaseYear = item.ReleaseYear,
                    ISBN = item.ISBN,
                    Price = item.Price,
                    Edition = item.Edition,
                    Genres = item.Genres,
                    AuthorId = item.AuthorId,
                    PublisherId = item.PublisherId
                };

                if (book.Equals(null))
                    return false;

                _bookService.CreateNewBook(book);

            }

            return true;
        }*/


        [HttpPost("[action]")]
        public bool ImportAllBooks(List<BookDTO> model)
        {
            var existingBooks = _bookService.GetAllBooks();

            foreach (var item in model)
            {
                if (!existingBooks.Any(b => b.ISBN == item.ISBN))
                {
                    var book = new Book
                    {
                        Title = item.Title,
                        CoverImage = item.CoverImage,
                        ReleaseYear = item.ReleaseYear,
                        ISBN = item.ISBN,
                        Price = item.Price,
                        Edition = item.Edition,
                        Genres = item.Genres,
                        AuthorId = item.AuthorId,
                        PublisherId = item.PublisherId
                    };

                    _bookService.CreateNewBook(book);
                }
                else
                {
                    continue;
                }
            }

            return true;
        }

        [HttpPost("[action]")]
        public bool ImportAllUsers(List<UserRegistrationDto> model)
        {
            bool status = true;

            foreach (var item in model)
            {
                var userCheck = _userManager.FindByEmailAsync(item.Email).Result;

                if (userCheck == null)
                {
                    var user = new BookStoreUser
                    {
                        UserName = item.Email,
                        NormalizedUserName = item.Email,
                        Email = item.Email,
                        EmailConfirmed = true,
                        ShoppingCart = new ShoppingCart()
                    };

                    var result = _userManager.CreateAsync(user, item.Password).Result;
                    status = status && result.Succeeded;
                }
                else
                {
                    continue;
                }
            }
            return status;
        }
    }
}
