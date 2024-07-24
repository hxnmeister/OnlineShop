using OnlineShop.Filters;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [RequiredMessage(fieldName: "Login")]
        [DisplayName(displayName: "Login")]
        public required string Login {  get; set; }

        [RequiredMessage(fieldName: "Password")]
        [DisplayName(displayName: "Password")]
        public required string Password { get; set; }

        [RequiredMessage(fieldName: "Email")]
        [EmailAddress]
        [DisplayName(displayName: "Email")]
        public required string Email { get; set; }

        [RequiredMessage(fieldName: "FirstName")]
        [DisplayName(displayName: "First name")]
        public required string FirstName { get; set; }

        [RequiredMessage(fieldName: "LastName")]
        [DisplayName(displayName: "Last name")]
        public required string LastName { get; set; }

        [Range(minimum: 16, maximum: 100, ErrorMessage = "Value for Age must be between 16 and 100!")]
        [DisplayName(displayName: "Age")]
        public int? Age { get; set; }
    }
}
