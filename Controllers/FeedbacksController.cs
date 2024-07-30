using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Filters;
using OnlineShop.Models;

namespace OnlineShop.Controllers
{
    public class FeedbacksController : Controller
    {
        private readonly OnlineShopDBContext _context;
        private readonly IConfiguration _configuration;
        private readonly string currentUserLoginKey;

        public FeedbacksController(OnlineShopDBContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;

            currentUserLoginKey = _configuration["SessionSettings:CurrentUserLoginKey"] ?? "current_user_login";
        }

        [HttpGet(template: "feedback/create")]
        [UserAuth(userRoles: ["USER", "ADMIN"])]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Feedbacks/Create
        [HttpPost(template: "feedback/create")]
        [UserAuth(userRoles: ["USER", "ADMIN"])]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind([ "Title", "Body" ])]Feedback feedback, int bookId)
        {
            if (ModelState.IsValid)
            {
                string userLogin = HttpContext.Session.GetString(currentUserLoginKey) ?? string.Empty;
                User? user = await _context.Users.FirstOrDefaultAsync(u => u.Login.Equals(userLogin));

                if(user != null)
                {
                    feedback.BookId = bookId;
                    feedback.UserId = user.Id;

                    _context.Add(feedback);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Details", "Books", new { id = bookId });
                }

                return BadRequest();
            }

            return View(feedback);
        }

        // GET: Feedbacks/Delete/5
        [HttpGet(template: "feedback/delete/{id}")]
        [UserAuth(userRoles: ["USER", "ADMIN"])]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feedback = await _context.Feedbacks
                .Include(f => f.Book)
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (feedback == null)
            {
                return NotFound();
            }

            return View(feedback);
        }

        // POST: Feedbacks/Delete/5
        [HttpPost(template: "feedback/delete/{id}"), ActionName("Delete")]
        [UserAuth(userRoles: ["USER", "ADMIN"])]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var feedback = await _context.Feedbacks.FindAsync(id);

            if (feedback != null)
            {
                int bookId = feedback.BookId;
                _context.Feedbacks.Remove(feedback);

                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Books", new { id = bookId });
            }

            return RedirectToAction("Index", "Books");
        }

        private bool FeedbackExists(int id)
        {
            return _context.Feedbacks.Any(e => e.Id == id);
        }
    }
}
