using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemasWeb01.Models;
using SistemasWeb01.Repository.Interfaces;
using static System.Collections.Specialized.BitVector32;

namespace SistemasWeb01.Controllers
{
    public class CountryController : Controller
    {
        private readonly ICountryRepository _countryRepository;
        public CountryController(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }
        public IActionResult Index()
        {
            IEnumerable<Country> countries = _countryRepository.AllCountries;
            return View(countries);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(Country country)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _countryRepository.CreateCountry(country);
                    TempData["mensaje"] = "El país se creó correctamente";
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException!.Message.Contains("UNIQUE constraint failed: Countries.Name"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe un país con el mismo nombre.");
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
            return View(country);
        }

        public IActionResult Details(int id)
        {
            Country? country = _countryRepository.GetCountryById(id);
            if (country == null)
            {
                return NotFound();
            }
            return View(country);
        }

        public IActionResult Edit(int id)
        {
            Country? country = _countryRepository.GetCountryById(id);
            if (country == null)
            {
                return NotFound();
            }
            return View(country);
        }

        [HttpPost]
        public IActionResult Edit(Country country)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _countryRepository.EditCountry(country);
                    TempData["mensaje"] = "El País se actualizó correctamente";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException!.Message.Contains("UNIQUE constraint failed: Countries.Name"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe un país con el mismo nombre.");
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
            return View(country);
        }

        public IActionResult Delete(int id)
        {
            Country? country = _countryRepository.GetCountryById(id);
            if (country == null)
            {
                return NotFound();
            }
            return View(country);
        }

        [HttpPost]
        public IActionResult Delete(Country country)
        {
            _countryRepository.DeleteCountry(country);
            TempData["mensaje"] = "El país se eliminó correctamente";
            return RedirectToAction("Index");
        }
    }
}
