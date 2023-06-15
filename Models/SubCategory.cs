using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SistemasWeb01.Models
{
    public class SubCategory
    {
        public int Id { get; set; }

        [Display(Name = "SubCategoría")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; }

        [Display(Name = "Categoría")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
