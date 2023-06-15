using Microsoft.AspNetCore.Mvc.Rendering;
using SistemasWeb01.Models;

namespace SistemasWeb01.Helpers
{
    public interface ICombosHelper
    {
        Task<IEnumerable<SelectListItem>> GetComboBrands();

        Task<IEnumerable<SelectListItem>> GetComboTallas();

    }
}
