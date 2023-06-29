using SistemasWeb01.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SistemasWeb01.ViewModels
{
	public class ShowCartViewModel
	{
		public User User { get; set; }

		[DataType(DataType.MultilineText)]
		[Display(Name = "Comentarios")]
		public string? Remarks { get; set; }

		public IEnumerable<TemporalCartItem> TemporalCartItems { get; set; }

		
		[Display(Name = "Cantidad")]
		public int Quantity => TemporalCartItems == null ? 0 : TemporalCartItems.Sum(ts => ts.Quantity);

		//[DisplayFormat(DataFormatString = "{0:C2}")]
		[Display(Name = "Valor")]
		public decimal Value => TemporalCartItems == null ? 0 : TemporalCartItems.Sum(ts => ts.Value);
	}

}
