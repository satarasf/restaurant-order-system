namespace RestaurantOrderSystem.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public List<MenuItem> MenuItems { get; set; } = new();

    }
}
