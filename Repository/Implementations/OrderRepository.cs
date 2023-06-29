using Microsoft.EntityFrameworkCore;
using SistemasWeb01.DataAccess;
using SistemasWeb01.Enums;
using SistemasWeb01.Models;
using SistemasWeb01.Repository.Interfaces;
using SistemasWeb01.ViewModels;

namespace SistemasWeb01.Repository.Implementations
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ShoppingDbContext _shoppingDbContext;
        public OrderRepository(ShoppingDbContext shoppingDbContext)
        {
            _shoppingDbContext = shoppingDbContext;

        }

        public IEnumerable<Order> AllOrders => _shoppingDbContext.Orders
            .Include(o => o.User)
            .Include(o => o.OderDetails)
            .ToList();

        public void CreateOrder(Order order)
        {
            try
            {
                _shoppingDbContext.Orders.Add(order);
                _shoppingDbContext.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void DeleteOrder(Order order)
        {
            _shoppingDbContext.Orders.Remove(order);
            _shoppingDbContext.SaveChanges();
        }

        public void EditOrder(Order order)
        {
            _shoppingDbContext.Orders.Update(order);
            _shoppingDbContext.SaveChanges();
        }

        public Order? GetOrderById(int id)
        {
            return _shoppingDbContext.Orders
                .Include(o => o.OderDetails)
                .Include(o => o.User)
                .FirstOrDefault(p => p.Id == id);
        }

        //public void ProcessOrderAsync(ShowCartViewModel model)
        //{
        //    Order order = new()
        //    {
        //        Date = DateTime.UtcNow,
        //        User = model.User,
        //        Remarks = model.Remarks,
        //        OderDetails = new List<OrderDetail>(),
        //        OrderStatus = OrderStatus.Nuevo
        //    };

        //    foreach (TemporalCartItem item in model.TemporalCartItems)
        //    {
        //        order.OderDetails.Add(new OrderDetail
        //        {
        //            Product = item.Product,
        //            Quantity = item.Quantity,
        //            Remarks = item.Remarks,
        //        });

        //        Product? product =  _productRepository.GetProductById(item.Product.Id);
        //        if (product != null)
        //        {
        //            product.InStock -= item.Quantity;
        //            _productRepository.EditProduct(product);
        //        }
        //        _temporalCartItemRepository.DeleteTemporalCartItem(item);

        //    }

        //    _shoppingDbContext.Orders.Add(order);
        //    _shoppingDbContext.SaveChanges();
        //}

        //private async Task<bool> CheckInventoryAsync(ShowCartViewModel model)
        //{
        //    bool response = true;
        //    foreach (TemporalCartItem item in model.TemporalCartItems)
        //    {
        //        Product? product = await _shoppingDbContext.Products.FindAsync(item.Product.Id);
        //        if (product == null)
        //        {
        //            response = false;
        //            response.Message = $"El producto {item.Product.Name}, ya no está disponible";
        //            return response;
        //        }
        //        if (product.Stock < item.Quantity)
        //        {
        //            response.IsSuccess = false;
        //            response.Message = $"Lo sentimos no tenemos existencias suficientes del producto {item.Product.Name}, para tomar su pedido. Por favor disminuir la cantidad o sustituirlo por otro.";
        //            return response;
        //        }
        //    }
        //    return response;
        //}
    } 
}


