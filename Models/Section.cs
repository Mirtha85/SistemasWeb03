using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SistemasWeb01.Models
{
    public class Section
    {
        public int Id { get; set; }

        [Display(Name = "Sección")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; }
    }
}
