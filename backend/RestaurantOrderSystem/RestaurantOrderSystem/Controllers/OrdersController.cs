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
        public IEnumerable<Order> GetOrder()
        {
            return _context.Orders
                .Include(x => x.Table)
                .Include(x => x.OrderItems)
                    .ThenInclude(xx => xx.MenuItem)
                .ToList();
            }
        }
    }

