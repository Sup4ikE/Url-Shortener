namespace UrlShortener.Core.Application.DTOs;

public class ShortUrlDto
{
    public string ShortCode { get; set; }
    public string OriginalUrl { get; set; }
    public DateTime CreatedDate { get; set; }
    public int CreatedById { get; set; }
}