using Microsoft.AspNetCore.Mvc.Rendering;
using SistemasWeb01.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SistemasWeb01.ViewModels
{
    public class EditTemporalSaleViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Talla")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una talla.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]

        public int? ProductId { get; set; }
        public Product? Product { get; set; }

        public int? ProductSizeId { get; set; }
        public ProductSize? ProductSize { get; set; }
        public IEnumerable<ProductSize>? ProductSizes { get; set; } = new List<ProductSize>();

        [DataType(DataType.MultilineText)]
        [Display(Name = "Comentarios")]
        public string? Remarks { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Display(Name = "Cantidad")]
        [Range(0.0000001, int.MaxValue, ErrorMessage = "Ingresar un valor mayor a cero.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int Quantity { get; set; }

    }
}
