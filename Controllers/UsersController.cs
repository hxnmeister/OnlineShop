using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using OnlineShop.Data;
using OnlineShop.Models;

namespace OnlineShop.Controllers
{
    public class UsersController : Controller
    {
        private readonly OnlineShopDBContext _context;
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _cache;
        private readonly string currentUserRoleKey;
        private readonly string currentUserLoginKey;

        public UsersController(OnlineShopDBContext context, IMemoryCache cache, IConfiguration configuration)
        {
            _context = context;
            _cache = cache;
            _configuration = configuration;

            currentUserRoleKey = _configuration["SessionSettings:CurrentUserRoleKey"] ?? "current_user_role";
            currentUserLoginKey = _configuration["SessionSettings:CurrentUserLoginKey"] ?? "current_user_login";
        }

        [HttpGet(template: "signup")]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost(template: "signup")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(SignUp user)
        {
            if (ModelState.IsValid)
            {
                if ((await _context.Users.FirstOrDefaultAsync(item => item.Email == user.Email)) != null)
                {
                    ModelState.AddModelError("Email", "Such email is already using!");

                    return View(user);
                }
                else if ((await _context.Users.FirstOrDefaultAsync(item => item.Login == user.Login)) != null)
                {
                    ModelState.AddModelError("Login", "Such login is already using!");

                    return View(user);
                }

                    

                User newUser = new()
                {
                    Id = (int)(user.Id == null ? _context.Users.Count() + 1 : user.Id),
                    Login = user.Login,
                    Password = user.Password,
                    FirstName = user.FirstName, 
                    LastName = user.LastName,
                    Age = user.Age,
                    Email = user.Email
                };

                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();

                return RedirectToAction("SignIn");
            }
            return View(user);
        }

        [HttpGet(template: "signin")]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost(template: "signin")]
        public async Task<IActionResult> SignIn(SignIn user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            User? currentUser = await _context.Users.FirstOrDefaultAsync(item => item.Login.Equals(user.Login) && item.Password.Equals(user.Password));

            if (currentUser == null)
            {
                ModelState.AddModelError("Login", "There is no user with such login!");

                return View(user);
            }

            HttpContext.Session.SetString(currentUserLoginKey, currentUser.Login);
            HttpContext.Session.SetString(currentUserRoleKey, currentUser.Role);

            return RedirectToAction("Index", "Books");
        }

        [HttpGet(template: "logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove(currentUserRoleKey);
            HttpContext.Session.Remove(currentUserLoginKey);
            return RedirectToAction("Index", "Books");
        }

        [HttpGet(template: "access-denied")]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
