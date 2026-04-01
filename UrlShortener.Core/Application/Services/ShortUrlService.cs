using UrlShortener.Core.Application.DTOs;
using UrlShortener.Core.Application.Interfaces;
using UrlShortener.Core.Domain.Entities;

public class ShortUrlService : IShortUrlService
{
    private readonly IShortUrlRepository _repo;
    private readonly ICodeGenerator _generator;

    public ShortUrlService(IShortUrlRepository repo, ICodeGenerator generator)
    {
        _repo = repo;
        _generator = generator;
    }

    public async Task<ShortenResponse> CreateAsync(string originalUrl, int userId)
    {
        if (string.IsNullOrWhiteSpace(originalUrl) ||
            !Uri.TryCreate(originalUrl, UriKind.Absolute, out _))
        {
            throw new ArgumentException("Invalid URL");
        }
        
        if (await _repo.ExistsByOriginalUrlAsync(originalUrl))
            throw new Exception("URL already exists");

        string code;

        do
        {
            code = _generator.Generate();
        }
        while (await _repo.ExistsByCodeAsync(code));

        var entity = new ShortUrl
        {
            ShortCode = code,
            OriginalUrl = originalUrl,
            CreatedById = userId,
            CreatedDate = DateTime.UtcNow
        };

        await _repo.AddAsync(entity);
        await _repo.SaveChangesAsync();

        return new ShortenResponse
        {
            ShortCode = code,
            ShortUrl = $"http://localhost:5005/{code}"
        };
    }

    public async Task<List<ShortUrlDto>> GetAllAsync()
    {
        var urls = await _repo.GetAllAsync();

        return urls.Select(x => new ShortUrlDto
        {
            ShortCode = x.ShortCode,
            OriginalUrl = x.OriginalUrl,
            CreatedDate = x.CreatedDate,
            CreatedById = x.CreatedById
        }).ToList();
    }

    public async Task<ShortUrlDetailsDto?> GetByCodeAsync(string code)
    {
        var entity = await _repo.GetWithUserAsync(code);

        if (entity == null) return null;

        return new ShortUrlDetailsDto
        {
            ShortCode = entity.ShortCode,
            OriginalUrl = entity.OriginalUrl,
            CreatedDate = entity.CreatedDate,
            CreatedByLogin = entity.CreatedBy.Login
        };
    }

    public async Task<string?> ResolveAsync(string code)
    {
        var entity = await _repo.GetByCodeAsync(code);
        return entity?.OriginalUrl;
    }

    public async Task<bool> DeleteAsync(string code, int userId, string role)
    {
        var entity = await _repo.GetByCodeAsync(code);

        if (entity == null) return false;

        if (role == "Admin" || entity.CreatedById == userId)
        {
            await _repo.DeleteAsync(entity);
            await _repo.SaveChangesAsync();
            return true;
        }

        return false;
    }
}