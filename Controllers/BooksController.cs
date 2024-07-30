using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Filters;
using OnlineShop.Models;
using OnlineShop.Services;
using System.Linq;

namespace OnlineShop.Controllers
{
    public class BooksController : Controller
    {
        private readonly OnlineShopDBContext _context;
        private readonly IUserService _userService;

        public BooksController(OnlineShopDBContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            return View(await _context.Books.Include(b => b.BooksImages).ToListAsync());
        }

        // GET: Books/Details/5
        [HttpGet(template: "books/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.Include(b => b.BooksImages)
                .Include(b => b.Feedbacks)
                .ThenInclude(f => f.User)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            ViewBag.Feedbacks = book.Feedbacks;
            ViewBag.HasUserLeftFeedback = book.Feedbacks.Any(f => f.User.Login.Equals(_userService.CurrentUserLogin));

            return View(book);
        }

        // GET: Books/Create
        [UserAuth(userRoles: ["ADMIN"])]
        [HttpGet(template: "books/create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        [UserAuth(userRoles: ["ADMIN"])]
        [HttpPost(template: "books/create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,PublisherName,Genre,Rating,PagesAmount,Price,PreviousPrice,Author,ImageUrl")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Edit/5
        [UserAuth(userRoles: ["ADMIN"])]
        [HttpGet(template: "books/edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Edit/5
        [UserAuth(userRoles: ["ADMIN"])]
        [HttpPost(template: "books/edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,PublisherName,Genre,Rating,PagesAmount,Price,PreviousPrice,Author,ImageUrl")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Book? currentBook = await _context.Books.AsNoTracking().FirstOrDefaultAsync(item => item.Id == id);

                    if(currentBook == null)
                    {
                        return NotFound();
                    }
                    else if(currentBook.Price !=  book.Price)
                    {
                        book.PreviousPrice = currentBook.Price;
                    }

                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Delete/5
        [UserAuth(userRoles: ["ADMIN"])]
        [HttpGet(template: "books/delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FirstOrDefaultAsync(m => m.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [UserAuth(userRoles: ["ADMIN"])]
        [HttpPost(template: "books/delete/{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}
