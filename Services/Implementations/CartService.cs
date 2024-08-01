using OnlineShop.Models;

namespace OnlineShop.Services.Implementations
{
    public class CartService : ICartService
    {
        public List<CartItem> CartItems { get; private set; } = new List<CartItem>();

        public int AddItem(CartItem cartItem)
        {
            var item = CartItems.FirstOrDefault(i => i.Book.Id == cartItem.Book.Id);

            if (item == null)
            {
                CartItems.Add(cartItem);
            }
            else
            {
                item.Quantity += cartItem.Quantity;
            }

            return CartItems.Count;
        }

        public void RemoveItem(int bookId)
        {
            var item = CartItems.FirstOrDefault(i => i.Book.Id == bookId);

            if (item != null)
            {
                --item.Quantity;

                if(item.Quantity == 0)
                {
                    CartItems.Remove(item);
                }
            }
        }

        public decimal GetTotalPrice()
        {
            return CartItems.Sum(i => i.Quantity * i.Book.Price);
        }
    }
}
