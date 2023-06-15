using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SistemasWeb01.Models
{
    public class ProductSize
    {
        public int Id { get; set; }

        [Display(Name = "Producto")]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        [Display(Name = "Talla")]
        public int TallaId { get; set; }
        public Talla Talla { get; set; }

        [Display(Name = "Cantidad")]

        public int? Quantity { get; set; }
    }
}
