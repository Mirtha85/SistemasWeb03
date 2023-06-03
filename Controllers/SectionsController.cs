using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemasWeb01.Models;
using SistemasWeb01.Repository.IRepository;

namespace SistemasWeb01.Controllers
{
    public class SectionsController : Controller
    {
        private readonly ISectionRepository _sectionRepository;

        public SectionsController(ISectionRepository sectionRepository)
        {
            _sectionRepository = sectionRepository;
        }

        public IActionResult Index()
        {
            IEnumerable<Section> sections = _sectionRepository.AllSections;
            return View(sections);
        }



        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(Section section)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    _sectionRepository.CreateSection(section);
                    TempData["mensaje"] = "La sección se creó correctamente";
                    return RedirectToAction("Index");
                }
                //validation for duplicate names
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("UNIQUE constraint failed: Sections.Name"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe una Sección con el mismo nombre.");
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
            return View(section);
        }


        public IActionResult Details(int id)
        {
            Section? section = _sectionRepository.GetSection(id);
            if (section == null)
            {
                return NotFound();
            }
            return View(section);
        }

        public IActionResult Edit(int id)
        {
            Section? section = _sectionRepository.GetSection(id);
            if (section == null)
            {
                return NotFound();
            }
            return View(section);
        }

        [HttpPost]
        public IActionResult Edit(Section section)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _sectionRepository.EditSection(section);
                    TempData["mensaje"] = "La sección se actualizó correctamente";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("UNIQUE constraint failed: Sections.Name"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe una section con el mismo nombre.");
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
            return View(section);
        }


        public IActionResult Delete(int id)
        {
            Section? section = _sectionRepository.GetSection(id);
            if (section == null)
            {
                return NotFound();
            }
            return View(section);
        }

        [HttpPost]
        public IActionResult Delete(Section section)
        {

            _sectionRepository.DeleteSection(section);
            TempData["mensaje"] = "La section se eliminó correctamente";
            return RedirectToAction("Index");
        }
    }
}
