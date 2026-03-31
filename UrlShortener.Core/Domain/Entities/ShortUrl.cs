namespace UrlShortener.Core.Domain.Entities;

public class ShortUrl
{
    public int Id { get; set; }
    public string OriginalUrl { get; set; }
    public string ShortCode { get; set; }
    public int CreatedById { get; set; }
    public DateTime CreatedDate { get; set; }

    public User CreatedBy { get; set; }
}