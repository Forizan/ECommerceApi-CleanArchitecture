using ECommerceApi.Domain.Entities;
using ECommerceApi.Application.Interfaces;

namespace ECommerceApi.Application.Services;

public class UserService
{
    private readonly IUnitOfWork _uow;

    public UserService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<User> CreateAsync(User user)
    {
        await _uow.Users.AddAsync(user);
        await _uow.SaveChangesAsync();
        return user;
    }
}
