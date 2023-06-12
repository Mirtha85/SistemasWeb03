﻿using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SistemasWeb01.Models
{
    public class Picture
    {
        public int Id { get; set; }

        [Display(Name = "Foto")]
        public string? PictureName { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }

        [Display(Name = "Foto")]
        public string ImageFullPath => PictureName == string.Empty
            ? $"https://localhost:57270/images/noimage.png"
            : $"https://localhost:57270/images/{PictureName}";
    }
}