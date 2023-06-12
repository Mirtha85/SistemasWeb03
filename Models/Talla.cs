﻿using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SistemasWeb01.Models
{
    public class Talla
    {
        public int Id { get; set; }

        [Display(Name = "Talla")]
        public string? Name { get; set; }

        [Display(Name = "Inicial")]
        public string? ShortName { get; set; }

        public ICollection<ProductSize>? ProductSizes { get; set; }
    }
}
