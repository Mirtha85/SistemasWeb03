using Microsoft.EntityFrameworkCore;
using SistemasWeb01.DataAccess;
using SistemasWeb01.Models;
using SistemasWeb01.Repository.Interfaces;

namespace SistemasWeb01.Repository.Implementations
{
    public class TemporalSaleRepository : ITemporalSaleRepository
    {
        private readonly ShoppingDbContext _shoppingDbContext;
        public TemporalSaleRepository(ShoppingDbContext shoppingDbContext)
        {
            _shoppingDbContext = shoppingDbContext;
        }
        public void CreateTempalSale(TemporalSale temporalSale)
        {
            try
            {
                _shoppingDbContext.TemporalSales.Add(temporalSale);
                _shoppingDbContext.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<TemporalSale> GetTemporalSalesByUserId(string userId)
        {
             return _shoppingDbContext.TemporalSales
            .Where(ts => ts.User.Id == userId).ToList();

        }
    }
}
