using SistemasWeb01.Models;
using System.IO.Pipelines;

namespace SistemasWeb01.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Product> ProductsBestSellers { get; }
        public IEnumerable<Category> Categories { get; }

        public int Quantity { get; set; }

        public HomeViewModel(IEnumerable<Product> productsBestSellers, IEnumerable<Category> categories) { 

            ProductsBestSellers = productsBestSellers;
            Categories = categories;
        }
    }
}
