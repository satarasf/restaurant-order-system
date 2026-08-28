using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantOrderSystem.Models;

namespace RestaurantOrderSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MenuItemsController(AppDbContext context) 
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<MenuItem> GetMenuItems()
        {
            return _context.MenuItems
                .Include(x => x.Category)
                .ToList();
        }


        // api/menuItems/2
        [HttpGet("id")]
        public MenuItem? GetMenuItem(int id)
        {
            return _context.MenuItems
                .Include(x => x.Category)
                .FirstOrDefault(x => x.Id == id);
        }


        // api/menuItems
        [HttpPost]
        public MenuItem CreateMenuItem(MenuItem menuItem)
        {
            _context.MenuItems.Add(menuItem);
            _context.SaveChanges();

            return menuItem;
        }


        // api/menuItems/4
        [HttpPut("{id}")]
        public void UpdateMenuItem(int id, MenuItem menuItem)
        {
            menuItem.Id = id;
            _context.Entry(menuItem).State = EntityState.Modified;
            _context.SaveChanges();

        }

        [HttpDelete("{id}")]
        public void DeleteMenuItem(int id)
        {
            var menuItem = _context.MenuItems.Find(id);
            if(menuItem != null)
            {
                _context.MenuItems.Remove(menuItem);
                _context.SaveChanges();
            }
        }
    }
}
