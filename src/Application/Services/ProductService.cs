using ECommerceApi.Domain.Entities;
using ECommerceApi.Application.Interfaces;

namespace ECommerceApi.Application.Services;

public class ProductService
{
    private readonly IUnitOfWork _uow;

    public ProductService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public Task<List<Product>> GetAllAsync()
        => _uow.Products.GetProductsWithCategoryAsync();

    public Task<Product?> GetByIdAsync(int id)
        => _uow.Products.GetByIdAsync(id);

    public async Task<Product> CreateAsync(Product product)
    {
        await _uow.Products.AddAsync(product);
        await _uow.SaveChangesAsync();
        return product;
    }

    public async Task<bool> UpdateAsync(Product product)
    {
        _uow.Products.Update(product);
        return await _uow.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product = await _uow.Products.GetByIdAsync(id);
        if (product == null) return false;

        _uow.Products.Remove(product);
        return await _uow.SaveChangesAsync() > 0;
    }
}
