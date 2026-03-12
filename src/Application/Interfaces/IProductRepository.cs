using ECommerceApi.Domain.Entities;

namespace ECommerceApi.Application.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    Task<List<Product>> GetProductsWithCategoryAsync();
}
