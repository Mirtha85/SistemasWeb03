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

        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Display(Name = "Precio")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public decimal Price { get; set; }

        [Display(Name = "Inventario")]
        public int? InStock { get; set; }

        [Display(Name = "Activo")]
        public bool IsDeleted { get; set; } = false;

        [Display(Name = "Nuevo")]
        public bool IsNew { get; set; } = false;

        [Display(Name = "Más Vendido")]
        public bool IsBestSeller { get; set; } = false;

        [Display(Name = "% Descuento")]
        public int? PercentageDiscount { get; set; }
    }

}
