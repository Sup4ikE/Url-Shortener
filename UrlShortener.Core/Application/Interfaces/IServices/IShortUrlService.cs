using UrlShortener.Core.Application.DTOs;
using UrlShortener.Core.Domain.Entities;

namespace UrlShortener.Core.Application.Interfaces;

public interface IShortUrlService
{
    Task<ShortenResponse> CreateAsync(string originalUrl, int userId);
    Task<List<ShortUrlDto>> GetAllAsync();
    Task<ShortUrlDetailsDto?> GetByCodeAsync(string code);
    Task<string?> ResolveAsync(string code);
    Task<bool> DeleteAsync(string code, int userId, string role);
}