using SistemasWeb01.Models;
using SistemasWeb01.Repository.IRepository;

namespace SistemasWeb01.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BethesdaPieShopDbContext _bethesdaPieShopDbContext;

        public CategoryRepository(BethesdaPieShopDbContext bethesdaPieShopDbContext)
        {
            _bethesdaPieShopDbContext = bethesdaPieShopDbContext;
        }

        public IEnumerable<Category> AllCategories => _bethesdaPieShopDbContext.Categories.OrderBy(p => p.CategoryName);
    }
}