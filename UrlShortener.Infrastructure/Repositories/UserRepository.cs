using Microsoft.EntityFrameworkCore;
using UrlShortener.Core.Application.Interfaces;
using UrlShortener.Core.Domain.Entities;
using UrlShortener.Infrastructure;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _db;

    public UserRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<User?> GetByLoginAsync(string login)
    {
        return await _db.Users.FirstOrDefaultAsync(x => x.Login == login);
    }
}