using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemasWeb01.DataAccess;
using SistemasWeb01.Enums;
using SistemasWeb01.Models;
using SistemasWeb01.Repository.Implementations;
using SistemasWeb01.Repository.Interfaces;
using System.Data;
using Vereyon.Web;

namespace SistemasWeb01.Controllers
{
    

    public class OrderController : Controller
    {
        private readonly ShoppingDbContext _shoppingDbContext;
        private readonly IOrderRepository _orderRepository;
        private readonly IFlashMessage _flashMessage;
        private readonly IProductRepository _productRepository;
        public OrderController(IOrderRepository orderRepository, IFlashMessage flashMessage, IProductRepository productRepository, ShoppingDbContext shoppingDbContext)
        {
            _orderRepository = orderRepository;
            _flashMessage = flashMessage;
            _productRepository = productRepository;
            _shoppingDbContext = shoppingDbContext;
        }


        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            IEnumerable<Order> orders = _orderRepository.AllOrders;
            return View(orders);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Details(int id)
        {
            Order? order = _orderRepository.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Dispatch(int id)
        {

            Order order = _orderRepository.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }

            if (order.OrderStatus != OrderStatus.Nuevo)
            {
                _flashMessage.Danger("Solo se pueden despachar pedidos que estén en estado 'nuevo'.");
            }
            else
            {
                order.OrderStatus = OrderStatus.Despachado;
                _orderRepository.EditOrder(order);
                _flashMessage.Confirmation("El estado del pedido ha sido cambiado a 'despachado'.");
            }

            return RedirectToAction(nameof(Details), new { Id = order.Id });
        }

        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Send(int id)
        {

            Order? order = _orderRepository.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }

            if (order.OrderStatus != OrderStatus.Despachado)
            {
                _flashMessage.Danger("Solo se pueden enviar pedidos que estén en estado 'despachado'.");
            }
            else
            {
                order.OrderStatus = OrderStatus.Enviado;
                _orderRepository.EditOrder(order);
                _flashMessage.Confirmation("El estado del pedido ha sido cambiado a 'enviado'.");
            }

            return RedirectToAction(nameof(Details), new { Id = order.Id });
        }


        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Confirm(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Order? order = _orderRepository.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }

            if (order.OrderStatus != OrderStatus.Enviado)
            {
                _flashMessage.Danger("Solo se pueden confirmar pedidos que estén en estado 'enviado'.");
            }
            else
            {
                order.OrderStatus = OrderStatus.Confirmado;
                _orderRepository.EditOrder(order);
                _flashMessage.Confirmation("El estado del pedido ha sido cambiado a 'confirmado'.");
            }

            return RedirectToAction(nameof(Details), new { Id = order.Id });
        }


        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Cancel(int id)
        {
            Order? order = _orderRepository.OrderById(id);
            if (order == null)
            {
                return NotFound();
            }

            if (order.OrderStatus == OrderStatus.Cancelado)
            {
                _flashMessage.Danger("No se puede cancelar un pedido que esté en estado 'cancelado'.");
            }
            else
            {
                CancelOrderAsync(order.Id);
                _flashMessage.Confirmation("El estado del pedido ha sido cambiado a 'cancelado'.");
            }

            return RedirectToAction(nameof(Details), new { Id = order.Id });
        }

        [Authorize(Roles = "Admin")]
        public bool CancelOrderAsync(int id)
        {
            Order? order = _orderRepository.OrderById(id);
           
            foreach (OrderDetail orderDetail in order.OderDetails)
            {
                Product? product = _productRepository.GetProductById(orderDetail.Product.Id);
                if (product != null)
                {
                    product.InStock += orderDetail.Quantity;
                }
            }

            order.OrderStatus = OrderStatus.Cancelado;
            _orderRepository.EditOrder(order);
            return true;
        }


        [Authorize(Roles = "User")]
        public async Task<IActionResult> MyOrders()
        {
            return View(await _shoppingDbContext.Orders
               .Include(s => s.User)
               .Include(s => s.OderDetails)
               .ThenInclude(sd => sd.Product)
               .Where(s => s.User.UserName == User.Identity.Name)
               .ToListAsync());
        }


        [Authorize(Roles = "User")]
        public async Task<IActionResult> MyDetails(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Order? order = _orderRepository.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }



    }

}
