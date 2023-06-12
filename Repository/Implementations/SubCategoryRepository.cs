using Microsoft.EntityFrameworkCore;
using SistemasWeb01.DataAccess;
using SistemasWeb01.Models;
using SistemasWeb01.Repository.Interfaces;

namespace SistemasWeb01.Repository.Implementations
{
    public class SubCategoryRepository : ISubCategoryRepository
    {
        private readonly ShoppingDbContext _shoppingDbContext;
        public SubCategoryRepository(ShoppingDbContext shoppingDbContext)
        {
            _shoppingDbContext = shoppingDbContext;
        }
        public IEnumerable<SubCategory> AllSubCategories => _shoppingDbContext.SubCategories.ToList();

        public void CreateSubCategory(SubCategory subcategory)
        {
            try
            {
                _shoppingDbContext.SubCategories.Add(subcategory);
                _shoppingDbContext.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void DeleteSubCategory(SubCategory subcategory)
        {
            _shoppingDbContext.SubCategories.Remove(subcategory);
            _shoppingDbContext.SaveChanges();
        }

        public void EditSubCategory(SubCategory subcategory)
        {
            try
            {
                _shoppingDbContext.SubCategories.Update(subcategory);
                _shoppingDbContext.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public SubCategory? GetSubCategoryById(int id)
        {
            SubCategory? subcategory = _shoppingDbContext.SubCategories
                .Include(s => s.Category)
                .FirstOrDefault(s => s.Id == id);
            return subcategory;
        }
    }
}
