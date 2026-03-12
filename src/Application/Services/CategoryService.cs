using ECommerceApi.Domain.Entities;
using ECommerceApi.Application.Interfaces;

namespace ECommerceApi.Application.Services;

public class CategoryService
{
    private readonly IUnitOfWork _uow;

    public CategoryService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<List<Category>> GetAllAsync()
    {
        return await _uow.Categories.GetAllAsync();
    }

    public async Task<Category> CreateAsync(Category category)
    {
        await _uow.Categories.AddAsync(category);
        await _uow.SaveChangesAsync();
        return category;
    }
}
