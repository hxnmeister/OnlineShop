using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Filters
{
    public class RequiredMessageAttribute : RequiredAttribute
    {
        public RequiredMessageAttribute(string fieldName) 
        {
            ErrorMessage = $"The {fieldName} is required!";
        }
    }
}
