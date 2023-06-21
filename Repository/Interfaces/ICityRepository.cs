using SistemasWeb01.Models;

namespace SistemasWeb01.Repository.Interfaces
{
    public interface ICityRepository
    {
        IEnumerable<City> AllCities { get; }
        City? GetCityById(int id);

        void CreateCity(City city);

        void EditCity(City city);

        void DeleteCity(City city);
    }
}
