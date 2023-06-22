using Microsoft.AspNetCore.Mvc.Rendering;
using SistemasWeb01.Models;

namespace SistemasWeb01.Helpers
{
    public interface ICombosHelper
    {

        Task<IEnumerable<SelectListItem>> GetComboCountriesAsync();

        Task<IEnumerable<SelectListItem>> GetComboStatesAsync(int countryId);

        Task<IEnumerable<SelectListItem>> GetComboCitiesAsync(int stateId);

    }
}
