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
        private readonly IProductRepository _productRepository;
        private readonly ITemporalCartItemRepository _temporalCartItemRepository;
        public OrderRepository(ShoppingDbContext shoppingDbContext, IProductRepository productRepository, ITemporalCartItemRepository temporalCartItemRepository)
        {
            _shoppingDbContext = shoppingDbContext;
            _productRepository = productRepository;
            _temporalCartItemRepository = temporalCartItemRepository;

        }

        public void ProcessOrderAsync(ShowCartViewModel model)
        {
            Order order = new()
            {
                Date = DateTime.UtcNow,
                User = model.User,
                Remarks = model.Remarks,
                OderDetails = new List<OrderDetail>(),
                OrderStatus = OrderStatus.Nuevo
            };

            foreach (TemporalCartItem item in model.TemporalCartItems)
            {
                order.OderDetails.Add(new OrderDetail
                {
                    Product = item.Product,
                    Quantity = item.Quantity,
                    Remarks = item.Remarks,
                });

                Product? product =  _productRepository.GetProductById(item.Product.Id);
                if (product != null)
                {
                    product.InStock -= item.Quantity;
                    _productRepository.EditProduct(product);
                }
                _temporalCartItemRepository.DeleteTemporalCartItem(item);

            }

            _shoppingDbContext.Orders.Add(order);
            _shoppingDbContext.SaveChanges();

        }
    } 
}


