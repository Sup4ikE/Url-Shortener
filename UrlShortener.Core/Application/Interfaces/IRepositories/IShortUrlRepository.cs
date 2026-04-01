using UrlShortener.Core.Domain.Entities;

public interface IShortUrlRepository
{
    Task AddAsync(ShortUrl entity);
    Task SaveChangesAsync();

    Task<ShortUrl?> GetByCodeAsync(string code);
    Task<ShortUrl?> GetWithUserAsync(string code);

    Task<bool> ExistsByCodeAsync(string code);
    Task<bool> ExistsByOriginalUrlAsync(string url);

    Task<List<ShortUrl>> GetAllAsync();

    Task DeleteAsync(ShortUrl entity);
}