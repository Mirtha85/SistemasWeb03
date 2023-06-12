using SistemasWeb01.Models;
using SistemasWeb01.ViewModels;

namespace SistemasWeb01.Repository.Implementations
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> AllCategories { get; }
        Category? GetCategoryById(int id);

        void CreateCategory(CategoryViewModel categoryViewModel);

        void EditCategory(CategoryViewModel categoryViewModel);

        void DeleteCategory(Category category);
    }
}
