using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Filters
{
    public class StringLengthMessageAttribute : StringLengthAttribute
    {
        public StringLengthMessageAttribute(int maximumLength, string fieldName) : base(maximumLength)
        {
            ErrorMessage = $"Length for {fieldName} must be less than {maximumLength} characters!";
        }
    }
}
