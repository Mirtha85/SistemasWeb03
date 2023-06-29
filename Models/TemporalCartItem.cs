using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace SistemasWeb01.Models
{
    public class TemporalCartItem
    {
        public int Id { get; set; }
        
        public User User { get; set; }

        public int ProductId { get; set; }

        [JsonIgnore]
        public Product Product { get; set; }

        public int ProductSizeId { get; set; }

        [Display(Name = "Talla")]
        [JsonIgnore]
        public ProductSize? ProductSize { get; set; }

        [Display(Name = "Cantidad")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int Quantity { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Comentarios")]
        public string? Remarks { get; set; }


        [Display(Name = "Valor")]
        public decimal Value => Product == null ? 0 : Quantity * Product.Price;
    }
}
