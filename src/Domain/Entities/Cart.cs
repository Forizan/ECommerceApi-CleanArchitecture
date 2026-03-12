namespace ECommerceApi.Domain.Entities;

public class Cart
{
    public int Id { get; set; }

    public int UserId { get; set; }
    public User? User { get; set; }

    public List<CartItem> Items { get; set; } = new();

    public decimal TotalPrice => Items.Sum(i => i.UnitPrice * i.Quantity);
}
