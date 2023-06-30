using SistemasWeb01.Models;
using SistemasWeb01.ViewModels;

namespace SistemasWeb01.Repository.Interfaces
{
    public interface IOrderRepository
    {
        IEnumerable<Order> AllOrders { get; }
        Order? GetOrderById(int id);

        Order? OrderById(int id);
        void CreateOrder(Order order);

        void EditOrder(Order order);

        void DeleteOrder(Order order);

    }
}
