using SistemasWeb01.Models;

namespace SistemasWeb01.Repository.Implementations
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> AllCategories { get; }
        Category? GetCategoryById(int id);

        void CreateCategory(Category category);

        void EditCategory(Category category);

        void DeleteCategory(Category category);
    }
}
