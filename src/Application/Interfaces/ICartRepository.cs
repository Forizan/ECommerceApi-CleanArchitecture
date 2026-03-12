using ECommerceApi.Domain.Entities;

namespace ECommerceApi.Application.Interfaces;

public interface ICartRepository : IRepository<Cart>
{
    Task<Cart?> GetByUserIdAsync(int userId);
}
