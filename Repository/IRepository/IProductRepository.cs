using SistemasWeb01.Models;
using SistemasWeb01.ViewModels;

namespace SistemasWeb01.Repository.IRepository
{
    public interface IProductRepository
    {

        IEnumerable<Product> AllProducts { get; }
        //IEnumerable<Product> ActiveProducts { get; }
        //IEnumerable<Product> NewProducts { get; }
        //Product? GetProductById(int productoId);
        //ProductViewModel? GetProduct(int id);
        //public Task<int> CreateProduct(ProductViewModel productovm);
        //void EditProduct(ProductViewModel productovm);
        //void DeleteProduct(Product producto);
        //void DeleteProductVm(ProductViewModel productovm);
    }
}
