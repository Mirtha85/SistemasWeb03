using SistemasWeb01.ViewModels;
using System.IO.Pipelines;

namespace SistemasWeb01.Models
{
    public interface IShoppingCart
    {
        void AddToCart(Product product, ProductSize productSize, int amount);
        int RemoveFromCart(Product product);
        List<ShoppingCartItem> GetShoppingCartItems();
        void ClearCart();
        decimal GetShoppingCartTotal();
        List<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
