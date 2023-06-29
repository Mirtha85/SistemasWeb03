using SistemasWeb01.Models;

namespace SistemasWeb01.Repository.Interfaces
{
    public interface ITemporalCartItemRepository
    {
        //IEnumerable<TemporalCartItem> AllTemporalCartItems { get; }
        TemporalCartItem? GetTemporalCartItemById(int id);
        IEnumerable<TemporalCartItem> GetTemporalCartItemsByUserId(string userId);

        void CreateTemporalCartItem(TemporalCartItem temporalCartItem);
        void EditTemporalCartItem(TemporalCartItem temporalCartItem);
        void DeleteTemporalCartItem(TemporalCartItem temporalCartItem);
    }
}
