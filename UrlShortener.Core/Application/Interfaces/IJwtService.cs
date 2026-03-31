using UrlShortener.Core.Domain.Entities;

namespace UrlShortener.Core.Application.Interfaces;

public interface IJwtService
{
    string GenerateToken(User user);
}