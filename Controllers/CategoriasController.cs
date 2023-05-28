using Microsoft.AspNetCore.Mvc;
using SistemasWeb01.Models;

namespace SistemasWeb01.Controllers
{
    public class CategoriasController : Controller
    {
        
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriasController(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public IActionResult Index()
        {
            IEnumerable<Categoria> categorias = _categoriaRepository.AllCategories;
            return View(categorias);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _categoriaRepository.CreateCategory(categoria);
                TempData["mensaje"] = "La categoria se creó correctamente";
                return RedirectToAction("Index");
            }
            return View(categoria);
        }


        public IActionResult Details(int id)
        {
            Categoria? categoria = _categoriaRepository.GetCategory(id);
            return View(categoria);
        }

        public IActionResult Edit(int id)
        {
            Categoria? categoria = _categoriaRepository.GetCategory(id);
            return View(categoria);
        }

        [HttpPost]
        public IActionResult Edit(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _categoriaRepository.EditCategory(categoria);
                TempData["mensaje"] = "La categoria se actualizó correctamente";
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }


        public IActionResult Delete(int id)
        {
            Categoria? categoria = _categoriaRepository.GetCategory(id);
            return View(categoria);
        }

        [HttpPost]
        public IActionResult Delete(Categoria categoria)
        {

            _categoriaRepository.DeleteCategory(categoria);
            TempData["mensaje"] = "La categoria se eliminó correctamente";
            return RedirectToAction("Index");
        }
    }
}
