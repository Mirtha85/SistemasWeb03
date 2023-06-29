using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SistemasWeb01.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }

        [Display(Name = "Orden")]
        public int OrderId { get; set; }
        public Order Order { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Comentarios")]
        public string? Remarks { get; set; }

        [Display(Name = "Producto")]
        public int ProductId { get; set; }
        [Display(Name = "Producto")]
        public Product Product { get; set; }


        [Display(Name = "Talla")]
        public int? ProductSizeId { get; set; }
        [Display(Name = "Talla")]
        public ProductSize? ProductSize { get; set; }


        [Display(Name = "Cantidad")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int Quantity { get; set; }

        [Display(Name = "Valor")]
        public decimal Value => Product == null ? 0 : Quantity * Product.Price;

    }
}
