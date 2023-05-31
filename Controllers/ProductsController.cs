using Microsoft.AspNetCore.Mvc;
using SistemasWeb01.Models;
using SistemasWeb01.Repository.IRepository;
using SistemasWeb01.ViewModels;

namespace SistemasWeb01.Controllers
{
    public class ProductsController : Controller
    {
        private IProductRepository _productRepository;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IProductImageRepository _productImageRepository;

        public ProductsController(IProductRepository productRepository,ICategoriaRepository categoriaRepository, IProductImageRepository productImageRepository)
        {
            _productRepository = productRepository;
            _categoriaRepository = categoriaRepository;
            _productImageRepository = productImageRepository;
        }
        public IActionResult Index()
        {
            ProductViewModel productoViewModel = new ProductViewModel(_productRepository.AllProducts);
            return View(productoViewModel);
        }
    }
}
