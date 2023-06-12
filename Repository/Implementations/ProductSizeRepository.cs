using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SistemasWeb01.DataAccess;
using SistemasWeb01.Models;
using SistemasWeb01.Repository.Interfaces;

namespace SistemasWeb01.Repository.Implementations
{
    public class ProductSizeRepository : IProductSizeRepository
    {
        private readonly ShoppingDbContext _shoppingDbContext;
        public ProductSizeRepository(ShoppingDbContext shoppingDbContext)
        {
            _shoppingDbContext = shoppingDbContext;
        }
        public IEnumerable<ProductSize> AllProductSizes => _shoppingDbContext.ProductSizes.ToList();

        public void CreateProductSize(ProductSize productSize)
        {
            try
            {
                _shoppingDbContext.ProductSizes.Add(productSize);
                _shoppingDbContext.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void DeleteProductSiza(ProductSize productSize)
        {
            _shoppingDbContext.ProductSizes.Remove(productSize);
            _shoppingDbContext.SaveChanges();
        }

        public void EditProductSize(ProductSize productSize)
        {
            try
            {
                _shoppingDbContext.ProductSizes.Update(productSize);
                _shoppingDbContext.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ProductSize? GetProductSizeById(int id)
        {
            return _shoppingDbContext.ProductSizes.FirstOrDefault(p => p.Id == id);
        }
    }
}
