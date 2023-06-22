using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemasWeb01.Helpers;
using SistemasWeb01.Models;
using SistemasWeb01.Repository.Implementations;
using SistemasWeb01.Repository.Interfaces;
using SistemasWeb01.ViewModels;
using System.Data;

namespace SistemasWeb01.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class BrandController : Controller
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IFormFileHelper _formFileHelper;
        public BrandController(IBrandRepository brandRepository, IFormFileHelper formFileHelper)
        {
            _brandRepository = brandRepository;
            _formFileHelper = formFileHelper;
        }

        public IActionResult Index()
        {
            IEnumerable<Brand> brands = _brandRepository.AllBrands;
            return View(brands);
        }
        public IActionResult Create()
        {
            BrandViewModel brandViewModel = new BrandViewModel();
            return View(brandViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Create(BrandViewModel brandViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (brandViewModel.formFile != null)
                    {
                        brandViewModel.ThumbnailImage = await _formFileHelper.UploadFile(brandViewModel.formFile);
                    }
                    _brandRepository.CreateBrand(brandViewModel);
                    TempData["mensaje"] = "La marca se creó correctamente";
                    return RedirectToAction("Index");
                }
                //validation for duplicate names
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("UNIQUE constraint failed: Brands.Name"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe una marca con el mismo nombre.");
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
            return View(brandViewModel);
        }

        public IActionResult Details(int id)
        {
            Brand? brand = _brandRepository.GetBrandById(id);
            if (brand == null)
            {
                return NotFound();
            }
            return View(brand);

        }

        public IActionResult Edit(int id)
        {
            Brand? brand = _brandRepository.GetBrandById(id);
            if (brand == null)
            {
                return NotFound();
            }
            BrandViewModel brandViewModel = new()
            {
                Id = brand.Id,
                Name = brand.Name,
                ThumbnailImage = brand.ThumbnailImage

            };
            return View(brandViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BrandViewModel brandViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (brandViewModel.formFile != null)
                    {
                        if (brandViewModel.ThumbnailImage != null)
                        {
                            _formFileHelper.DeleteFile(brandViewModel.ThumbnailImage);
                        }
                        brandViewModel.ThumbnailImage = await _formFileHelper.UploadFile(brandViewModel.formFile);
                    }
                    _brandRepository.EditBrand(brandViewModel);
                    TempData["mensaje"] = "La marca se actualizó correctamente";
                    return RedirectToAction("Index");
                }

                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("UNIQUE constraint failed: Brands.Name"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe una marca con el mismo nombre.");
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
            return View(brandViewModel);
        }

        public IActionResult Delete(int id)
        {
            Brand? brand = _brandRepository.GetBrandById(id);
            if (brand == null)
            {
                return NotFound();
            }
            return View(brand);
        }

        [HttpPost]
        public IActionResult Delete(Brand brand)
        {
            if (brand == null) return NotFound();
            _formFileHelper.DeleteFile(brand.ThumbnailImage);

            _brandRepository.DeleteBrand(brand);
            TempData["mensaje"] = "La marca se eliminó correctamente";
            return RedirectToAction("Index");
        }
    }
}
