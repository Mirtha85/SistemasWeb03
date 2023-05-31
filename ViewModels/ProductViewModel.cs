using SistemasWeb01.Models;

namespace SistemasWeb01.ViewModels
{
    public class ProductViewModel
    {
        public IEnumerable<Product> Products { get; }

        //public IEnumerable<Product> ActiveProducts { get; }

        public ProductViewModel(IEnumerable<Product> products)
        {
            Products = products;
        }

    }
}
