using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SistemasWeb01.Models
{
    public class Picture
    {
        public int Id { get; set; }

        [Display(Name = "Foto")]
        public string? PictureName { get; set; }

        [Display(Name = "Producto")]
        public int ProductId { get; set; }
        public Product? Product { get; set; }

    }
}
