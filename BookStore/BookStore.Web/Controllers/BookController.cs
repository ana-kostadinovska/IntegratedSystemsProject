using BookStore.Domain.Enums;
using BookStore.Domain.Models;
using BookStore.Repository.Interface;
using BookStore.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Policy;

namespace BookStore.Web.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IAuthorService _authorService;
        private readonly IPublisherService _publisherService;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IRepository<Book> _bookRepository;

        public BookController(IBookService bookService, IAuthorService authorService, IPublisherService publisherService, IShoppingCartService shoppingCartService, IRepository<Book> bookRepository)
        {
            _bookService = bookService;
            _authorService = authorService;
            _publisherService = publisherService;
            _shoppingCartService = shoppingCartService;
            _bookRepository = bookRepository;
        }

        public IActionResult Index()
        {
            var books = _bookRepository.GetAllIncluding(b => b.Author, b => b.Publisher).ToList();
            return View(_bookService.GetAllBooks());
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = _bookService.GetDetailsForBook(id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        public IActionResult Create()
        {
            var authors = _authorService.GetAllAuthors()
                                .Select(a => new { Id = a.Id, FullName = a.Name + " " + a.Surname })
                                .ToList();

            ViewBag.Authors = new SelectList(authors, "Id", "FullName");
            ViewBag.Publishers = new SelectList(_publisherService.GetAllPublishers(), "Id", "Name");

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Title,CoverImage,ReleaseYear,ISBN,Price,Edition,Genres,AuthorId,PublisherId")] Book book)
        {
            if (ModelState.IsValid)
            {
                book.Id = Guid.NewGuid();
                _bookService.CreateNewBook(book);
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }  

        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = _bookService.GetDetailsForBook(id);
            if (book == null)
            {
                return NotFound();
            }

            ViewBag.Genres = Enum.GetValues(typeof(GenreEnum)).Cast<GenreEnum>().ToList();

            var authors = _authorService.GetAllAuthors()
                               .Select(a => new { Id = a.Id, FullName = a.Name + " " + a.Surname })
                               .ToList();

            ViewBag.Authors = new SelectList(authors, "Id", "FullName", book.AuthorId);
            ViewData["Publishers"] = new SelectList(_publisherService.GetAllPublishers(), "Id", "Name", book.PublisherId);

            return View(book);
        }
  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id,Title,CoverImage,ReleaseYear,ISBN,Price,Edition,Genres,AuthorId,PublisherId")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _bookService.EditExistingBook(book);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = _bookService.GetDetailsForBook(id);
            if (book == null)
            {
                return NotFound();
            }

            var authors = _authorService.GetAllAuthors()
                                .Select(a => new { Id = a.Id, FullName = a.Name + " " + a.Surname })
                                .ToList();

            ViewBag.Authors = new SelectList(authors, "Id", "FullName", book.AuthorId);
            ViewData["Publishers"] = new SelectList(_publisherService.GetAllPublishers(), "Id", "Name", book.PublisherId);

            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _bookService.DeleteBook(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult AddToShoppingCart(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = _bookService.GetDetailsForBook(id);

            BookInShoppingCart bs = new BookInShoppingCart();

            if (book != null)
            {
                bs.BookId = book.Id;
            }

            return View(bs);
        }

        [HttpPost]
        public IActionResult AddToShoppingCartConfirmed(BookInShoppingCart model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _shoppingCartService.AddToShoppingConfirmed(model, userId);

            return View("Index", _bookService.GetAllBooks());
        }
    }
}
