using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemasWeb01.Helpers;
using SistemasWeb01.Models;
using SistemasWeb01.Repository.Interfaces;
using SistemasWeb01.ViewModels;
using System.Data;
using System.Security.Policy;

namespace SistemasWeb01.Controllers
{
    [Authorize(Roles = "Admin")]
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
            CreateProductViewModel createProductViewModel = new()
            {
                Categories =  _categoryRepository.AllCategories,
                Brands = _brandRepository.AllBrands,
                Tallas = _tallaRepository.AllTallas,
            };

            return View(createProductViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductViewModel createProductViewModel)
        {
            if (ModelState.IsValid)
            {
                string imageName = string.Empty;
                if (createProductViewModel.ImageFile != null)
                {
                    imageName =  await _formFileHelper.UploadFile(createProductViewModel.ImageFile);
                }

                Product product = new()
                {
                    Description = createProductViewModel.Description,
                    Name = createProductViewModel.Name,
                    Price = createProductViewModel.Price,
                    InStock = createProductViewModel.InStock,
                    IsNew = createProductViewModel.IsNew,
                    IsBestSeller = createProductViewModel.IsBestSeller,
                    PercentageDiscount = createProductViewModel.PercentageDiscount,
                    SubCategoryId = createProductViewModel.SubCategoryId,
                    BrandId = createProductViewModel.BrandId,
                };

                product.ProductSizes = new List<ProductSize>()
                {
                    new ProductSize
                    {
                        Talla =  _tallaRepository.GetTallaById(createProductViewModel.TallaId)
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
                    TempData["mensaje"] = "El producto se creó correctamente";
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

            createProductViewModel.Categories = _categoryRepository.AllCategories;
            return View(createProductViewModel);
        }

        [HttpGet]
        public IActionResult GetSubCategoriesByCategory(int categoryId)
        {

            IEnumerable<SubCategory> subCategories = _subCategoryRepository.GetSubCategoriesByCategoryId(categoryId);

            return Ok(subCategories);
        }


        public IActionResult Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product? product = _productRepository.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            EditProductViewModel editProductViewModel = new()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                InStock = product.InStock,
                IsDeleted = product.IsDeleted,
                IsNew = product.IsNew,
                IsBestSeller = product.IsBestSeller,
                PercentageDiscount = product.PercentageDiscount,
                SubCategoryId = product.SubCategoryId,
                BrandId = product.BrandId,
                CategoryId = product.SubCategory.CategoryId,
                Categories = _categoryRepository.AllCategories,
                SubCategories = _subCategoryRepository.AllSubCategories,
                Brands = _brandRepository.AllBrands
            };

            return View(editProductViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditProductViewModel editProductViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Product product = new()
                    {
                        Id = editProductViewModel.Id,
                        Name = editProductViewModel.Name,
                        Description = editProductViewModel.Description,
                        Price = editProductViewModel.Price,
                        InStock = editProductViewModel.InStock,
                        IsDeleted = editProductViewModel.IsDeleted,
                        IsNew = editProductViewModel.IsNew,
                        IsBestSeller = editProductViewModel.IsBestSeller,
                        PercentageDiscount = editProductViewModel.PercentageDiscount,
                        SubCategoryId = editProductViewModel.SubCategoryId,
                        BrandId = editProductViewModel.BrandId,
                    };
                    _productRepository.EditProduct(product);
                    TempData["mensaje"] = "El producto se actualizó correctamente";
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
            return View(editProductViewModel);
        }


        public async Task<IActionResult> Details(int id)
        {
            Product? product = _productRepository.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        public IActionResult Delete(int id)
        {
            Product? product = _productRepository.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            
            return View(product);
        }
        [HttpPost]
        public IActionResult Delete(Product model)
        {
            Product? product = _productRepository.GetProductById(model.Id);
            _productRepository.DeleteProduct(product);
            foreach (Picture picture in product.Pictures)
            {
                _formFileHelper.DeleteFile(picture.PictureName);
            }
            return RedirectToAction(nameof(Index));
        }

        // -------------------------------------------------------
        public IActionResult AddImage(int id)
        {
            Product? product =  _productRepository.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            ProductImageViewModel model = new()
            {
                ProductId = product.Id,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddImage(ProductImageViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.PictureName =  await _formFileHelper.UploadFile(model.ImageFile);

                Product? product =  _productRepository.GetProductById(model.ProductId);
                if (product == null)
                {
                    return NotFound();
                }
                Picture picture = new()
                {
                    Product = product,
                    PictureName = model.PictureName,
                };
                try
                {
                    _pictureRepository.CreatePicture(picture);
                    return RedirectToAction(nameof(Details), new { Id = product.Id });
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }

            return View(model);
        }

        public IActionResult EditImage(int id)
        {
            Picture? picture = _pictureRepository.GetPictureById(id);
            if (picture == null)
            {
                return NotFound();
            }
            ProductImageViewModel pictureViewModel = new()
            {
                Id = picture.Id,
                PictureName = picture.PictureName,
                ProductId = picture.ProductId,
            };
            return View(pictureViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditImage(ProductImageViewModel pictureViewModel)
        {
            if (ModelState.IsValid)
            {
                if (pictureViewModel.PictureName != null)
                {
                    _formFileHelper.DeleteFile(pictureViewModel.PictureName);
                }
                pictureViewModel.PictureName = await _formFileHelper.UploadFile(pictureViewModel.ImageFile);
                _pictureRepository.EditPicture(pictureViewModel);
                return RedirectToAction(nameof(Details), new { Id = pictureViewModel.ProductId });
            }
            return View(pictureViewModel);
        }

        public IActionResult DeleteImage(int id)
        {

            Picture? picture = _pictureRepository.GetPictureById(id);
            if (picture == null)
            {
                return NotFound();
            }
            return View(picture);
        }

        [HttpPost]
        public IActionResult DeleteImage(Picture picture)
        {
            if (picture == null)
            {
                return NotFound();
            }

            _formFileHelper.DeleteFile(picture.PictureName);
            _pictureRepository.DeletePicture(picture);
            return RedirectToAction(nameof(Details), new { Id = picture.ProductId });
        }


        public IActionResult AddTalla(int id)
        {
            Product? product = _productRepository.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            List<Talla> currentTallas = product.ProductSizes.Select(ps => new Talla
            {
                Id = ps.Talla.Id,
                Name = ps.Talla.Name,
                ShortName = ps.Talla.ShortName,
                SizeNumber = ps.Talla.SizeNumber
            }).ToList();

            //
            ProductSizeViewModel model = new()
            {
                ProductId = product.Id,
                Tallas = _tallaRepository.TallasFiltered(currentTallas),
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult AddTalla(ProductSizeViewModel model)
        {
            if (ModelState.IsValid)
            {
                Product? product = _productRepository.GetProductById(model.ProductId);
                ProductSize productsize = new()
                {
                    Talla = _tallaRepository.GetTallaById(model.TallaId),
                    Product = product,
                    Quantity = model.Quantity
                };
                try
                {
                    _productSizeRepository.CreateProductSize(productsize);
                    return RedirectToAction(nameof(Details), new { Id = product.Id });
                }
                ////validation for duplicate ProductId and TallaId
                //catch (DbUpdateException dbUpdateException)
                //{
                //    if (dbUpdateException.InnerException.Message.Contains("UNIQUE constraint failed: ProductSizes.ProductId, ProductSizes.TallaId"))
                //    {
                //        ModelState.AddModelError(string.Empty, "El producto ya tiene esa talla.");
                //    }
                //    else
                //    {
                //        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                //    }
                //}
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }

            return View(model);
        }

        public IActionResult EditTalla(int id)
        {
            ProductSize? productSize = _productSizeRepository.GetProductSizeById(id);
            if (productSize == null)
            {
                return NotFound();
            }
            ProductSizeViewModel productSizeViewModel = new()
            {
                Id = productSize.Id,
                ProductId = productSize.ProductId,
                Quantity = productSize.Quantity,
                TallaId = productSize.TallaId,
                Tallas = _tallaRepository.AllTallas
            };
            return View(productSizeViewModel);
        }

        [HttpPost]
        public IActionResult EditTalla(ProductSizeViewModel productSizeViewModel)
        {
            if (ModelState.IsValid)
            {
                ProductSize productSize = new ProductSize()
                {
                    Id = productSizeViewModel.Id,
                    ProductId = productSizeViewModel.ProductId,
                    TallaId = productSizeViewModel.TallaId,
                    Quantity = productSizeViewModel.Quantity
                };
                _productSizeRepository.EditProductSize(productSize);
                return RedirectToAction(nameof(Details), new { Id = productSizeViewModel.ProductId });
            }
            return View(productSizeViewModel);
        }

        public IActionResult DeleteTalla(int id) 
        {
            ProductSize? productSize = _productSizeRepository.GetProductSizeById(id);
            if (productSize == null)
            {
                return NotFound();
            }
            return View(productSize);
        }

        [HttpPost]
        public IActionResult DeleteTalla(ProductSize productSize) 
        {
            _productSizeRepository.DeleteProductSize(productSize);
            return RedirectToAction(nameof(Details), new { Id = productSize.ProductId });
        }

    }
}
