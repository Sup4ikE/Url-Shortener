using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UrlShortener.Core.Application.DTOs;
using UrlShortener.Core.Application.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class ShortUrlController : ControllerBase
{
    private readonly IShortUrlService _service;

    public ShortUrlController(IShortUrlService service)
    {
        _service = service;
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ShortenRequest request)
    {
        var userIdClaim = User.FindFirst("userId");
        if (userIdClaim == null) return Unauthorized();

        int userId = int.Parse(userIdClaim.Value);

        try
        {
            var result = await _service.CreateAsync(request.Url, userId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        } 
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllAsync();
        return Ok(result);
    }

    [Authorize]
    [HttpGet("{code}")]
    public async Task<IActionResult> GetByCode(string code)
    {
        var result = await _service.GetByCodeAsync(code);

        if (result == null) return NotFound();

        return Ok(result);
    }

    [HttpGet("r/{code}")]
    public async Task<IActionResult> RedirectToOriginal(string code)
    {
        var url = await _service.ResolveAsync(code);

        if (url == null) return NotFound();

        return Redirect(url);
    }

    [Authorize]
    [HttpDelete("{code}")]
    public async Task<IActionResult> Delete(string code)
    {
        int userId = int.Parse(User.FindFirst("userId")!.Value);
        string role = User.FindFirst(ClaimTypes.Role)!.Value;

        var result = await _service.DeleteAsync(code, userId, role);

        if (!result)
            return NotFound();

        return NoContent();
    }
}