using Microsoft.AspNetCore.Mvc;
using SistemasWeb01.Helpers;
using SistemasWeb01.Models;
using SistemasWeb01.Repository.Interfaces;

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
    }
}
