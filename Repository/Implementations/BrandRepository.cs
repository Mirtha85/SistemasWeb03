using SistemasWeb01.DataAccess;
using SistemasWeb01.Models;
using SistemasWeb01.Repository.Interfaces;
using SistemasWeb01.ViewModels;

namespace SistemasWeb01.Repository.Implementations
{
    public class BrandRepository : IBrandRepository
    {
        private readonly ShoppingDbContext _shoppingDbContext;
        public BrandRepository(ShoppingDbContext shoppingDbContext)
        {
            _shoppingDbContext = shoppingDbContext;
        }
        public IEnumerable<Brand> AllBrands => _shoppingDbContext.Brands.ToList();

        public void CreateBrand(BrandViewModel brandViewModel)
        {
            try
            {
                Brand brand = new()
                {
                    Name = brandViewModel.Name,
                    ThumbnailImage = brandViewModel.ThumbnailImage
                };
                _shoppingDbContext.Brands.Add(brand);
                _shoppingDbContext.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void DeleteBrand(Brand brand)
        {
            _shoppingDbContext.Brands.Remove(brand);
            _shoppingDbContext.SaveChanges();
        }

        public void EditBrand(BrandViewModel brandViewModel)
        {
            try
            {
                Brand brand = new()
                {
                    Id = brandViewModel.Id,
                    Name = brandViewModel.Name,
                    ThumbnailImage = brandViewModel.ThumbnailImage
                };
                _shoppingDbContext.Brands.Update(brand);
                _shoppingDbContext.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Brand? GetBrandById(int id)
        {
            return _shoppingDbContext.Brands.FirstOrDefault(b => b.Id == id);
        }
    }
}
