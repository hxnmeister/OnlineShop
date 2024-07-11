using OnlineShop.Filters;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShop.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [RequiredMessage(fieldName: "Title")]
        [StringLengthMessage(maximumLength: 100, fieldName: "Title")]
        [DisplayName(displayName: "Title")]
        public required string Title { get; set; }

        [RequiredMessage(fieldName: "Publisher")]
        [StringLengthMessage(maximumLength: 100, fieldName: "Publisher")]
        [DisplayName(displayName: "Publisher")]
        public required string PublisherName { get; set; }

        [RequiredMessage(fieldName: "Genre")]
        [StringLengthMessage(maximumLength: 55, fieldName: "Genre")]
        [DisplayName(displayName: "Genre")]
        public required string Genre { get; set; }

        [RequiredMessage(fieldName: "Rating")]
        [Range(minimum: 1, maximum: 5, ErrorMessage = "Rating value must be between 1 and 5!")]
        [DisplayName(displayName: "Rating")]
        public int Rating { get; set; }

        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "There is must be at least 1 page in book!")]
        [DisplayName(displayName: "Pages")]
        public int? PagesAmount { get; set; }

        [RequiredMessage(fieldName: "Price")]
        [Column(TypeName = "decimal(18,2)")]
        [DisplayName(displayName: "Price")]
        public decimal Price { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PreviousPrice { get; set; }

        [StringLengthMessage(maximumLength: 150, fieldName: "Author")]
        [DisplayName(displayName: "Author")]
        public string? Author { get; set; }

        [DisplayName(displayName: "Url to image")]
        public string? ImageUrl { get; set; }

        public override string ToString()
        {
            return $"Title: {this.Title}\n" +
                   $"Publisher: {this.PublisherName}\n" +
                   $"Genre: {this.Genre}\n" +
                   $"Rating: {this.Rating}\n" +
                   $"Price: {this.Price}" +
                   $"Author(-s): {this.Author}";
        }
    }
}
