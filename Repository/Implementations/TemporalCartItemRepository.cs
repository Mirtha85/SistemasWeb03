using Microsoft.EntityFrameworkCore;
using SistemasWeb01.DataAccess;
using SistemasWeb01.Models;
using SistemasWeb01.Repository.Interfaces;

namespace SistemasWeb01.Repository.Implementations
{
    public class TemporalCartItemRepository : ITemporalCartItemRepository
    {
        private readonly ShoppingDbContext _shoppingDbContext;
        public TemporalCartItemRepository(ShoppingDbContext shoppingDbContext)
        {
            _shoppingDbContext = shoppingDbContext;
        }
        public void CreateTemporalCartItem(TemporalCartItem temporalCartItem)
        {
            try
            {
                _shoppingDbContext.TemporalCartItems.Add(temporalCartItem);
                _shoppingDbContext.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void DeleteTemporalCartItem(TemporalCartItem temporalCartItem)
        {
            try
            {
                _shoppingDbContext.TemporalCartItems.Remove(temporalCartItem);
                _shoppingDbContext.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void EditTemporalCartItem(TemporalCartItem temporalCartItem)
        {
            try
            {
                _shoppingDbContext.TemporalCartItems.Update(temporalCartItem);
                _shoppingDbContext.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public TemporalCartItem? GetTemporalCartItemById(int id)
        {
            return _shoppingDbContext.TemporalCartItems
                .Include(t => t.User)
                .Include(t => t.Product)
                .ThenInclude(tp => tp.Pictures)
                .Include(t => t.ProductSize)
                .ThenInclude(tps => tps.Talla)
                .FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<TemporalCartItem> GetTemporalCartItemsByUserId(string userId)
        {
            return _shoppingDbContext.TemporalCartItems

               .Include(s => s.Product)
               .ThenInclude(sp => sp.Pictures)
               .Include(s => s.ProductSize)
               .ThenInclude(sp => sp.Talla)
               .Where(ts => ts.User.Id == userId).ToList();

        }
    }
}
