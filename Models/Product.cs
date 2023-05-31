using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SistemasWeb01.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Descripción")]
        [MaxLength(500, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        public string Description { get; set; }

        
        [Display(Name = "Precio")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int Price { get; set; }

        
        [Display(Name = "Inventario")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int Stock { get; set; }

        public bool IsDeleted { get; set; } = false;
        public bool IsNew { get; set; } = true;

        [Display(Name = "Categoría")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; } = default!;

        public string? ImageUrl { get; set; }

        public List<ProductImage> ProductImages { get; set; }

        [Display(Name = "Fotos")]
        public int ImagesNumber => ProductImages == null ? 0 : ProductImages.Count;

        [Display(Name = "Foto")]
        public string ImageFullPath => ProductImages == null || ProductImages.Count == 0
            ? $"~/images/noimage.jpg"
            : ProductImages.FirstOrDefault().ImageFullPath;
    }
}
