using SistemasWeb01.Models;

namespace SistemasWeb01.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Pie> PiesOfTheWeek { get; }
        public IEnumerable<Product> Products { get; }
        public HomeViewModel(IEnumerable<Pie> piesOfTheWeek, IEnumerable<Product> products)
        {
            PiesOfTheWeek = piesOfTheWeek;
            Products = products;

        }
    }
}
