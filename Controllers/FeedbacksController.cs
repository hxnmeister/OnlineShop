using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        [HttpGet]
        [UserAuth(userRoles: ["USER", "ADMIN"])]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Feedbacks/Create
        [HttpPost]
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

        // GET: Feedbacks/Edit/5
        [HttpGet]
        [UserAuth(userRoles: ["USER", "ADMIN"])]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feedback = await _context.Feedbacks.FindAsync(id);
            if (feedback == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Genre", feedback.BookId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", feedback.UserId);
            return View(feedback);
        }

        // POST: Feedbacks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [UserAuth(userRoles: ["USER", "ADMIN"])]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Body,BookId,UserId")] Feedback feedback)
        {
            if (id != feedback.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(feedback);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FeedbackExists(feedback.Id))
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

            return View(feedback);
        }

        // GET: Feedbacks/Delete/5
        [HttpGet]
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
        [HttpPost, ActionName("Delete")]
        [UserAuth(userRoles: ["USER", "ADMIN"])]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var feedback = await _context.Feedbacks.FindAsync(id);
            if (feedback != null)
            {
                _context.Feedbacks.Remove(feedback);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FeedbackExists(int id)
        {
            return _context.Feedbacks.Any(e => e.Id == id);
        }
    }
}
