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

        public OrderStatus OrderStatus { get; set; }

        public List<OrderDetail> OderDetail { get; set; }

        [DisplayFormat(DataFormatString = "{0:N0}")]
        [Display(Name = "Líneas")]
        public int Lines => OderDetail == null ? 0 : OderDetail.Count;

        
        [Display(Name = "Cantidad")]
        public int Quantity => OderDetail == null ? 0 : OderDetail.Sum(sd => sd.Quantity);

        
        [Display(Name = "Valor")]
        public decimal Value => OderDetail == null ? 0 : OderDetail.Sum(sd => sd.Value);
    }

}
