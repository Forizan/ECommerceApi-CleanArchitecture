using ECommerceApi.Domain.Entities;
using ECommerceApi.Application.Interfaces;

namespace ECommerceApi.Application.Services;

public class CartService
{
    private readonly IUnitOfWork _uow;

    public CartService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Cart> GetCartByUserIdAsync(int userId)
    {
        var cart = await _uow.Carts.GetByUserIdAsync(userId);
        if (cart == null)
        {
            cart = new Cart { UserId = userId };
            await _uow.Carts.AddAsync(cart);
            await _uow.SaveChangesAsync();
        }
        return cart;
    }

    public async Task<Cart> AddItemToCartAsync(int userId, int productId, int quantity)
    {
        // Validate input
        var validator = new Validators.AddToCartRequestValidator();
        var validationResult = await validator.ValidateAsync((productId, quantity));
        if (!validationResult.IsValid)
        {
            var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
            throw new ArgumentException(errors);
        }

        var cart = await GetCartByUserIdAsync(userId);
        var product = await _uow.Products.GetByIdAsync(productId);

        if (product == null)
        {
            throw new KeyNotFoundException($"Product with ID {productId} not found");
        }

        var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == productId);
        if (existingItem != null)
        {
            existingItem.Quantity += quantity;
        }
        else
        {
            cart.Items.Add(new CartItem
            {
                ProductId = productId,
                Quantity = quantity,
                UnitPrice = product.Price // Assuming Product has Price
            });
        }

        await _uow.SaveChangesAsync();
        return cart;
    }

    public async Task<Cart> RemoveItemFromCartAsync(int userId, int productId)
    {
        var cart = await GetCartByUserIdAsync(userId);
        var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);

        if (item != null)
        {
            cart.Items.Remove(item);
            await _uow.SaveChangesAsync();
        }

        return cart;
    }

    public async Task<bool> ClearCartAsync(int userId)
    {
        var cart = await GetCartByUserIdAsync(userId);
        cart.Items.Clear();
        return await _uow.SaveChangesAsync() > 0;
    }
}
