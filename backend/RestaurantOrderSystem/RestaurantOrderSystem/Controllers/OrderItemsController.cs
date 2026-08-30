using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantOrderSystem.Models;

namespace RestaurantOrderSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrderItemsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<OrderItem> GetOrderItems()
        {
            return _context.OrderItems
                .Include(x => x.MenuItem)
                .ToList();
        }

        [HttpGet("{id}")]
        public OrderItem? GetOrderItem(int id)
        {
            return _context.OrderItems
                .Include(x => x.MenuItem)
                .FirstOrDefault(x => x.Id == id);
        }

        [HttpPost]
        public OrderItem CreateOrderItem(OrderItem orderItem)
        {
            _context.OrderItems.Add(orderItem);
            _context.SaveChanges();

            return orderItem;
        }

        [HttpPut("{id}")]
        public void UpdateOrderItem(int id, OrderItem orderItem)
        {
            orderItem.Id = id;
            _context.Entry(orderItem).State = EntityState.Modified;
            _context.SaveChanges();
        }

        [HttpDelete("{id}")]
        public void DeleteOrderItem(int id)
        {
            var orderItem = _context.OrderItems.Find(id);
            if(orderItem != null)
            {
                _context.OrderItems.Remove(orderItem);
                _context.SaveChanges();
            }
        }

    }
}
