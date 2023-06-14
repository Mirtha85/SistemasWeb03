using SistemasWeb01.Models;
using SistemasWeb01.ViewModels;

namespace SistemasWeb01.Repository.Interfaces
{
    public interface IBrandRepository
    {
        IEnumerable<Brand> AllBrands { get; }
        Brand? GetBrandById(int id);

        void CreateBrand(BrandViewModel brandViewModel);

        void EditBrand(BrandViewModel brandViewModel);

        void DeleteBrand(Brand brand);
    }
}
