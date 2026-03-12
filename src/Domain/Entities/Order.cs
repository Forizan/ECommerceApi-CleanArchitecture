namespace ECommerceApi.Domain.Entities;

public class Order
{
    public int Id { get; set; }

    public int UserId { get; set; }
    public User? User { get; set; }

    public List<OrderItem> Items { get; set; } = new();

    public decimal TotalPrice => Items.Sum(i => i.UnitPrice * i.Quantity);

    public string Status { get; set; } = "Pending"; 
    // Pending, Paid, Shipped, Completed, Cancelled

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
