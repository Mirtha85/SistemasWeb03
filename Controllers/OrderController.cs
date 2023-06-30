using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemasWeb01.Models;
using SistemasWeb01.Repository.Implementations;
using SistemasWeb01.Repository.Interfaces;
using System.Data;

namespace SistemasWeb01.Controllers
{
    [Authorize(Roles = "Admin")]

    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public IActionResult Index()
        {
            IEnumerable<Order> orders = _orderRepository.AllOrders;
            return View(orders);
        }

        public IActionResult Details(int id)
        {
            Order? order = _orderRepository.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }
    }

    

}
