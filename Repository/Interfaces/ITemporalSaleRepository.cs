using SistemasWeb01.Models;

namespace SistemasWeb01.Repository.Interfaces
{
    public interface ITemporalSaleRepository
    {
        //IEnumerable<TemporalSale> AllTemporalSales { get; }
        TemporalSale? GetTemporalSaleById(int id);
        IEnumerable<TemporalSale> GetTemporalSalesByUserId(string userId);

        void CreateTempalSale(TemporalSale temporalSale);
        void EditTemporalSale(TemporalSale temporalSale);
        void DeleteTemporalSale(TemporalSale temporalSale);
    }
}
