using Microsoft.AspNetCore.Mvc.Rendering;
using SistemasWeb01.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SistemasWeb01.ViewModels
{
    public class AddProductToCartViewModel
    {
        //public int Id { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public IEnumerable<SelectListItem>? ProductSizes { get; set; }

        [Display(Name = "Talla")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una talla.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]

        public int ProductSizeId { get; set; }
        public ProductSize? ProductSize { get; set; }

        [Range(0.0000001, int.MaxValue, ErrorMessage = "Ingresa un valor mayor a cero.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]

        public int Amount { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Comentarios")]
        public string? Remarks { get; set; }

    }
}
