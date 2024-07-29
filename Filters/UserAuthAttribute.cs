using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace OnlineShop.Filters
{
    public class UserAuthAttribute : ActionFilterAttribute
    {
        private readonly string[] _userRoles;

        public UserAuthAttribute(string[] userRoles)
        {
            this._userRoles = userRoles;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            string? currentUserRole = context.HttpContext.Session.GetString(configuration["SessionSettings:CurrentUserRoleKey"] ?? "current_user_role") ?? "ANONIMUS";

            if(!_userRoles.Contains(currentUserRole) && !_userRoles.Contains("ALL"))
            {
                context.Result = new RedirectToActionResult("AccessDenied", "Users", null);
            }
        }
    }
}
