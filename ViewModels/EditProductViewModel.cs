using SistemasWeb01.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SistemasWeb01.ViewModels
{
    public class EditProductViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; }

        [Display(Name = "Descripción")]
        [MaxLength(500, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string? Description { get; set; }


        [Display(Name = "Categoría")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una categoría.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int CategoryId { get; set; }
        public IEnumerable<Category>? Categories { get; set; }

        [DisplayName("SubCategoría")]
        public int SubCategoryId { get; set; }
        public IEnumerable<SubCategory>? SubCategories { get; set; } = new List<SubCategory>();

        [Display(Name = "Marca")]
        public int? BrandId { get; set; }
        public IEnumerable<Brand>? Brands { get; set; } = new List<Brand>();


        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Display(Name = "Precio")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public decimal Price { get; set; }

        [Display(Name = "Stock")]
        public int? InStock { get; set; }

        [Display(Name = "Activo")]
        public bool IsDeleted { get; set; } = false;

        [Display(Name = "Nuevo")]
        public bool IsNew { get; set; } = false;

        [Display(Name = "Más Vendido")]
        public bool IsBestSeller { get; set; } = false;

        [Display(Name = "% Descuento")]
        public int? PercentageDiscount { get; set; } = 0;
    }

}
