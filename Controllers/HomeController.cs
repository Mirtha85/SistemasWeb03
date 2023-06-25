using System.Diagnostics;
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
   
    
    public HomeController(ILogger<HomeController> logger, IProductRepository productRepository, ICategoryRepository categoryRepository, ISubCategoryRepository subCategoryRepository, IProductSizeRepository productSizeRepository)
    {
        _logger = logger;
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _subCategoryRepository = subCategoryRepository;
        _productSizeRepository = productSizeRepository;


    }

    public IActionResult Index()
    {
        IEnumerable<Product> products = _productRepository.BestSellingProducts;
        IEnumerable<Category> categories = _categoryRepository.AllCategories;
        HomeViewModel model = new HomeViewModel(products, categories);
        return View(model);
    }

    public IActionResult DetailsProduct(int id) 
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
        string tallasList = "";
        foreach(ProductSize productSize in product.ProductSizes!)
        {
            tallasList += $"•  {productSize.Talla.ShortName}      ";
        }
        DetailsProductViewModel model = new DetailsProductViewModel(product, tallas);
        model.TallasDisponibles = tallasList;

        return View(model);
    }

    public IActionResult DetallePrueba(int id)
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
        string tallasList = "";
        foreach (ProductSize productSize in product.ProductSizes!)
        {
            tallasList += $"•  {productSize.Talla.ShortName}      ";
        }
        DetailsProductViewModel model = new DetailsProductViewModel(product, tallas);
        model.TallasDisponibles = tallasList;

        return View(model);
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
