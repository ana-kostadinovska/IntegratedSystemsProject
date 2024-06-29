using Microsoft.AspNetCore.Mvc;

namespace BookStore.Web.Controllers
{
    public class ShoppingCartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
