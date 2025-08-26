using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PodcastApi.DTOs.Episodes;
using PodcastApi.Interfaces;
using System.Security.Claims;

namespace PodcastApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] 
public sealed class FavoritesController : ControllerBase
{
    private readonly IFavoriteService _favoriteService;

    public FavoritesController(IFavoriteService favoriteService)
    {
        _favoriteService = favoriteService;
    }

    // extracts userId from JWT
    private int GetUserId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdClaim))
            throw new UnauthorizedAccessException("User ID claim missing in token");

        return int.Parse(userIdClaim);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EpisodeDto>>> GetFavorites(CancellationToken cancellationToken)
    {
        var userId = GetUserId();
        var favorites = await _favoriteService.GetFavoritesByUserIdAsync(userId, cancellationToken);
        return Ok(favorites);
    }

    [HttpPost("{episodeId:int}")]
    public async Task<IActionResult> AddFavorite(int episodeId, CancellationToken cancellationToken)
    {
        var userId = GetUserId();
        await _favoriteService.AddFavoriteAsync(userId, episodeId, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{episodeId:int}")]
    public async Task<IActionResult> RemoveFavorite(int episodeId, CancellationToken cancellationToken)
    {
        var userId = GetUserId();
        await _favoriteService.RemoveFavoriteAsync(userId, episodeId, cancellationToken);
        return NoContent();
    }
}
