using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using PodcastApi.Interfaces;
using System.Security.Claims;
using PodcastApi.DTOs.Auth;

namespace PodcastApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] DTOs.Auth.LoginRequest request)
    {
        try
        {
            var token = await _authService.LoginAsync(request);
            return Ok(new { Token = token });
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized("Invalid credentials");
        }
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] DTOs.Auth.RegisterRequest request)
    {
        try
        {
            var user = await _authService.RegisterAsync(request);
            return Ok(new { Message = "User registered successfully", UserId = user.Id });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("profile")]
    [Authorize] // This endpoint requires authentication
    public IActionResult GetProfile()
    {
        var userId = User.FindFirst("userId")?.Value;
        var email = User.FindFirst(ClaimTypes.Email)?.Value;
        var name = User.FindFirst(ClaimTypes.Name)?.Value;

        return Ok(new { UserId = userId, Email = email, Name = name });
    }
}
