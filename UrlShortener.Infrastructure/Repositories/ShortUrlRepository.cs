using Microsoft.EntityFrameworkCore;
using UrlShortener.Core.Domain.Entities;

namespace UrlShortener.Infrastructure.Repositories;

public class ShortUrlRepository : IShortUrlRepository
{
    private readonly AppDbContext _db;

    public ShortUrlRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task AddAsync(ShortUrl entity)
    {
        await _db.ShortUrls.AddAsync(entity);
    }

    public async Task<ShortUrl?> GetByCodeAsync(string code)
    {
        return await _db.ShortUrls
            .FirstOrDefaultAsync(x => x.ShortCode == code);
    }

    public async Task<ShortUrl?> GetWithUserAsync(string code)
    {
        return await _db.ShortUrls
            .Include(x => x.CreatedBy)
            .FirstOrDefaultAsync(x => x.ShortCode == code);
    }

    public Task<bool> ExistsByCodeAsync(string code)
    {
        return _db.ShortUrls.AnyAsync(x => x.ShortCode == code);
    }

    public Task<bool> ExistsByOriginalUrlAsync(string url)
    {
        return _db.ShortUrls.AnyAsync(x => x.OriginalUrl == url);
    }

    public async Task<List<ShortUrl>> GetAllAsync()
    {
        return await _db.ShortUrls
            .AsNoTracking()
            .OrderByDescending(x => x.CreatedDate)
            .ToListAsync();
    }

    public Task DeleteAsync(ShortUrl entity)
    {
        _db.ShortUrls.Remove(entity);
        return Task.CompletedTask;
    }
    
    public async Task SaveChangesAsync()
    {
        await _db.SaveChangesAsync();
    }
}