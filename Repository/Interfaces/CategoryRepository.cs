using SistemasWeb01.DataAccess;
using SistemasWeb01.Models;
using SistemasWeb01.Repository.Implementations;
using static System.Collections.Specialized.BitVector32;

namespace SistemasWeb01.Repository.Interfaces
{
    
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ShoppingDbContext _shoppingDbContext;
        public CategoryRepository(ShoppingDbContext shoppingDbContext)
        {
            _shoppingDbContext = shoppingDbContext;
        }
        public IEnumerable<Category> AllCategories => _shoppingDbContext.Categories.ToList();

        public void CreateCategory(Category category)
        {
            try
            {
                _shoppingDbContext.Categories.Add(category);
                _shoppingDbContext.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void DeleteCategory(Category category)
        {
            try
            {
                _shoppingDbContext.Categories.Remove(category);
                _shoppingDbContext.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void EditCategory(Category category)
        {
            try
            {
                _shoppingDbContext.Categories.Update(category);
                _shoppingDbContext.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Category? GetCategoryById(int id)
        {
            return _shoppingDbContext.Categories.FirstOrDefault(c => c.Id == id);
        }
    }
}
