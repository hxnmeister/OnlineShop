using Microsoft.EntityFrameworkCore;
using OnlineShop.Models;

namespace OnlineShop.Data
{
    public class OnlineShopDBContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<BooksImages> BooksImages { get; set; }

        public OnlineShopDBContext(DbContextOptions<OnlineShopDBContext> options) : base(options) {}
    }
}
