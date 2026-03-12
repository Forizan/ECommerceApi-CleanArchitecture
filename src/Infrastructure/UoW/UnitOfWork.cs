using ECommerceApi.Domain.Entities;
using ECommerceApi.Application.Interfaces;
using ECommerceApi.Infrastructure.Persistence;
using ECommerceApi.Infrastructure.Repositories;

namespace ECommerceApi.Infrastructure.UoW;

public class UnitOfWork : IUnitOfWork
{
    private readonly ECommerceDbContext _context;

    public IProductRepository Products { get; }
    public IRepository<Category> Categories { get; }
    public IRepository<User> Users { get; }
    public ICartRepository Carts { get; }
    public IOrderRepository Orders { get; }

    public UnitOfWork(ECommerceDbContext context)
    {
        _context = context;

        Products = new ProductRepository(context);
        Categories = new Repository<Category>(_context);
        Users = new Repository<User>(_context);
        Carts = new CartRepository(_context);
        Orders = new OrderRepository(_context);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
