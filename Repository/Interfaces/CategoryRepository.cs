using SistemasWeb01.DataAccess;
using SistemasWeb01.Models;
using SistemasWeb01.Repository.Implementations;
using SistemasWeb01.ViewModels;
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

        public void CreateCategory(CategoryViewModel categoryViewModel)
        {
            try
            {
                Category category = new()
                {
                    Name = categoryViewModel.Name,
                    ThumbnailImage = categoryViewModel.ThumbnailImage
                };
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

        public void EditCategory(CategoryViewModel categoryViewModel)
        {
            try
            {
                Category category = new()
                {
                    Id = categoryViewModel.Id,
                    Name = categoryViewModel.Name,
                    ThumbnailImage = categoryViewModel.ThumbnailImage
                };
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
