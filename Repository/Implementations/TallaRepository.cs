using Microsoft.EntityFrameworkCore;
using SistemasWeb01.DataAccess;
using SistemasWeb01.Models;
using SistemasWeb01.Repository.Interfaces;

namespace SistemasWeb01.Repository.Implementations
{
    public class TallaRepository : ITallaRepository
    {
        private readonly ShoppingDbContext _shoppingDbContext;
        public TallaRepository(ShoppingDbContext shoppingDbContext)
        {
            _shoppingDbContext = shoppingDbContext;
        }
        public IEnumerable<Talla> AllTallas => _shoppingDbContext.Tallas.Include(t => t.ProductSizes).ToList();

        public void CreateTalla(Talla talla)
        {
            try
            {
                _shoppingDbContext.Tallas.Add(talla);
                _shoppingDbContext.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void DeleteTalla(Talla talla)
        {
            _shoppingDbContext.Tallas.Remove(talla);
            _shoppingDbContext.SaveChanges();
        }

        public void EditTalla(Talla talla)
        {
            try
            {
                _shoppingDbContext.Tallas.Update(talla);
                _shoppingDbContext.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Talla GetTallaById(int id)
        {
            return _shoppingDbContext.Tallas.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Talla> TallasFiltered(IEnumerable<Talla> filter)
        {
            List<Talla> tallas = _shoppingDbContext.Tallas.ToList();
            List<Talla> tallasFiltered = new();
            foreach(Talla talla in tallas)
            {
                if(!filter.Any(t => t.Id == talla.Id))
                {
                    tallasFiltered.Add(talla);
                }
            }
            return tallasFiltered;
        }
    }

        
}
