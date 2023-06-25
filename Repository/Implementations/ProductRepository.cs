using Microsoft.EntityFrameworkCore;
using SistemasWeb01.DataAccess;
using SistemasWeb01.Models;
using SistemasWeb01.Repository.Interfaces;
using System.Drawing.Drawing2D;

namespace SistemasWeb01.Repository.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private readonly ShoppingDbContext _shoppingDbContext;
        public ProductRepository(ShoppingDbContext shoppingDbContext)
        {
            _shoppingDbContext = shoppingDbContext;
        }
        public IEnumerable<Product> AllProducts => _shoppingDbContext.Products
            .Include(p => p.Pictures)
            .Include(p => p.Brand)
            .Include(p => p.ProductSizes!)
            .ThenInclude(p => p.Talla)
            .Include(p => p.SubCategory)
            .ThenInclude(p => p.Category)
            .OrderByDescending(p => p.Id)
            .ToList();

        public IEnumerable<Product> ProductsNotDeleted
        {
            get
            {
                return _shoppingDbContext.Products.Where(p => p.IsDeleted == false)
                    .Include(p => p.Brand)
                    .Include(p => p.SubCategory)
                    .ThenInclude(p => p.Category)
                    .OrderByDescending(p => p.Id)
                    .ToList();
            }
        }

        public IEnumerable<Product> BestSellingProducts
        {
            get
            {
                return _shoppingDbContext.Products
                    .Include(p => p.Pictures)
                    .Include(p => p.Brand)
                    .Include(p => p.SubCategory)
                    .ThenInclude(p => p.Category)
                    .Include(p => p.ProductSizes!)
                    .ThenInclude(p => p.Talla)
                    .Where(p => p.IsBestSeller);
            }
        }

        public IEnumerable<Product> NewProducts
        {
            get
            {
                return _shoppingDbContext.Products
                    .Include(p => p.Pictures)
                    .Include(p => p.SubCategory)
                    .ThenInclude(p => p.Category)
                    .Where(p => p.IsNew);
            }
        }

        public void CreateProduct(Product product)
        {
            try
            {
                _shoppingDbContext.Products.Add(product);
                _shoppingDbContext.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void DeleteProduct(Product product)
        {
            try
            {
                _shoppingDbContext.Products.Remove(product);
                _shoppingDbContext.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void DisableProduct(Product product)
        {
            try
            {
                _shoppingDbContext.Products.Update(product);
                _shoppingDbContext.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void EditProduct(Product product)
        {
            try
            {
                _shoppingDbContext.Products.Update(product);
                _shoppingDbContext.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Product? GetProductById(int id)
        {
            var product = _shoppingDbContext.Products
                .Include(p => p.Brand)
                .Include(p => p.Pictures)
                .Include(p => p.ProductSizes!)
                .ThenInclude(ps => ps.Talla)
                .Include(p => p.SubCategory)
                .ThenInclude(p => p.Category)
                .FirstOrDefault(p => p.Id == id);

            return product;
        }
    }
}
