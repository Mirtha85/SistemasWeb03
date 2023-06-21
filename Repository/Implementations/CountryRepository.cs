using Microsoft.EntityFrameworkCore;
using SistemasWeb01.DataAccess;
using SistemasWeb01.Models;
using SistemasWeb01.Repository.Interfaces;
using static System.Collections.Specialized.BitVector32;

namespace SistemasWeb01.Repository.Implementations
{
    public class CountryRepository : ICountryRepository
    {
        private readonly ShoppingDbContext _shoppingDbContext;
        public CountryRepository(ShoppingDbContext shoppingDbContext)
        {
            _shoppingDbContext = shoppingDbContext;
        }
        public IEnumerable<Country> AllCountries => _shoppingDbContext.Countries
            .Include(c => c.States!)
            .ThenInclude(cs => cs.Cities)
            .ToList();
        public void CreateCountry(Country country)
        {
            try
            {
                _shoppingDbContext.Countries.Add(country);
                _shoppingDbContext.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void DeleteCountry(Country country)
        {
            try
            {
                _shoppingDbContext.Countries.Remove(country);
                _shoppingDbContext.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void EditCountry(Country country)
        {
            try
            {
                _shoppingDbContext.Countries.Update(country);
                _shoppingDbContext.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Country? GetCountryById(int id)
        {
            Country? country = _shoppingDbContext.Countries
                .Include(c => c.States!)
                .ThenInclude(c => c.Cities)
                .FirstOrDefault(s => s.Id == id);
            return country;
        }
    }
}
