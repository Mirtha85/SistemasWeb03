using Microsoft.EntityFrameworkCore;
using SistemasWeb01.DataAccess;
using SistemasWeb01.Models;
using SistemasWeb01.Repository.Interfaces;

namespace SistemasWeb01.Repository.Implementations
{
    public class CityRepository : ICityRepository
    {
        private readonly ShoppingDbContext _shoppingDbContext;
        public CityRepository(ShoppingDbContext shoppingDbContext)
        {
            _shoppingDbContext = shoppingDbContext;
        }
        public IEnumerable<City> AllCities => _shoppingDbContext.Cities.ToList();

        public void CreateCity(City city)
        {
            try
            {
                _shoppingDbContext.Cities.Add(city);
                _shoppingDbContext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteCity(City city)
        {
            try
            {
                _shoppingDbContext.Cities.Remove(city);
                _shoppingDbContext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void EditCity(City city)
        {
            try
            {
                _shoppingDbContext.Cities.Update(city);
                _shoppingDbContext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public City? GetCityById(int id)
        {
            City? city = _shoppingDbContext.Cities
                .Include(c => c.State)
                .FirstOrDefault(s => s.Id == id);
            return city;
        }
    }
}
