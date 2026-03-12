using ECommerceApi.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApi.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var (success, token, message) = await _authService.RegisterAsync(request.Email, request.Password);
        
        if (!success)
        {
            return BadRequest(new { message });
        }

        return Ok(new { token, message });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var (success, token, message) = await _authService.LoginAsync(request.Email, request.Password);
        
        if (!success)
        {
            return Unauthorized(new { message });
        }

        return Ok(new { token, message });
    }
}

public record RegisterRequest(string Email, string Password);
public record LoginRequest(string Email, string Password);
