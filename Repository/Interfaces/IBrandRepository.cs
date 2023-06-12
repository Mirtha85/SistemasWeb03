using SistemasWeb01.Models;
using SistemasWeb01.ViewModels;

namespace SistemasWeb01.Repository.Interfaces
{
    public interface IBrandRepository
    {
        IEnumerable<Brand> AllBrand { get; }
        Brand? GetBrandById(int id);

        void CreateBrand(Brand brand);

        void EditBrand(Brand brand);

        void DeleteBrand(Brand brand);
    }
}
