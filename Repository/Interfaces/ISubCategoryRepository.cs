using SistemasWeb01.Models;
using System.Security.Policy;

namespace SistemasWeb01.Repository.Interfaces
{
    public interface ISubCategoryRepository
    {
        IEnumerable<SubCategory> AllSubCategories { get; }
        SubCategory? GetSubCategoryById(int id);
        IEnumerable<SubCategory> GetSubCategoriesByCategoryId(int categoryId);

        void CreateSubCategory(SubCategory subcategory);

        void EditSubCategory(SubCategory subcategory);

        void DeleteSubCategory(SubCategory subcategory);
    }
}
