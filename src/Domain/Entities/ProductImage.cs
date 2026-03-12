using System.Text.Json.Serialization;

namespace ECommerceApi.Domain.Entities;

public class ProductImage
{
    public int Id { get; set; }
    public string Url { get; set; } = "";

    public int ProductId { get; set; }
    [JsonIgnore]
    public Product? Product { get; set; }
}
