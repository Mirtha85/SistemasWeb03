using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SistemasWeb01.Models
{
    public class ProductImage
    {
        public int Id { get; set; }

        public Product Product { get; set; }
        public int ProductId { get; set; }

        [Display(Name = "Foto")]
        public string ImageFullPath { get; set; }
    }
}
