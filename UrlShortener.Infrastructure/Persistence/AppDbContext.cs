using Microsoft.EntityFrameworkCore;
using UrlShortener.Core.Domain.Entities;

namespace UrlShortener.Infrastructure;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<ShortUrl> ShortUrls { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }
}