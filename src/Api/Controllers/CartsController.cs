using ECommerceApi.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApi.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartsController : ControllerBase
{
    private readonly CartService _cartService;

    public CartsController(CartService cartService)
    {
        _cartService = cartService;
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetCart(int userId)
    {
        var cart = await _cartService.GetCartByUserIdAsync(userId);
        return Ok(cart);
    }

    [HttpPost("{userId}/items")]
    public async Task<IActionResult> AddItem(int userId, [FromBody] AddToCartRequest request)
    {
        try
        {
            var cart = await _cartService.AddItemToCartAsync(userId, request.ProductId, request.Quantity);
            return Ok(cart);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{userId}/items/{productId}")]
    public async Task<IActionResult> RemoveItem(int userId, int productId)
    {
        var cart = await _cartService.RemoveItemFromCartAsync(userId, productId);
        return Ok(cart);
    }

    [HttpDelete("{userId}")]
    public async Task<IActionResult> ClearCart(int userId)
    {
        var result = await _cartService.ClearCartAsync(userId);
        if (!result) return NotFound();
        return NoContent();
    }
}

public class AddToCartRequest
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}
