using OnlineShop.Filters;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Models
{
    public class Feedback
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(length: 10, ErrorMessage = "Title must be at least 10 characters in length!")]
        public required string Title { get; set; }

        [RequiredMessage(fieldName: "Text")]
        [DisplayName(displayName: "Left feedback here...")]
        [MinLength(length: 20, ErrorMessage = "Feedback must be at least 20 characters in length!")]
        public required string Body { get; set; }

        public int BookId { get; set; }
        public Book? Book { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
