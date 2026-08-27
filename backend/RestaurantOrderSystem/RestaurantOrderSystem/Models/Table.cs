namespace RestaurantOrderSystem.Models
{
    public class Table
    {
        public int Id { get; set; }
        public int TableNumber { get; set; }
        public int Seats { get; set; }

        public List<Order> Orders { get; set; } = new();



    }
}
