using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantOrderSystem.Models;

namespace RestaurantOrderSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrdersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Order> GetOrders()
        {
            return _context.Orders
                .Include(x => x.Table)
                .Include(x => x.OrderItems)
                    .ThenInclude(xx => xx.MenuItem)
                .ToList();
        }

        [HttpGet("{id}")]
        public Order? GetOrders(int id)
        {
            return _context.Orders
                .Include(x => x.Table)
                .Include(x => x.OrderItems)
                    .ThenInclude(xx => xx.MenuItem)
                .FirstOrDefault(x => x.Id == id);
        }


        [HttpPost]
        public Order CreateOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();

            return order;
        }

        [HttpPut("{id}")]
        public void UpdateOrder(int id, Order order)
        {
            order.Id = id;
            _context.Entry(order).State = EntityState.Modified;
            _context.SaveChanges();
        }

        [HttpDelete("{id}")]
        public void DeleteOrder(int id)
        {
            var order = _context.Orders.Find(id);
            if(order != null)
            {
                _context.Orders.Remove(order);
                _context.SaveChanges();
            }
        }

    }
}

