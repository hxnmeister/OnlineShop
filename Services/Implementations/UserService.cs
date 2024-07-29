
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Text;

namespace OnlineShop.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly string userLoginSessionKey;
        private readonly string userRoleSessionKey;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public string CurrentUserLogin {  get; private set; }
        public string CurrentUserRole { get; private set; }

        public UserService(string? userLoginSessionKey, string? userRoleSessionKey, IHttpContextAccessor httpContextAccessor)
        {
            this.userLoginSessionKey = userLoginSessionKey ?? "current_user_login";
            this.userRoleSessionKey = userRoleSessionKey ?? "current_user_role";

            _httpContextAccessor = httpContextAccessor;

            CurrentUserLogin = _httpContextAccessor.HttpContext.Session.GetString(this.userLoginSessionKey) ?? string.Empty;
            CurrentUserRole = _httpContextAccessor.HttpContext.Session.GetString(this.userRoleSessionKey) ?? string.Empty;
        }

        public string GetAllUserData() => $"Login:{CurrentUserLogin}\nRole:{CurrentUserRole}";

        public bool IsAdmin() => CurrentUserRole.Equals("ADMIN");

        public bool IsCommonUser() => CurrentUserRole.Equals("USER");
    }
}
