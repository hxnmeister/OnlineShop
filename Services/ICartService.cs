using OnlineShop.Models;

namespace OnlineShop.Services
{
    public interface ICartService
    {
        public List<CartItem> CartItems { get; }

        int AddItem(CartItem cartItem);
        void RemoveItem(int bookId);
        decimal GetTotalPrice();
    }
}
