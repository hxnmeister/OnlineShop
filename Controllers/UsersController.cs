using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using OnlineShop.Data;
using OnlineShop.Models;

namespace OnlineShop.Controllers
{
    public class UsersController : Controller
    {
        private readonly OnlineShopDBContext _context;
        private readonly IMemoryCache _cache;

        public UsersController(OnlineShopDBContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
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

            return RedirectToAction("Index", "Books");
        }
    }
}
