using SistemasWeb01.Models;

namespace SistemasWeb01.Repository.Interfaces
{
    public interface IProductSizeRepository
    {
        IEnumerable<ProductSize> AllProductSizes { get; }
        IEnumerable<ProductSize> GetSizesByProductId(int productId);
        ProductSize? GetProductSizeById(int id);
        void CreateProductSize(ProductSize productSize);

        void EditProductSize(ProductSize productSize);

        void DeleteProductSize(ProductSize productSize);


    }
}
