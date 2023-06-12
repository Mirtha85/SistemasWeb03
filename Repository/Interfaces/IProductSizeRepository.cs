using SistemasWeb01.Models;

namespace SistemasWeb01.Repository.Interfaces
{
    public interface IProductSizeRepository
    {
        IEnumerable<ProductSize> AllProductSizes { get; }
        ProductSize? GetProductSizeById(int id);
        void CreateProductSize(ProductSize productSize);

        void EditProductSize(ProductSize productSize);

        void DeleteProductSiza(ProductSize productSize);
    }
}
