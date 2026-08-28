using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantOrderSystem.Models;

namespace RestaurantOrderSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }

        [HttpGet("{id}")]
        public Category? GetAllCategorie(int id)
        {
            return _context.Categories.FirstOrDefault(c => c.Id == id);
        }

        [HttpPost]
        public Category CreateCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();

            return category;
        }

        [HttpPut("{id}")]
        public void UpdateCategory(int id, Category category)
        {
            category.Id = id;
            _context.Entry(category).State = EntityState.Modified;
            _context.SaveChanges();
        }

        [HttpDelete("{id}")]
        public void DeleteCategory(int id)
        {
            var category = _context.Categories.Find(id);
            if(category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
        }
    }
}
