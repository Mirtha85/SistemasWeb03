using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SistemasWeb01.Models
{
    public class TemporalSale
    {
        public int Id { get; set; }
        public User User { get; set; }

        public Product Product { get; set; }

        [Display(Name = "Talla")]
        public ProductSize? ProductSize { get; set; }

        [Display(Name = "Cantidad")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int Quantity { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Comentarios")]
        public string? Remarks { get; set; }
    }

}
