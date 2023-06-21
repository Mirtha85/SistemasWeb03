using Microsoft.EntityFrameworkCore;
using SistemasWeb01.DataAccess;
using SistemasWeb01.Models;
using SistemasWeb01.Repository.Interfaces;

namespace SistemasWeb01.Repository.Implementations
{
    public class StateRepository : IStateRepository
    {
        private readonly ShoppingDbContext _shoppingDbContext;
        public StateRepository(ShoppingDbContext shoppingDbContext)
        {
            _shoppingDbContext = shoppingDbContext;
        }
        public IEnumerable<State> AllStates => _shoppingDbContext.States.ToList();

        public void CreateState(State state)
        {
            try
            {
                _shoppingDbContext.States.Add(state);
                _shoppingDbContext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteState(State state)
        {
            try
            {
                _shoppingDbContext.States.Remove(state);
                _shoppingDbContext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void EditState(State state)
        {
            try
            {
                _shoppingDbContext.States.Update(state);
                _shoppingDbContext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public State? GetStateById(int id)
        {
            State? state = _shoppingDbContext.States
                .Include(c => c.Country)
                .Include(c => c.Cities!)
                .FirstOrDefault(s => s.Id == id);
            return state;
        }

        public IEnumerable<State> StatesByCountryId(int countryId)
        {
            IEnumerable<State> statesByCountry = _shoppingDbContext.States
                 .Where(c => c.CountryId == countryId)
                 .OrderBy(c => c.Name)
                 .ToList();
            return statesByCountry;
        }
    }
}
