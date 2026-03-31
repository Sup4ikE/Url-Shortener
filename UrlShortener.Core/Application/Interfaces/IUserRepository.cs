using UrlShortener.Core.Domain.Entities;

namespace UrlShortener.Core.Application.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByLoginAsync(string login);
}