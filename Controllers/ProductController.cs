using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemasWeb01.Helpers;
using SistemasWeb01.Models;
using SistemasWeb01.Repository.Interfaces;
using SistemasWeb01.ViewModels;
using System.Security.Policy;

namespace SistemasWeb01.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISubCategoryRepository _subCategoryRepository;
        private readonly ITallaRepository _tallaRepository;
        private readonly IProductSizeRepository _productSizeRepository;
        private readonly IPictureRepository _pictureRepository;
        private readonly IFormFileHelper _formFileHelper;
        private readonly IBrandRepository _brandRepository;
        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository, ISubCategoryRepository subCategoryRepository, ITallaRepository tallaRepository, IProductSizeRepository productSizeRepository, IPictureRepository pictureRepository, IFormFileHelper formFileHelper, IBrandRepository brandRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _subCategoryRepository = subCategoryRepository;
            _tallaRepository = tallaRepository;
            _productSizeRepository = productSizeRepository;
            _pictureRepository = pictureRepository;
            _formFileHelper = formFileHelper;
            _brandRepository = brandRepository;
            
        }
        public IActionResult Index()
        {
            IEnumerable<Product> products = _productRepository.AllProducts;
            return View(products);
        }

        public IActionResult Create()
        {
            CreateProductViewModel model = new()
            {
                Categories =  _categoryRepository.AllCategories,
                Brands = _brandRepository.AllBrands,
                Tallas = _tallaRepository.AllTallas,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                string imageName = string.Empty;
                if (model.ImageFile != null)
                {
                    imageName =  await _formFileHelper.UploadFile(model.ImageFile);
                }

                Product product = new()
                {
                    Description = model.Description,
                    Name = model.Name,
                    Price = model.Price,
                    InStock = model.InStock,
                    IsNew = model.IsNew,
                    IsBestSeller = model.IsBestSeller,
                    PercentageDiscount = model.PercentageDiscount,
                    SubCategoryId = model.SubCategoryId,
                    BrandId = model.BrandId,
                };

                product.ProductSizes = new List<ProductSize>()
                {
                    new ProductSize
                    {
                        Talla =  _tallaRepository.GetTallaById(model.TallaId)
                    }
                };

                if (imageName != string.Empty)
                {
                    product.Pictures = new List<Picture>()
                    {
                        new Picture { PictureName = imageName }
                    };
                }

                try
                {
                    _productRepository.CreateProduct(product);
                    return RedirectToAction(nameof(Index));
                }
                //validation for duplicate names
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("UNIQUE constraint failed: Products.Name"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe una producto con el mismo nombre.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }

            model.Categories = _categoryRepository.AllCategories;
            return View(model);
        }

        [HttpGet]
        public IActionResult GetSubCategoriesByCategory(int categoryId)
        {

            IEnumerable<SubCategory> subCategories = _subCategoryRepository.GetSubCategoriesByCategoryId(categoryId);

            return Ok(subCategories);
        }

    }
}
