using BookStore.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Web.Controllers
{
    public class PartnerBooksController : Controller
    {

        private readonly IPartnerTeamService _partnerTeamService;

        public PartnerBooksController(IPartnerTeamService partnerTeamService)
        {
            _partnerTeamService = partnerTeamService;
        }

        public IActionResult Index()
        {
            var books = _partnerTeamService.GetPartnerBooks();
            return View(books);
        }
    }
}
