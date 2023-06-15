using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SistemasWeb01.Models
{
    public class Talla
    {
        public int Id { get; set; }

        [Display(Name = "Talla")]
        public string? Name { get; set; }

        [Display(Name = "Abreviación")]
        public string? ShortName { get; set; }

        [Display(Name = "Número")]
        public string? SizeNumber { get; set; }

        public ICollection<ProductSize>? ProductSizes { get; set; }
    }
}
