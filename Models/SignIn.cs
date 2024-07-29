using OnlineShop.Filters;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Models
{
    public class SignIn
    {
        [RequiredMessage(fieldName: "Login")]
        [DisplayName(displayName: "Login")]
        public required string Login { get; set; }

        [RequiredMessage(fieldName: "Password")]
        [DisplayName(displayName: "Password")]
        public required string Password { get; set; }

        [Compare(otherProperty: "Password", ErrorMessage = "Passwords didn`t match!")]
        [DisplayName(displayName: "Repeat password")]
        public required string RepeatPassword { get; set; }

        public string Role { get; set; } = "USER";
    }
}
