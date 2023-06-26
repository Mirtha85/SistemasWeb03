using Microsoft.AspNetCore.Mvc.Rendering;
using SistemasWeb01.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SistemasWeb01.ViewModels
{
    public class DetailsProductViewModel
    {
        public Product Product { get; }
        public IEnumerable<SelectListItem>? ProductSizes { get; set; }
        public string TallasDisponibles { get; set; } = string.Empty;

        [Display(Name = "Talla")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una talla.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int ProductSizeId { get; set; }
        public ProductSize productSize { get; set; }
        public int Amount { get; set; }
        public DetailsProductViewModel( Product product, IEnumerable<SelectListItem>? productSizes)
        {
            Product = product;
            ProductSizes = productSizes;
          
        }
    }
}
