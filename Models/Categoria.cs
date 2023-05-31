using System.ComponentModel.DataAnnotations;

namespace SistemasWeb01.Models
{
    public class Categoria
    {
        public int Id { get; set; }

        [Display(Name = "Categoria")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; }
        
        [DataType(DataType.MultilineText)]
        [Display(Name = "Descripción")]
        [MaxLength(500, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        public string? Description { get; set; }
        public List<Product>? Products { get; set; }
    }
}
