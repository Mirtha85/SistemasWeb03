using SistemasWeb01.Models;

namespace SistemasWeb01.Repository.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product> AllProducts { get; }
        IEnumerable<Product> ProductsNotDeleted { get; }
        IEnumerable<Product> BestSellingProducts { get; }
        IEnumerable<Product> NewProducts { get; }
        Product? GetProductById(int id);
        void CreateProduct(Product product);

        void EditProduct(Product product);

        void DeleteProduct(Product product);
        void DisableProduct(Product product);
    }
}
