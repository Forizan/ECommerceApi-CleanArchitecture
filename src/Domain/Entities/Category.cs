using System.Text.Json.Serialization;

namespace ECommerceApi.Domain.Entities;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = "";

    public int? ParentCategoryId { get; set; }
    [JsonIgnore]
    public Category? ParentCategory { get; set; }

    [JsonIgnore]
    public List<Category> SubCategories { get; set; } = new();
    [JsonIgnore]
    public List<Product> Products { get; set; } = new();
}
