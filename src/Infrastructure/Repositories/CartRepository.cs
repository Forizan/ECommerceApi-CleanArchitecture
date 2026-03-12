using ECommerceApi.Domain.Entities;
using ECommerceApi.Application.Interfaces;
using ECommerceApi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApi.Infrastructure.Repositories;

public class CartRepository : Repository<Cart>, ICartRepository
{
    private readonly ECommerceDbContext _context;

    public CartRepository(ECommerceDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Cart?> GetByUserIdAsync(int userId)
    {
        return await _context.Carts
            .Include(c => c.Items)
            .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(c => c.UserId == userId);
    }
}
