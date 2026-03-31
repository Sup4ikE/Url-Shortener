using Microsoft.EntityFrameworkCore;
using UrlShortener.Core.Domain.Entities;

namespace UrlShortener.Infrastructure;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<ShortUrl> ShortUrls { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                Login = "admin",
                PasswordHash = "admin",
                Role = "Admin"
            },
            new User
            {
                Id = 2,
                Login = "user",
                PasswordHash = "user",
                Role = "User"
            }
        );
    }
}