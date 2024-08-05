using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Filters;
using OnlineShop.Models;
using OnlineShop.Services;
using System.Linq;

namespace OnlineShop.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly OnlineShopDBContext _context;

        public CartController(ICartService cartService, OnlineShopDBContext context)
        {
            _cartService = cartService;
            _context = context;
        }

        [HttpGet(template: "cart")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost(template: "cart/add")]
        public async Task<IActionResult> AddToCart(int bookId, int quantity)
        {
            var book = await _context.Books.Include(b => b.BooksImages).FirstOrDefaultAsync(b => b.Id == bookId);

            if(book != null)
            {
                _cartService.AddItem(new CartItem { Book = book, Quantity = quantity });
            }

            var items = _cartService.CartItems;

            return RedirectToAction("Index", "Books");
        }

        [HttpPost(template: "cart/remove")]
        public IActionResult RemoveFromCart(int bookId) 
        {
            _cartService.RemoveItem(bookId);

            return RedirectToAction("Index", "Cart");
        }

        [HttpGet(template: "cart/checkout")]
        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost(template: "cart/checkout")]
        public IActionResult CheckoutResult()
        {
            return View();
        }
    }
}
