using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Table = RestaurantOrderSystem.Models.Table;

// Der Compiler "Verwechselt" manchmal welche Table ich nutzen will,
// weil Entity Framework Core auch eine Klasse namens Table hat. Daher der using Table = ...

namespace RestaurantOrderSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TablesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TablesController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IEnumerable<Table> GetTables()
        {
            return _context.Tables.ToList();
        }

        [HttpGet("{id}")]
        public Table? GetTable(int id)
        {
            return _context.Tables.FirstOrDefault(x => x.Id == id);
        }

        [HttpPost]
        public Table CreateTable(Table table)
        {
            _context.Tables.Add(table);
            _context.SaveChanges();

            return table;
        }


        [HttpPut("{id}")]
        public void UpdateTable(int id, Table table)
        {
            table.Id = id;
            _context.Entry(table).State = EntityState.Modified;
            _context.SaveChanges();
        }

        [HttpDelete("{id}")]
        public void DeleteTable(int id)
        {
            var table = _context.Tables.Find(id);
            if(table != null)
            {
                _context.Tables.Remove(table);
                _context.SaveChanges();
            }
        }
    }
}
