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

        public void DeleteTemporalSale(TemporalSale temporalSale)
        {
            try
            {
                _shoppingDbContext.TemporalSales.Remove(temporalSale);
                _shoppingDbContext.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void EditTemporalSale(TemporalSale temporalSale)
        {
            try
            {
                _shoppingDbContext.TemporalSales.Update(temporalSale);
                _shoppingDbContext.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public TemporalSale? GetTemporalSaleById(int id)
        {
            return _shoppingDbContext.TemporalSales
                .Include(t => t.User)
                .Include(t => t.Product)
                .ThenInclude(tp => tp.Pictures)
                .Include(t => t.ProductSize)
                .ThenInclude(tps => tps.Talla)
                .FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<TemporalSale> GetTemporalSalesByUserId(string userId)
        {
             return _shoppingDbContext.TemporalSales

                .Include(s => s.Product)
                .ThenInclude(sp => sp.Pictures)
                .Include(s => s.ProductSize)
                .ThenInclude(sp => sp.Talla)
                .Where(ts => ts.User.Id == userId).ToList();

        }
    }
}
