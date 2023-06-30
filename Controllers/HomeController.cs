using System.Diagnostics;
using System.IO.Pipelines;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using SistemasWeb01.DataAccess;
using SistemasWeb01.Enums;
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
    private readonly ITemporalCartItemRepository _temporalCartItemRepository;
    private readonly IOrderRepository _orderRepository;

    public HomeController(IUserRepository userRepository, ILogger<HomeController> logger, IProductRepository productRepository, ICategoryRepository categoryRepository, ISubCategoryRepository subCategoryRepository, IProductSizeRepository productSizeRepository, IShoppingCart shoppingCart, ITemporalSaleRepository temporalSaleRepository, ICombosHelper combosHelper, ITemporalCartItemRepository temporalCartItemRepository, IOrderRepository orderRepository)
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
        _temporalCartItemRepository = temporalCartItemRepository;
        _orderRepository = orderRepository;
    }


    public async Task<IActionResult> Index()
    {
        IEnumerable<Product> products = _productRepository.BestSellingProducts;
        IEnumerable<Category> categories = _categoryRepository.AllCategories;
        HomeViewModel model = new HomeViewModel(products, categories);
        User user = await _userRepository.GetUserAsync(User.Identity.Name);
        if (user != null)
        {
            model.Quantity = _temporalCartItemRepository.GetTemporalCartItemsByUserId(user.Id).Count();
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

        Product? product = _productRepository.GetProductById(id);
        if (product == null)
        {
            return NotFound();
        }

        User user = await _userRepository.GetUserAsync(User.Identity.Name); //siempre devuelve el correo del usuario logeado
        if (user == null)
        {
            return NotFound();
        }
        ProductSize sizeDefault = new() { ProductId = product.Id, TallaId = 6, Quantity = 1 };
        TemporalCartItem temporalCartItem = new()
        {
            Product = product,
            Quantity = 1,
            User = user,
            ProductSize = sizeDefault
        };

        _temporalCartItemRepository.CreateTemporalCartItem(temporalCartItem);
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
    public async Task<IActionResult> Details(AddProductToCartViewModel model)
    {
        if (!User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Login", "Account");
        }

        Product? product = _productRepository.GetProductById(model.ProductId);
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
        TemporalCartItem temporalCartItem = new()
        {
            Product = product,
            Quantity = model.Amount,
            ProductSize = productSize,
            Remarks = model.Remarks,
            User = user
        };

        _temporalCartItemRepository.CreateTemporalCartItem(temporalCartItem);
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

		IEnumerable<TemporalCartItem> temporalCartItems =  _temporalCartItemRepository.GetTemporalCartItemsByUserId(user.Id);

		ShowCartViewModel model = new()
		{
			User = user,
			TemporalCartItems = temporalCartItems,
		};

		return View(model);
	}

    //ShowCartPost
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ShowCart(ShowCartViewModel model)
    {
        User? user = await _userRepository.GetUserAsync(User.Identity.Name);
        if (user == null)
        {
            return NotFound();
        }

        model.User = user;
        model.TemporalCartItems =  _temporalCartItemRepository.GetTemporalCartItemsByUserId(user.Id);

        bool response = ProcessOrderAsync(model);
        if (response == true)
        {
            return RedirectToAction(nameof(OrderSuccess));
        }

        TempData["mensaje"] = "Lo sentimos su pedido no pudo completarse por falta de inventario";
        return View(model);
    }





    //Metodos para aumentar y disminuir la cantidad de items
    public async Task<IActionResult> DecreaseQuantity(int id)
    {
        if (id == null)
        {
            return NotFound();
        }

        TemporalCartItem? temporalCartItem =  _temporalCartItemRepository.GetTemporalCartItemById(id);
        if (temporalCartItem == null)
        {
            return NotFound();
        }

        if (temporalCartItem.Quantity > 1)
        {
            temporalCartItem.Quantity--;
            _temporalCartItemRepository.EditTemporalCartItem(temporalCartItem);
        }

        return RedirectToAction(nameof(ShowCart));
    }

    public async Task<IActionResult> IncreaseQuantity(int id)
    {
        if (id == null)
        {
            return NotFound();
        }

        TemporalCartItem? temporalCartItem = _temporalCartItemRepository.GetTemporalCartItemById(id);
        if (temporalCartItem == null)
        {
            return NotFound();
        }

        temporalCartItem.Quantity++;
        _temporalCartItemRepository.EditTemporalCartItem(temporalCartItem);

        return RedirectToAction(nameof(ShowCart));
    }

    public async Task<IActionResult> Delete(int id)
    {
        TemporalCartItem? temporalCartItem = _temporalCartItemRepository.GetTemporalCartItemById(id);
        if (temporalCartItem == null)
        {
            return NotFound();
        }

        _temporalCartItemRepository.DeleteTemporalCartItem(temporalCartItem);
        return RedirectToAction(nameof(ShowCart));
    }









    //Metodo editar el temporalSale o shopping cart items
    //public async Task<IActionResult> Edit(int id)
    //{

    //    TemporalSale? temporalSale =  _temporalSaleRepository.GetTemporalSaleById(id);
    //    if (temporalSale == null)
    //    {
    //        return NotFound();
    //    }

    //    Product? product = _productRepository.GetProductById(temporalSale.Product.Id);
    //    if (product == null)
    //    {
    //        return NotFound();
    //    }

    //    EditTemporalSaleViewModel model = new()
    //    {
    //        Id = temporalSale.Id,
    //        ProductSizes = _productSizeRepository.GetSizesByProductId(product.Id),
    //        Quantity = temporalSale.Quantity,
    //        ProductSizeId = temporalSale.ProductSize.Id,
    //        Remarks = temporalSale.Remarks,
          
    //    };

    //    return View(model);
    //}

    //[HttpPost]
    //public async Task<IActionResult> Edit(int id, EditTemporalSaleViewModel model)
    //{

    //    if (id != model.Id)
    //    {
    //        return NotFound();
    //    }

    //    if (ModelState.IsValid)
    //    {
    //        try
    //        {
    //            TemporalSale temporalSale =  _temporalSaleRepository.GetTemporalSaleById(id); 
                
    //            temporalSale.Quantity = model.Quantity;
    //            temporalSale.ProductSize = model.ProductSize;
    //            temporalSale.Remarks = model.Remarks;
    //            _temporalSaleRepository.EditTemporalSale(temporalSale);
    //        }
    //        catch (Exception exception)
    //        {
    //            ModelState.AddModelError(string.Empty, exception.Message);
    //            return View(model);
    //        }

    //        return RedirectToAction(nameof(ShowCart));
    //    }
      
    //    return View(model);



    //}


    public async Task<IActionResult> Edit(int id)
    {

        TemporalCartItem? temporalCartItem = _temporalCartItemRepository.GetTemporalCartItemById(id);
        if (temporalCartItem == null)
        {
            return NotFound();
        }

        Product product = _productRepository.GetProductById(temporalCartItem.ProductSizeId);
        if (product == null)
        {
            return NotFound();
        }

        EditTemporalSaleViewModel model = new EditTemporalSaleViewModel();

        model.Id = temporalCartItem.Id;
        model.ProductId = product.Id;
        model.Product = product;
        model.ProductSizes = await _combosHelper.GetAllProductSizeByProductId(product.Id);
        model.Quantity = temporalCartItem.Quantity;
        model.ProductSizeId = temporalCartItem.ProductSizeId;
        model.Remarks = temporalCartItem.Remarks;

      

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
                TemporalCartItem? temporalCartItem = _temporalCartItemRepository.GetTemporalCartItemById(id);

                temporalCartItem.Quantity = model.Quantity;
                temporalCartItem.ProductSizeId = model.ProductSizeId;
                temporalCartItem.Remarks = model.Remarks;
                _temporalCartItemRepository.EditTemporalCartItem(temporalCartItem);
                return RedirectToAction(nameof(ShowCart));
            }
            catch (Exception exception)
            {
                ModelState.AddModelError(string.Empty, exception.Message);
                return View(model);
            }
        }

        return View(model);
    }




    [Authorize]
    public IActionResult OrderSuccess()
    {
        return View();
    }


    /// <summary>
    /// PROCESAR ORDER

    public bool ProcessOrderAsync(ShowCartViewModel model)
    {
        bool response =  CheckInventoryAsync(model);
        if (response == false)
        {
            TempData["mensaje"] = "Lo sentimos su pedido no pudo completarse por falta de inventario";
        }

        Order order = new()
        {
            Date = DateTime.UtcNow,
            User = model.User,
            Remarks = model.Remarks,
            OderDetails = new List<OrderDetail>(),
            OrderStatus = OrderStatus.Nuevo
        };

        foreach (TemporalCartItem item in model.TemporalCartItems)
        {
            order.OderDetails.Add(new OrderDetail
            {
                Product = item.Product,
                Quantity = item.Quantity,
                Remarks = item.Remarks,
            });

            Product? product = _productRepository.GetProductById(item.Product.Id);
            if (product != null)
            {
                product.InStock -= item.Quantity;
                _productRepository.EditProduct(product);
            }
            _temporalCartItemRepository.DeleteTemporalCartItem(item);
        }

        _orderRepository.CreateOrder(order);
        return response;
    }

    private bool CheckInventoryAsync(ShowCartViewModel model)
    {
        bool response = true;
        foreach (TemporalCartItem item in model.TemporalCartItems)
        {
            Product? product =  _productRepository.GetProductById(item.Product.Id);
            if (product == null)
            {
                response = false;
                TempData["mensaje"] = "El producto ya no está disponible";
                return response;
            }
            if (product.InStock < item.Quantity)
            {
                response = false;
                TempData["mensaje"] = $"Lo sentimos no tenemos existencias suficiente del producto {item.Product.Name}, para tomar su pedido. Por favor disminuir la cantidad o cambiarlo por otro";
                return response;
            }
        }
        return response;
    }




    public IActionResult Roulette()
    {
        List<User> users = new List<User>();
        users = _userRepository.AllUsers.ToList();
        
        return View(users);
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
