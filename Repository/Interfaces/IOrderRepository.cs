using SistemasWeb01.Models;
using SistemasWeb01.ViewModels;

namespace SistemasWeb01.Repository.Interfaces
{
    public interface IOrderRepository
    {
        //Task<bool> CheckInventoryAsync(ShowCartViewModel model);
        void ProcessOrderAsync(ShowCartViewModel model);

    }
}
