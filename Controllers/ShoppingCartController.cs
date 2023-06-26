using Microsoft.AspNetCore.Mvc;
using SistemasWeb01.Models;
using SistemasWeb01.Repository.Interfaces;
using SistemasWeb01.ViewModels;

namespace SistemasWeb01.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductSizeRepository _productSizeRepository;
        private readonly IShoppingCart _shoppingCart;

        public ShoppingCartController(IProductRepository productRepository, IShoppingCart shoppingCart)
        {
            _productRepository = productRepository;
            _shoppingCart = shoppingCart;

        }
        public ViewResult Index()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var shoppingCartViewModel = new ShoppingCartViewModel(_shoppingCart, _shoppingCart.GetShoppingCartTotal());

            return View(shoppingCartViewModel);
        }

        //public RedirectToActionResult AddToShoppingCart(DetailsProductViewModel model)
        //{
        //    var selectedProduct = _productRepository.AllProducts.FirstOrDefault(p => p.Id == model.Product.Id);
        //    model.productSize = _
        //    if (selectedProduct != null)
        //    {
                
                
        //        //_shoppingCart.AddToCart(selectedProduct);
        //    }
        //    return RedirectToAction("Index");
        //}

        //public RedirectToActionResult RemoveFromShoppingCart(int id)
        //{
        //    var selectedPie = _productRepository.AllProducts.FirstOrDefault(p => p.Id == id);

        //    if (selectedPie != null)
        //    {
        //        _shoppingCart.RemoveFromCart(selectedPie);
        //    }
        //    return RedirectToAction("Index");
        //}
    }
}
