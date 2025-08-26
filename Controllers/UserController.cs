using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PodcastApi.DTOs.Users;
using PodcastApi.Interfaces;

namespace PodcastApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] 
public sealed class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<UserDto>> GetById(int id, CancellationToken ct)
    {
        var user = await _userService.GetUserByIdAsync(id, ct);
        if (user is null)
            return NotFound();

        return Ok(user);
    }

    [HttpGet("by-email/{email}")]
    public async Task<ActionResult<UserDto>> GetByEmail(string email, CancellationToken ct)
    {
        var user = await _userService.GetUserByEmailAsync(email, ct);
        if (user is null)
            return NotFound();

        return Ok(user);
    }

    [HttpGet("me")]
    public async Task<ActionResult<UserDto>> GetCurrentUser(CancellationToken ct)
    {
        var email = User.Identity?.Name;
        if (string.IsNullOrEmpty(email))
            return Unauthorized();

        var user = await _userService.GetUserByEmailAsync(email, ct);
        if (user is null)
            return NotFound();

        return Ok(user);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAll(CancellationToken ct)
    {
        var users = await _userService.GetAllUsersAsync(ct);
        return Ok(users);
    }
}
