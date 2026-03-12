using ECommerceApi.Domain.Entities;
using ECommerceApi.Application.Interfaces;

namespace ECommerceApi.Application.Services;

public class OrderService
{
    private readonly IUnitOfWork _uow;

    public OrderService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Order> CreateOrderAsync(int userId)
    {
        var cart = await _uow.Carts.GetByUserIdAsync(userId);
        if (cart == null || !cart.Items.Any())
        {
            throw new ArgumentException($"Cart is empty for user {userId}");
        }

        var order = new Order
        {
            UserId = userId,
            Status = "Pending",
            CreatedAt = DateTime.UtcNow,
            Items = cart.Items.Select(i => new OrderItem
            {
                ProductId = i.ProductId,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice
            }).ToList()
        };

        await _uow.Orders.AddAsync(order);
        
        // Clear the cart after order creation
        cart.Items.Clear();
        
        await _uow.SaveChangesAsync();

        return order;
    }

    public async Task<List<Order>> GetOrdersByUserIdAsync(int userId)
    {
        return await _uow.Orders.GetByUserIdAsync(userId);
    }
}
