using ECommerceApi.Domain.Entities;
using ECommerceApi.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ECommerceApi.Application.Services;

public class AuthService
{
    private readonly IUnitOfWork _uow;
    private readonly IConfiguration _configuration;

    public AuthService(IUnitOfWork uow, IConfiguration configuration)
    {
        _uow = uow;
        _configuration = configuration;
    }

    public async Task<(bool Success, string Token, string Message)> RegisterAsync(string email, string password)
    {
        // Validate input
        var validator = new Validators.RegisterRequestValidator();
        var validationResult = await validator.ValidateAsync((email, password));
        if (!validationResult.IsValid)
        {
            var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
            return (false, string.Empty, errors);
        }

        var existingUser = await _uow.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (existingUser != null)
        {
            return (false, string.Empty, "User already exists");
        }

        var user = new User
        {
            Email = email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
            Role = "Customer"
        };

        await _uow.Users.AddAsync(user);
        await _uow.SaveChangesAsync();

        var token = GenerateJwtToken(user);
        return (true, token, "Registration successful");
    }

    public async Task<(bool Success, string Token, string Message)> LoginAsync(string email, string password)
    {
        var user = await _uow.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
        {
            return (false, string.Empty, "Invalid email or password");
        }

        var token = GenerateJwtToken(user);
        return (true, token, "Login successful");
    }

    private string GenerateJwtToken(User user)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var secretKey = jwtSettings["SecretKey"] ?? throw new ArgumentException("JWT SecretKey not configured");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(24),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
