using BookStore.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using Stripe;
using BookStore.Domain;

namespace BookStore.Web.Controllers
{
    public class ShoppingCartController : Controller
    {

        private readonly IShoppingCartService _shoppingCartService;
        private readonly StripeSettings _stripeSettings;

        public ShoppingCartController(IShoppingCartService _shoppingCartService, IOptions<StripeSettings> stripeSettings) //, IOptions<StripeSettings> stripeSettings
        {
            this._shoppingCartService = _shoppingCartService;
            _stripeSettings = stripeSettings.Value;
        }

        // GET: ShoppingCarts
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var dto = _shoppingCartService.getShoppingCartInfo(userId);
            return View(dto);
            //return (IActionResult)dto;
        }


        public IActionResult DeleteFromShoppingCart(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _shoppingCartService.deleteProductFromShoppingCart(userId, id);

            return RedirectToAction("Index");

        }


        public IActionResult order()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = _shoppingCartService.order(userId);
            //if (result == true)
            return RedirectToAction("Index", "ShoppingCarts");


        }

        public IActionResult SuccessPayment()
        {
            return View();
        }

        public IActionResult NotsuccessPayment()
        {
            return View();
        }




        // public IActionResult PayOrder(string stripeEmail, string stripeToken)
        public IActionResult PayOrder(string stripeEmail, string stripeToken)
        {
            StripeConfiguration.ApiKey = _stripeSettings.SecretKey;
            var customerService = new CustomerService();
            var chargeService = new ChargeService();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var order = this._shoppingCartService.getShoppingCartInfo(userId);

            var customer = customerService.Create(new CustomerCreateOptions
            {
                Email = stripeEmail,
                Source = stripeToken
            });

            var charge = chargeService.Create(new ChargeCreateOptions
            {
                Amount = (Convert.ToInt32(order.TotalPrice) * 100),
                Description = "BookSAM payment",
                Currency = "mkd",
                Customer = customer.Id
            });

            if (charge.Status == "succeeded")
            {
                this.order();
                return RedirectToAction("SuccessPayment");

            }
            else
            {
                return RedirectToAction("NotsuccessPayment");
            }
        }
    }
}
