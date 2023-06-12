using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemasWeb01.Helpers;
using SistemasWeb01.Models;
using SistemasWeb01.Repository.Implementations;
using SistemasWeb01.ViewModels;
using static System.Collections.Specialized.BitVector32;

namespace SistemasWeb01.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IFormFileHelper _formFileHelper;
        public CategoryController(ICategoryRepository categoryRepository, IFormFileHelper formFileHelper)
        {
            _categoryRepository = categoryRepository;
            _formFileHelper = formFileHelper;
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
    }
}
