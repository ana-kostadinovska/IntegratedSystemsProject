using BookStore.Domain.DTO;
using BookStore.Domain.Models;
using BookStore.Repository.Interface;
using BookStore.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service.Implementation
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IRepository<ShoppingCart> _shoppingCartRepository;
        private readonly IRepository<BookInShoppingCart> _bookInCartRepository;
        private readonly IRepository<BookInOrder> _bookInOrderRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRepository<Order> _orderRepository;

        public ShoppingCartService(IRepository<ShoppingCart> shoppingCartRepository, IRepository<BookInShoppingCart> bookInCartRepository, IRepository<BookInOrder> bookInOrderRepository, IUserRepository userRepository, IRepository<Order> orderRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _bookInCartRepository = bookInCartRepository;
            _bookInOrderRepository = bookInOrderRepository;
            _userRepository = userRepository;
            _orderRepository = orderRepository;
        }

        public bool AddToShoppingConfirmed(BookInShoppingCart model, string userId)
        {
            var loggedInUser = _userRepository.Get(userId);

            if(loggedInUser == null)
            {
                return false;
            }

            var userCart = loggedInUser.ShoppingCart;

            if (userCart.BooksInShoppingCart == null)
                userCart.BooksInShoppingCart = new List<BookInShoppingCart>(); ;

            userCart.BooksInShoppingCart.Add(model);
            _shoppingCartRepository.Update(userCart);

            return true;
        }

        public bool deleteProductFromShoppingCart(string userId, Guid bookId)
        {
            if (bookId != null)
            {
                var loggedInUser = _userRepository.Get(userId);

                if (loggedInUser == null)
                {
                    return false;
                }

                var userCart = loggedInUser.ShoppingCart;
                var book = userCart.BooksInShoppingCart.Where(x => x.BookId == bookId).FirstOrDefault();

                if (book == null)
                {
                    return false;
                }

                userCart.BooksInShoppingCart.Remove(book);

                _shoppingCartRepository.Update(userCart);
                return true;
            }
            return false;
        }

        public ShoppingCartDto getShoppingCartInfo(string userId)
        {
            var loggedInUser = _userRepository.Get(userId);

            var userCart = loggedInUser?.ShoppingCart;
            var booksInShoppingCart = userCart?.BooksInShoppingCart?.ToList();

            var totalPrice = booksInShoppingCart.Select(x => (x.Book.Price * x.Quantity)).Sum();

            ShoppingCartDto dto = new ShoppingCartDto
            {
                Books = booksInShoppingCart,
                TotalPrice = totalPrice
            };
            return dto;
        }

        public bool order(string userId)
        {
            if (userId != null)
            {
                var loggedInUser = _userRepository.Get(userId);

                if(loggedInUser == null)
                {
                    return false;
                }

                var userShoppingCart = loggedInUser.ShoppingCart;
                // TODO: Implement Stripe
                //EmailMessage message = new EmailMessage();
                //message.Subject = "Successfull order";
                //message.MailTo = loggedInUser.Email;

                Order order = new Order
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    User = loggedInUser
                };

                _orderRepository.Insert(order);

                List<BookInOrder> booksInOrder = userShoppingCart.BooksInShoppingCart.Select(
                    x => new BookInOrder
                    {
                        Id = Guid.NewGuid(),
                        BookId = x.Book.Id,
                        Book = x.Book,
                        OrderId = order.Id,
                        Order = order,
                        Quantity = x.Quantity
                    }
                    ).ToList();


                //StringBuilder sb = new StringBuilder();

                var totalPrice = 0.0;

                //sb.AppendLine("Your order is completed. The order conatins: ");

                for (int i = 0; i < booksInOrder.Count(); i++)
                {
                    var currentItem = booksInOrder[i];
                    totalPrice += currentItem.Quantity * currentItem.Book.Price;
                    //sb.AppendLine(i.ToString() + ". " + currentItem.Book.ProductName + " with quantity of: " + currentItem.Quantity + " and price of: $" + currentItem.Product.Price);
                }

               /* sb.AppendLine("Total price for your order: " + totalPrice.ToString());
                message.Content = sb.ToString();*/


                foreach (var product in booksInOrder)
                {
                    _bookInOrderRepository.Insert(product);
                }

                loggedInUser.ShoppingCart.BooksInShoppingCart.Clear();
                _userRepository.Update(loggedInUser);
                //this._emailService.SendEmailAsync(message);

                return true;
            }
            return false;
        }
    }
    
}
