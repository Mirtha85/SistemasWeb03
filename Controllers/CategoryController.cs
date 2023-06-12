using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemasWeb01.Helpers;
using SistemasWeb01.Models;
using SistemasWeb01.Repository.Implementations;
using SistemasWeb01.Repository.Interfaces;
using SistemasWeb01.ViewModels;
using static System.Collections.Specialized.BitVector32;

namespace SistemasWeb01.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IFormFileHelper _formFileHelper;
        private readonly ISubCategoryRepository _subCategoryRepository;
        public CategoryController(ICategoryRepository categoryRepository, IFormFileHelper formFileHelper, ISubCategoryRepository subCategoryRepository)
        {
            _categoryRepository = categoryRepository;
            _formFileHelper = formFileHelper;
            _subCategoryRepository = subCategoryRepository;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> categories = _categoryRepository.AllCategories;
           return View(categories);
        }
        public IActionResult Create()
        {
            CategoryViewModel categoryViewModel = new CategoryViewModel();
            return View(categoryViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryViewModel categoryViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (categoryViewModel.formFile != null)
                    {
                        categoryViewModel.ThumbnailImage = await _formFileHelper.UploadFile(categoryViewModel.formFile);
                    }
                    _categoryRepository.CreateCategory(categoryViewModel);
                    TempData["mensaje"] = "La categoría se creó correctamente";
                    return RedirectToAction("Index");
                }
                //validation for duplicate names
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("UNIQUE constraint failed: Categories.Name"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe una categoría con el mismo nombre.");
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
            return View(categoryViewModel);
        }

        public IActionResult Details(int id)
        {
            Category? category = _categoryRepository.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
            
        }

        public IActionResult Edit(int id)
        {
            Category? category = _categoryRepository.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            CategoryViewModel categoryViewModel = new()
            {
                Id = category.Id,
                Name = category.Name,
                ThumbnailImage = category.ThumbnailImage

            };
            return View(categoryViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryViewModel categoryViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (categoryViewModel.formFile != null)
                    {
                        if (categoryViewModel.ThumbnailImage != null)
                        {
                            _formFileHelper.DeleteFile(categoryViewModel.ThumbnailImage);
                        }
                        categoryViewModel.ThumbnailImage = await _formFileHelper.UploadFile(categoryViewModel.formFile);
                    }
                    _categoryRepository.EditCategory(categoryViewModel);
                    TempData["mensaje"] = "La categoría se actualizó correctamente";
                    return RedirectToAction("Index");
                }

                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("UNIQUE constraint failed: Categories.Name"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe una categoría con el mismo nombre.");
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
            return View(categoryViewModel);
        }

        public IActionResult Delete(int id)
        {
            Category? category = _categoryRepository.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        public IActionResult Delete(Category category)
        {
            if (category == null) return NotFound();
            _formFileHelper.DeleteFile(category.ThumbnailImage);

            _categoryRepository.DeleteCategory(category);
            TempData["mensaje"] = "La categoría se eliminó correctamente";
            return RedirectToAction("Index");
        }

        // *************************** Crud de 2do. Nivel ********************************************

        public IActionResult AddSubCategory(int id)
        {

            Category? category = _categoryRepository.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            SubCategory subcategory = new()
            {
                CategoryId = id,
            };


            return View(subcategory);
        }

        [HttpPost]
        public IActionResult AddSubCategory(SubCategory subcategory)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    subcategory = new()
                    {
                        //Products = new List<Product>(),
                        Category = _categoryRepository.GetCategoryById(subcategory.CategoryId),
                        Name = subcategory.Name,
                    };
                    _subCategoryRepository.CreateSubCategory(subcategory);

                    TempData["mensaje"] = "La subcategoría se agregó correctamente";
                    return RedirectToAction(nameof(Details), new { Id = subcategory.CategoryId });
                }
                //validation for duplicate names
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("UNIQUE constraint failed: SubCategories.Name"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe una subcategoría con el mismo nombre.");
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
            return View(subcategory);
        }

        public IActionResult EditSubCategory(int id)
        {
            SubCategory? subcategory = _subCategoryRepository.GetSubCategoryById(id);
            if (subcategory == null)
            {
                return NotFound();
            }

            return View(subcategory);
        }

        [HttpPost]
        public IActionResult EditSubCategory(int id, SubCategory subcategory)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _subCategoryRepository.EditSubCategory(subcategory);
                    TempData["mensaje"] = "La subcategoría se actualizó correctamente";
                    return RedirectToAction(nameof(Details), new { Id = subcategory.CategoryId });
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("UNIQUE constraint failed: SubCategories.Name"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe una subcategoría con el mismo nombre en esta sección.");
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
            return View(subcategory);
        }

        public IActionResult DetailsSubCategory(int id)
        {
            SubCategory? subcategory = _subCategoryRepository.GetSubCategoryById(id);
            if (subcategory == null)
            {
                return NotFound();
            }
            return View(subcategory);
        }
        public IActionResult DeleteSubCategory(int id)
        {
            SubCategory? subcategory = _subCategoryRepository.GetSubCategoryById(id);
            if (subcategory == null)
            {
                return NotFound();
            }
            return View(subcategory);
        }
        [HttpPost, ActionName("DeleteSubCategory")]
        public IActionResult DeleteSubCategoryConfirm(int id)
        {
            SubCategory? subcategory = _subCategoryRepository.GetSubCategoryById(id);
            _subCategoryRepository.DeleteSubCategory(subcategory);
            TempData["mensaje"] = "La subcateogría se eliminó correctamente";
            return RedirectToAction(nameof(Details), new { Id = subcategory.Category.Id });
        }
    }
}
