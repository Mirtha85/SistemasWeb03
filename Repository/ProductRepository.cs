using Microsoft.EntityFrameworkCore;
using SistemasWeb01.Models;
using SistemasWeb01.Repository.IRepository;

namespace SistemasWeb01.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly BethesdaPieShopDbContext _bethesdaPieShopDbContext;
        public ProductRepository(BethesdaPieShopDbContext bethesdaPieShopDbContext)
        {
            _bethesdaPieShopDbContext = bethesdaPieShopDbContext;
        }
        public IEnumerable<Product> AllProducts
        {
            get
            {
                return _bethesdaPieShopDbContext.Products
                    .Include(p => p.Categoria)
                    .Include(p => p.ProductImages)
                    .OrderByDescending(p => p.Id).ToList();
            }
        }
            
    }
}
