using ECommerceApi.Domain.Entities;

namespace ECommerceApi.Application.Interfaces;

public interface IOrderRepository : IRepository<Order>
{
    Task<List<Order>> GetByUserIdAsync(int userId);
}
