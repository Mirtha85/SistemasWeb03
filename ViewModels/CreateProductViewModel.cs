using Microsoft.AspNetCore.Mvc.Rendering;
using SistemasWeb01.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Policy;
using System.Xml.Linq;

namespace SistemasWeb01.ViewModels
{
    public class CreateProductViewModel : EditProductViewModel
    {

        [Display(Name = "Categoría")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una categoría.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int CategoryId { get; set; }
        public IEnumerable<Category>? Categories { get; set; }

        [DisplayName("SubCategoría")]
        public int SubCategoryId { get; set; }
        public IEnumerable<SubCategory>? SubCategories { get; set; } = new List<SubCategory>();


        [Display(Name = "Talla")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una talla.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int TallaId { get; set; }
        public IEnumerable<Talla>? Tallas { get; set; }

        public int? BrandId { get; set; }
        public IEnumerable<Brand>? Brands { get; set; }


        [Display(Name = "Foto")]
        public IFormFile? ImageFile { get; set; }


    }
}
