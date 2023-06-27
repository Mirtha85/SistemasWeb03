using Microsoft.EntityFrameworkCore;
using SistemasWeb01.DataAccess;
using SistemasWeb01.Models;
using SistemasWeb01.ViewModels;
using System.IO.Pipelines;

namespace SistemasWeb01.Repository.Implementations
{
    public class ShoppingCart : IShoppingCart
    {
        private readonly ShoppingDbContext _shoppingDbContext;

        public string? ShoppingCartId { get; set; }

        public List<ShoppingCartItem> ShoppingCartItems { get; set; } = default!;

        private ShoppingCart(ShoppingDbContext shoppingDbContext)
        {
            _shoppingDbContext = shoppingDbContext;
        }
        /*This method we didn't have on our interface, it is a static method
         * It will return me a fully created ShoppingCart
         * I am passing a services colletion
         * When the user visits the site this code is going to run and it's going to check if there is already
         * and ID called CartId available for the user.If not the will create a new GUID and restore the values as the CartId.
         * When the user is returning, we'll be able to find the existing CartId and we'll use that.
         */
        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession? session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.Session;

            ShoppingDbContext context = services.GetService<ShoppingDbContext>() ?? throw new Exception("Error initializing");

            string cartId = session?.GetString("CartId") ?? Guid.NewGuid().ToString();

            session?.SetString("CartId", cartId);

            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }

        //public void AddToCartt(Product product)
        //{
        //    var shoppingCartItem =
        //            _shoppingDbContext.ShoppingCartItems.SingleOrDefault(
        //                s => s.Product.Id == product.Id && s.ShoppingCartId == ShoppingCartId);

        //    if (shoppingCartItem == null)
        //    {
        //        shoppingCartItem = new ShoppingCartItem
        //        {
        //            ShoppingCartId = ShoppingCartId,
        //            Product = product,
        //            Amount = 1
        //        };

        //        _shoppingDbContext.ShoppingCartItems.Add(shoppingCartItem);
        //    }
        //    else
        //    {
        //        shoppingCartItem.Amount++;
        //    }
        //    _shoppingDbContext.SaveChanges();
        //}
        public void AddToCart(Product product, ProductSize productSize, int amount)
        {
            var shoppingCartItem =
                    _shoppingDbContext.ShoppingCartItems.SingleOrDefault(
                        s => s.Product.Id == product.Id && s.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Product = product,
                    Amount = amount,
                    ProductSize = productSize
                };

                _shoppingDbContext.ShoppingCartItems.Add(shoppingCartItem);
            }
            _shoppingDbContext.SaveChanges();
        }




        public int RemoveFromCart(Product product)
        {
            var shoppingCartItem =
                    _shoppingDbContext.ShoppingCartItems.SingleOrDefault(
                        s => s.Product.Id == product.Id && s.ShoppingCartId == ShoppingCartId);

            var localAmount = 0;

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    localAmount = shoppingCartItem.Amount;
                }
                else
                {
                    _shoppingDbContext.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }

            _shoppingDbContext.SaveChanges();

            return localAmount;
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ??=
                       _shoppingDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                            .Include(s => s.Product)
                            .ThenInclude(sp => sp.Pictures)
                            .Include(s => s.ProductSize)
                            .ThenInclude(sp => sp.Talla)
                            .ToList();
        }

        public void ClearCart()
        {
            var cartItems = _shoppingDbContext
                .ShoppingCartItems
                .Where(cart => cart.ShoppingCartId == ShoppingCartId);

            _shoppingDbContext.ShoppingCartItems.RemoveRange(cartItems);

            _shoppingDbContext.SaveChanges();
        }

        public decimal GetShoppingCartTotal()
        {
            var total = _shoppingDbContext.ShoppingCartItems
                .Where(c => c.ShoppingCartId == ShoppingCartId)
                .ToList() // force to handle it as C# object
                .Select(c => c.Product.Price * c.Amount).Sum();
            return total;
        }
    }
}
