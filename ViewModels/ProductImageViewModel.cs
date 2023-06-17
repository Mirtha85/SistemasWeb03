using SistemasWeb01.Models;
using System.ComponentModel.DataAnnotations;

namespace SistemasWeb01.ViewModels
{
    public class ProductImageViewModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }

        [Display(Name = "Foto")]
        public string? PictureName { get; set; }

        [Display(Name = "Foto")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public IFormFile ImageFile { get; set; }
    }

}
