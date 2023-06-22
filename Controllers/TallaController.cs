using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemasWeb01.Models;
using SistemasWeb01.Repository.Interfaces;
using System.Data;

namespace SistemasWeb01.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class TallaController : Controller
    {
        private readonly ITallaRepository _tallaRepository;
        public TallaController(ITallaRepository tallaRepository)
        {
            _tallaRepository = tallaRepository;
        }
        public IActionResult Index()
        {
            IEnumerable<Talla> tallas = _tallaRepository.AllTallas;
            return View(tallas);
        }

        public IActionResult Create()
        {
            Talla talla = new Talla();
            return View(talla);
        }

        [HttpPost]
        public IActionResult Create(Talla talla)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _tallaRepository.CreateTalla(talla);
                    TempData["mensaje"] = "La talla se creó correctamente";
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("UNIQUE constraint failed: Tallas.ShortName"))
                    {
                        ModelState.AddModelError(string.Empty, "YYa existe una talla con la misma abreviación.");
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
            return View(talla);
        }
        public IActionResult Details(int id)
        {
            Talla? talla = _tallaRepository.GetTallaById(id);
            if (talla == null)
            {
                return NotFound();
            }
            return View(talla);

        }

        public IActionResult Edit(int id)
        {
            Talla? talla = _tallaRepository.GetTallaById(id);
            if (talla == null)
            {
                return NotFound();
            }
            return View(talla);
        }

        [HttpPost]
        public IActionResult Edit(Talla talla)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _tallaRepository.EditTalla(talla);
                    TempData["mensaje"] = "La talla se actualizó correctamente";
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("UNIQUE constraint failed: Tallas.ShortName"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe una talla con la misma abreviación.");
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
            return View(talla);
        }

        public IActionResult Delete(int id)
        {
            Talla? talla = _tallaRepository.GetTallaById(id);
            if (talla == null)
            {
                return NotFound();
            }
            return View(talla);
        }

        [HttpPost]
        public IActionResult Delete(Talla talla)
        {
            if (talla == null) return NotFound();

            _tallaRepository.DeleteTalla(talla);
            TempData["mensaje"] = "La talla se eliminó correctamente";
            return RedirectToAction("Index");
        }
    }
}
