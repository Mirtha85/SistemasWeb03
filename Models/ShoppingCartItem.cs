using System.ComponentModel.DataAnnotations;
using System.IO.Pipelines;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace SistemasWeb01.Models
{
    public class ShoppingCartItem
    {
        public int ShoppingCartItemId { get; set; }
        public int ProductId { get; set; }

        [JsonIgnore]
        public Product  Product { get; set; } = default!;
        
        [Display(Name = "Talla")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una talla.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int ProductSizeId { get; set; }


        [JsonIgnore]
        public ProductSize ProductSize { get; set; } = default!;

        [Display(Name = "Cantidad")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes de colocar una cantidad mayor a 1.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int Amount { get; set; }
        public string? ShoppingCartId { get; set; }
    }
}
