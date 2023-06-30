using SistemasWeb01.Enums;
using System.ComponentModel.DataAnnotations;

namespace SistemasWeb01.Models
{
    public class Order
    {
        public int Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}")]
        [Display(Name = "Fecha")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public DateTime Date { get; set; }

        public User User { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Comentarios")]
        public string? Remarks { get; set; }

        [Display(Name = "Estado")]
        public OrderStatus OrderStatus { get; set; }

        public List<OrderDetail> OderDetails { get; set; }

        [DisplayFormat(DataFormatString = "{0:N0}")]
        [Display(Name = "Líneas")]
        public int Lines => OderDetails == null ? 0 : OderDetails.Count;

        
        [Display(Name = "Cantidad")]
        public int Quantity => OderDetails == null ? 0 : OderDetails.Sum(sd => sd.Quantity);

        
        [Display(Name = "Valor")]
        public decimal Value => OderDetails == null ? 0 : OderDetails.Sum(sd => sd.Value);
    }

}
