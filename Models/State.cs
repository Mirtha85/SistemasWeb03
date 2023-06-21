using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace SistemasWeb01.Models
{
    public class State
    {
        public int Id { get; set; }

        [Display(Name = "Departamento")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; }

        [Display(Name = "País")]
        public int CountryId { get; set; }

        [JsonIgnore]
        public Country? Country { get; set; }

        public List<City>? Cities { get; set; }

        [Display(Name = "Ciudades")]
        public int CitiesNumber => Cities == null ? 0 : Cities.Count;
    }
}
