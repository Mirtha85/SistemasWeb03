using SistemasWeb01.Models;

namespace SistemasWeb01.Repository.IRepository
{
    public interface IProductImageRepository
    {
        IEnumerable<ProductImage> AllProductImages { get; }
    }
}
