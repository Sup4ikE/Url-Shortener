using UrlShortener.Core.Application.DTOs;
using UrlShortener.Core.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace UrlShortener.API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _auth;

    public AuthController(IAuthService auth)
    {
        _auth = auth;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var token = await _auth.LoginAsync(dto.Login, dto.Password);

        if (token == null)
            return Unauthorized("Invalid credentials");

        return Ok(new { token });
    }
}