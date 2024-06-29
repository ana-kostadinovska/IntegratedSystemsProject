using Microsoft.AspNetCore.Mvc;

namespace BookStore.Web.Controllers
{
    public class AuthorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
