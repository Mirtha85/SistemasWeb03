using SistemasWeb01.Models;
using System.ComponentModel.DataAnnotations;

namespace SistemasWeb01.ViewModels
{
    public class AddProductImageViewModel
    {
        public int ProductId { get; set; }
        public Product? Product { get; set; }

        [Display(Name = "Foto")]
        public string? PictureName { get; set; }

        [Display(Name = "Foto")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public IFormFile ImageFile { get; set; }
    }

}
