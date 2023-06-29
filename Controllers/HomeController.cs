using System.Diagnostics;
using System.IO.Pipelines;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using SistemasWeb01.Helpers;
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
    private readonly ICombosHelper _combosHelper;
    

    private readonly IUserRepository _userRepository;
    private readonly ITemporalSaleRepository _temporalSaleRepository;

    public HomeController(IUserRepository userRepository, ILogger<HomeController> logger, IProductRepository productRepository, ICategoryRepository categoryRepository, ISubCategoryRepository subCategoryRepository, IProductSizeRepository productSizeRepository, IShoppingCart shoppingCart, ITemporalSaleRepository temporalSaleRepository, ICombosHelper combosHelper)
    {
        _userRepository = userRepository;

        _logger = logger;
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _subCategoryRepository = subCategoryRepository;
        _productSizeRepository = productSizeRepository;
        _shoppingCart = shoppingCart;
        _temporalSaleRepository = temporalSaleRepository;
        _combosHelper = combosHelper;
    }

    public async Task<IActionResult> Index()
    {
        IEnumerable<Product> products = _productRepository.BestSellingProducts;
        IEnumerable<Category> categories = _categoryRepository.AllCategories;
        HomeViewModel model = new HomeViewModel(products, categories);
        User user = await _userRepository.GetUserAsync(User.Identity.Name);
        if(user != null)
        {
            model.Quantity = _temporalSaleRepository.GetTemporalSalesByUserId(user.Id).Count();
        }
        return View(model);
    }

    


    public IActionResult ListProducts()
    {
        IEnumerable<Product> products = _productRepository.AllProducts;
        IEnumerable<Category> categories = _categoryRepository.AllCategories;
        HomeViewModel model = new HomeViewModel(products, categories);
        return View(model);
    }


    // ------ 2da FORMA ----------------
    public async Task<IActionResult> AddToCart(int id)
    {
        
        if (!User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Login", "Account");
        }

        Product? product =  _productRepository.GetProductById(id);
        if (product == null)
        {
            return NotFound();
        }

        User user = await _userRepository.GetUserAsync(User.Identity.Name); //siempre devuelve el correo del usuario logeado
        if (user == null)
        {
            return NotFound();
        }

        TemporalSale temporalSale = new()
        {
            Product = product,
            Quantity = 1,
            User = user
        };

        _temporalSaleRepository.CreateTempalSale(temporalSale);
        return RedirectToAction(nameof(Index));
    }


    // ------ PRODUCT DETAILS  ----------------
    public async Task<IActionResult> Details(int id)
    {
        Product? product = _productRepository.GetProductById(id);
        if (product == null)
        {
            return NotFound();
        }

        string tallas = string.Empty;
        foreach (ProductSize? talla in product.ProductSizes!)
        {
            tallas += $"{talla.Talla.ShortName}, ";
        }
        tallas = tallas.Substring(0, tallas.Length - 2);

        AddProductToCartViewModel model = new()
        {
            Product = product,
            ProductId = product.Id,
            ProductSizes = await _combosHelper.GetAllProductSizeByProductId(product.Id),
            Amount = 1
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Details(AddProductToCartViewModel model)
    {
        if (!User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Login", "Account");
        }

        Product? product =  _productRepository.GetProductById(model.ProductId);
        if (product == null)
        {
            return NotFound();
        }

        User? user = await _userRepository.GetUserAsync(User.Identity.Name);
        if (user == null)
        {
            return NotFound();
        }
        ProductSize? productSize = _productSizeRepository.GetProductSizeById(model.ProductSizeId);
        if (productSize == null)
        {
            return NotFound();
        }
        TemporalSale temporalSale = new()
        {
            Product = product,
            Quantity = model.Amount,
            ProductSize = productSize,
            Remarks = model.Remarks,
            User = user
        };

        _temporalSaleRepository.CreateTempalSale(temporalSale);
        return RedirectToAction(nameof(Index));
    }


	[Authorize]
	public async Task<IActionResult> ShowCart()
	{
		User? user = await _userRepository.GetUserAsync(User.Identity.Name);
		if (user == null)
		{
			return NotFound();
		}

		IEnumerable<TemporalSale> temporalSales =  _temporalSaleRepository.GetTemporalSalesByUserId(user.Id);

		ShowCartViewModel model = new()
		{
			User = user,
			TemporalSales = temporalSales,
		};

		return View(model);
	}


    //Metodos para aumentar y disminuir la cantidad de items
    public async Task<IActionResult> DecreaseQuantity(int id)
    {
        if (id == null)
        {
            return NotFound();
        }

        TemporalSale? temporalSale =  _temporalSaleRepository.GetTemporalSaleById(id);
        if (temporalSale == null)
        {
            return NotFound();
        }

        if (temporalSale.Quantity > 1)
        {
            temporalSale.Quantity--;
            _temporalSaleRepository.EditTemporalSale(temporalSale);
        }

        return RedirectToAction(nameof(ShowCart));
    }

    public async Task<IActionResult> IncreaseQuantity(int id)
    {
        if (id == null)
        {
            return NotFound();
        }

        TemporalSale? temporalSale = _temporalSaleRepository.GetTemporalSaleById(id);
        if (temporalSale == null)
        {
            return NotFound();
        }

        temporalSale.Quantity++;
        _temporalSaleRepository.EditTemporalSale(temporalSale);

        return RedirectToAction(nameof(ShowCart));
    }

    public async Task<IActionResult> Delete(int id)
    {
        TemporalSale? temporalSale = _temporalSaleRepository.GetTemporalSaleById(id);
        if (temporalSale == null)
        {
            return NotFound();
        }

        _temporalSaleRepository.DeleteTemporalSale(temporalSale);
        return RedirectToAction(nameof(ShowCart));
    }

    //Metodo editar el temporalSale o shopping cart items
    public async Task<IActionResult> Edit(int id)
    {

        TemporalSale? temporalSale =  _temporalSaleRepository.GetTemporalSaleById(id);
        if (temporalSale == null)
        {
            return NotFound();
        }

        Product? product = _productRepository.GetProductById(temporalSale.Product.Id);
        if (product == null)
        {
            return NotFound();
        }

        EditTemporalSaleViewModel model = new()
        {
            Id = temporalSale.Id,
            ProductSizes = _productSizeRepository.GetSizesByProductId(product.Id),
            Quantity = temporalSale.Quantity,
            ProductSizeId = temporalSale.ProductSize.Id,
            Remarks = temporalSale.Remarks,
          
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, EditTemporalSaleViewModel model)
    {

        if (id != model.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                TemporalSale temporalSale =  _temporalSaleRepository.GetTemporalSaleById(id); 
                
                temporalSale.Quantity = model.Quantity;
                temporalSale.ProductSize = model.ProductSize;
                temporalSale.Remarks = model.Remarks;
                _temporalSaleRepository.EditTemporalSale(temporalSale);
            }
            catch (Exception exception)
            {
                ModelState.AddModelError(string.Empty, exception.Message);
                return View(model);
            }

            return RedirectToAction(nameof(ShowCart));
        }
      
        return View(model);



    }

























    //public IActionResult AddToCart(int id)
    //{
    //    Product? product = _productRepository.GetProductById(id);

    //    if (product == null)
    //    {
    //        return NotFound();
    //    }
    //    List<SelectListItem> tallas = _productSizeRepository.GetSizesByProductId(product.Id)
    //            .OrderBy(t => t.Id)
    //                 .Select(t =>
    //                  new SelectListItem
    //                  {
    //                      Value = t.Id.ToString(),
    //                      Text = t.Talla.ShortName
    //                  }).ToList();
    //    CartItemViewModel model = new()
    //    {
    //        Product = product,
    //        ProductId = product.Id,
    //        ProductSizes = tallas,
    //        Amount = 1

    //    };
    //    return View(model);
    //}

    //[HttpPost]
    //public IActionResult AddToCart(CartItemViewModel model)
    //{
    //    Product? product = _productRepository.GetProductById(model.ProductId);
    //    if (product != null)
    //    {
    //        ProductSize? productSize = _productSizeRepository.GetProductSizeById(model.ProductSizeId);
    //        _shoppingCart.AddToCart(product, productSize, model.Amount);
    //        return RedirectToAction("Index");
    //    }
    //     return View(model);
    //}


    //public ViewResult ShowCart()
    //   {
    //       var items = _shoppingCart.GetShoppingCartItems();
    //       _shoppingCart.ShoppingCartItems = items;

    //       var shoppingCartViewModel = new ShoppingCartViewModel(_shoppingCart, _shoppingCart.GetShoppingCartTotal());

    //       return View(shoppingCartViewModel);
    //   }





























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
