﻿using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SistemasWeb01.Models
{
    public class Country
    {
        public int Id { get; set; }

        [Display(Name = "País")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; }

        public List<State>? States { get; set; }

        [Display(Name = "Departamentos")]
        public int StatesNumber => States == null ? 0 : States.Count;

        [Display(Name = "Ciudades")]
        public int CitiesNumber => States == null ? 0 : States.Sum(s => s.CitiesNumber);
    }
}
