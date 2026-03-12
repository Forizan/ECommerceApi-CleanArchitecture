using System.Text.Json.Serialization;

namespace ECommerceApi.Domain.Entities;

public class CartItem
{
    public int Id { get; set; }

    public int CartId { get; set; }
    [JsonIgnore]
    public Cart? Cart { get; set; }

    public int ProductId { get; set; }
    public Product? Product { get; set; }

    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
