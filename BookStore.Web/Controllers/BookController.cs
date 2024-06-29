using Microsoft.AspNetCore.Mvc;

namespace BookStore.Web.Controllers
{
    public class BookController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
