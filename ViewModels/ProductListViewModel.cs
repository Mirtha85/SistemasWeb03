using SistemasWeb01.Models;
using System.IO.Pipelines;

namespace SistemasWeb01.ViewModels
{
    public class ProductListViewModel
    {
        public IEnumerable<Product> Products { get; }
        public string? CurrentCategory { get; }
        public ProductListViewModel(IEnumerable<Product> products, string? currentCategory)
        {
            Products = products;
            CurrentCategory = currentCategory;
        }

    }
}
