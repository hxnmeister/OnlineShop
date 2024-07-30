using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Models
{
    public class BooksImages
    {
        [Key]
        public int Id { get; set; }

        public string? ImageUrl { get; set; }

        public int BookId { get; set; }
        public Book? Book { get; set; }
    }
}
