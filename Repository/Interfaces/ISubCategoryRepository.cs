using SistemasWeb01.Models;

namespace SistemasWeb01.Repository.Interfaces
{
    public interface ISubCategoryRepository
    {
        IEnumerable<SubCategory> AllSubCategories { get; }
        SubCategory? GetSubCategoryById(int id);

        void CreateSubCategory(SubCategory subcategory);

        void EditSubCategory(SubCategory subcategory);

        void DeleteSubCategory(SubCategory subcategory);
    }
}
