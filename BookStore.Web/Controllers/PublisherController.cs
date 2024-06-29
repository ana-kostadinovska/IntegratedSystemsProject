using Microsoft.AspNetCore.Mvc;

namespace BookStore.Web.Controllers
{
    public class PublisherController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
