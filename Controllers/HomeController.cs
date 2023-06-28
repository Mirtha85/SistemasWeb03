using System.Diagnostics;
using System.IO.Pipelines;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using SistemasWeb01.Models;
using SistemasWeb01.Repository.Interfaces;
using SistemasWeb01.ViewModels;

namespace SistemasWeb01.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly ISubCategoryRepository _subCategoryRepository;
    private readonly IProductSizeRepository _productSizeRepository;
    private readonly IShoppingCart _shoppingCart;

    public HomeController(ILogger<HomeController> logger, IProductRepository productRepository, ICategoryRepository categoryRepository, ISubCategoryRepository subCategoryRepository, IProductSizeRepository productSizeRepository, IShoppingCart shoppingCart)
    {
        _logger = logger;
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _subCategoryRepository = subCategoryRepository;
        _productSizeRepository = productSizeRepository;
        _shoppingCart = shoppingCart;
    }

    public IActionResult Index()
    {
        IEnumerable<Product> products = _productRepository.BestSellingProducts;
        IEnumerable<Category> categories = _categoryRepository.AllCategories;
        HomeViewModel model = new HomeViewModel(products, categories);
        return View(model);
    }

    


    public IActionResult ListProducts()
    {
        IEnumerable<Product> products = _productRepository.AllProducts;
        IEnumerable<Category> categories = _categoryRepository.AllCategories;
        HomeViewModel model = new HomeViewModel(products, categories);
        return View(model);
    }


    public IActionResult AddToCart(int id)
    {
        Product? product = _productRepository.GetProductById(id);

        if (product == null)
        {
            return NotFound();
        }
        List<SelectListItem> tallas = _productSizeRepository.GetSizesByProductId(product.Id)
                .OrderBy(t => t.Id)
                     .Select(t =>
                      new SelectListItem
                      {
                          Value = t.Id.ToString(),
                          Text = t.Talla.ShortName
                      }).ToList();
        CartItemViewModel model = new()
        {
            Product = product,
            ProductId = product.Id,
            ProductSizes = tallas,
            Amount = 1

        };
        return View(model);
    }

    [HttpPost]
    public IActionResult AddToCart(CartItemViewModel model)
    {
        Product? product = _productRepository.GetProductById(model.ProductId);
        if (product != null)
        {
            ProductSize? productSize = _productSizeRepository.GetProductSizeById(model.ProductSizeId);
            _shoppingCart.AddToCart(product, productSize, model.Amount);
            return RedirectToAction("Index");
        }
         return View(model);
    }


    public ViewResult ShowCart()
    {
        var items = _shoppingCart.GetShoppingCartItems();
        _shoppingCart.ShoppingCartItems = items;

        var shoppingCartViewModel = new ShoppingCartViewModel(_shoppingCart, _shoppingCart.GetShoppingCartTotal());

        return View(shoppingCartViewModel);
    }





























    public ViewResult List(string category)
    {
        IEnumerable<Product> product;
        string? currentCategory;

        if (string.IsNullOrEmpty(category))
        {
            product = _productRepository.AllProducts.OrderBy(p => p.Id);
            currentCategory = "Todos";
        }
        else
        {
            product = _productRepository.AllProducts.Where(p => p.SubCategory.Category.Name == category)
                .OrderBy(p => p.Id);
            currentCategory = _categoryRepository.AllCategories.FirstOrDefault(c => c.Name == category)?.Name;
        }

        return View(new ProductListViewModel(product, currentCategory));
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
