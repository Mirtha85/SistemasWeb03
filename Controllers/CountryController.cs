using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemasWeb01.Models;
using SistemasWeb01.Repository.Implementations;
using SistemasWeb01.Repository.Interfaces;
using System.Diagnostics.Metrics;
using static System.Collections.Specialized.BitVector32;

namespace SistemasWeb01.Controllers
{
    public class CountryController : Controller
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IStateRepository _stateRepository;
        public CountryController(ICountryRepository countryRepository, IStateRepository stateRepository)
        {
            _countryRepository = countryRepository;
            _stateRepository = stateRepository;
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


        //2do Nivel Create
        public IActionResult AddState(int id)
        {

            Country? country = _countryRepository.GetCountryById(id);
            if (country == null)
            {
                return NotFound();
            }
            State state = new()
            {
                CountryId = id,
            };


            return View(state);
        }


        [HttpPost]
        public IActionResult AddState(State state)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    state = new()
                    {
                        Cities = new List<City>(),
                        Country = _countryRepository.GetCountryById(state.CountryId),
                        Name = state.Name,
                    };
                    _stateRepository.CreateState(state);

                    TempData["mensaje"] = "El departamento se agregó correctamente";
                    return RedirectToAction(nameof(Details), new { Id = state.CountryId });
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException!.Message.Contains("UNIQUE constraint failed: States.Name"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe un departamento con el mismo nombre.");
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
            return View(state);
        }

        public IActionResult EditState(int id)
        {
            State? state = _stateRepository.GetStateById(id);
            if (state == null)
            {
                return NotFound();
            }

            return View(state);
        }

        [HttpPost]
        public IActionResult EditState(int id, State state)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _stateRepository.EditState(state);
                    TempData["mensaje"] = "El departamento se actualizó correctamente";
                    return RedirectToAction(nameof(Details), new { Id = state.CountryId });
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException!.Message.Contains("UNIQUE constraint failed: States.Name"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe un departamento con el mismo nombre.");
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
            return View(state);
        }

        public IActionResult DetailsState(int id)
        {
            State? state = _stateRepository.GetStateById(id);
            if (state == null)
            {
                return NotFound();
            }
            return View(state);
        }

        public IActionResult DeleteState(int id)
        {
            State? state = _stateRepository.GetStateById(id);
            if (state == null)
            {
                return NotFound();
            }
            return View(state);
        }

        [HttpPost]
        public IActionResult DeleteState(State state)
        {
            _stateRepository.DeleteState(state);
            TempData["mensaje"] = "El país se eliminó correctamente";
            return RedirectToAction(nameof(Details), new { Id = state.Country!.Id });
        }


       
    }
}
