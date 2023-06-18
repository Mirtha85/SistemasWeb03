using Microsoft.AspNetCore.Mvc.Rendering;
using SistemasWeb01.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SistemasWeb01.ViewModels
{
    public class ProductSizeViewModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }

        [Display(Name = "Talla")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una talla.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int TallaId { get; set; }

        public IEnumerable<Talla>? Tallas { get; set; }
        public int? Quantity { get; set; }

    }
}
