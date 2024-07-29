namespace OnlineShop.Services
{
    public interface IUserService
    {
        string CurrentUserLogin { get; }
        string CurrentUserRole { get; }

        string GetAllUserData();
        bool IsAdmin();
        bool IsCommonUser();
    }
}
