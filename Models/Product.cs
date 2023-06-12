﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Drawing.Drawing2D;
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
        public string? Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Display(Name = "Precio")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public decimal Price { get; set; }


        [Display(Name = "Inventario")]
        public int? InStock { get; set; }

        public bool IsDeleted { get; set; } = false;
        public bool IsNew { get; set; } = false;
        public bool IsBestSeller { get; set; } = false;
        public int? PercentageDiscount { get; set; }

        [Display(Name = "SubCategoría")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int SubCategoryId { get; set; }
        public SubCategory? SubCategory { get; set; }
        public int? BrandId { get; set; }
        public Brand? Brand { get; set; }
        public List<Picture>? Pictures { get; set; }
        public List<ProductSize>? ProductSizes { get; set; }

        //read property
        [Display(Name = "Foto")]
        public string ImageFullPath => Pictures == null || Pictures.Count == 0
            ? $"https://localhost:57270/images/noimage.jpg"
            : Pictures.FirstOrDefault().ImageFullPath;
    }
}