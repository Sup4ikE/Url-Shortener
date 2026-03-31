using UrlShortener.Core.Application.Interfaces;

namespace UrlShortener.Core.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _users;
    private readonly IJwtService _jwt;

    public AuthService(IUserRepository users, IJwtService jwt)
    {
        _users = users;
        _jwt = jwt;
    }

    public async Task<string?> LoginAsync(string login, string password)
    {
        var user = await _users.GetByLoginAsync(login);

        if (user == null || user.PasswordHash != password)
            return null;

        return _jwt.GenerateToken(user);
    }
}