using SistemasWeb01.Models;
using static System.Collections.Specialized.BitVector32;

namespace SistemasWeb01.Repository.Interfaces
{
    public interface ICountryRepository
    {
        IEnumerable<Country> AllCountries { get; }
        Country? GetCountryById(int id);

        void CreateCountry(Country country);

        void EditCountry(Country country);

        void DeleteCountry(Country country);
    }
}
