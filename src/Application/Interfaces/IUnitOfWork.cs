using ECommerceApi.Domain.Entities;

namespace ECommerceApi.Application.Interfaces;

public interface IUnitOfWork
{
    IProductRepository Products { get; }
    IRepository<Category> Categories { get; }
    IRepository<User> Users { get; }
    ICartRepository Carts { get; }
    IOrderRepository Orders { get; }

    Task<int> SaveChangesAsync();
}
