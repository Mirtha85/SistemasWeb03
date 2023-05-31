using SistemasWeb01.Models;
using SistemasWeb01.Repository.IRepository;

namespace SistemasWeb01.Repository
{
    public class ProductImageRepository : IProductImageRepository
    {
        private readonly BethesdaPieShopDbContext _bethesdaPieShopDbContext;
        public ProductImageRepository(BethesdaPieShopDbContext bethesdaPieShopDbContext)
        {
            _bethesdaPieShopDbContext = bethesdaPieShopDbContext;
        }
        public IEnumerable<ProductImage> AllProductImages => _bethesdaPieShopDbContext.ProductImages.ToList();
    }
}
