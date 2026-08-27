namespace RestaurantOrderSystem.Models
{

    public enum OrderStatus
    {
        Open,
        InPrepation,
        Served,
        Paid
    }
    public class Order
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public OrderStatus Status { get; set; } = OrderStatus.Open;

        public int TableId { get; set; }
        public Table? Table { get; set; }

        public List<OrderItem> OrderItems { get; set; } = new();


    }
}
