using ECommerceApi.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceApi.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ProductService>();
        services.AddScoped<CartService>();
        services.AddScoped<CategoryService>();
        services.AddScoped<UserService>();
        services.AddScoped<OrderService>();
        services.AddScoped<AuthService>();

        return services;
    }
}
