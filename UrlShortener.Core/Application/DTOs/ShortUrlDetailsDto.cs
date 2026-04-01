namespace UrlShortener.Core.Application.DTOs;

public class ShortUrlDetailsDto
{
    public string ShortCode { get; set; }
    public string OriginalUrl { get; set; }
    public DateTime CreatedDate { get; set; }
    public string CreatedByLogin { get; set; }
}