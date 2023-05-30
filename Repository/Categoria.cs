using System.ComponentModel.DataAnnotations;

namespace SistemasWeb01.Repository
{
    public class Categoria
    {
        public int Id { get; set; }

        [Display(Name = "Categoria")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
