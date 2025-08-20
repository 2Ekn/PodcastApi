using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PodcastApi.Data;
using PodcastApi.DTOs.Episodes;
using PodcastApi.DTOs.Playlists;
using PodcastApi.Interfaces;
using System.Security.Claims;

namespace PodcastApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public sealed class PlaylistsController : ControllerBase
{
    private readonly IPlaylistService _playlistService;
    private readonly PodcastDbContext _context; // 👈 for ownership checks

    public PlaylistsController(IPlaylistService playlistService, PodcastDbContext context)
    {
        _playlistService = playlistService;
        _context = context;
    }

    private int GetUserId() =>
        int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    private async Task<bool> UserOwnsPlaylistAsync(int playlistId, int userId, CancellationToken ct)
    {
        return await _context.Playlists
            .AsNoTracking()
            .AnyAsync(p => p.Id == playlistId && p.UserId == userId, ct);
    }

    [HttpPost]
    public async Task<ActionResult<PlaylistDto>> Create([FromBody] CreatePlaylistRequest request, CancellationToken ct)
    {
        var userId = GetUserId();
        var playlist = await _playlistService.CreatePlaylistAsync(userId, request, ct);
        return CreatedAtAction(nameof(GetById), new { id = playlist.Id }, playlist);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PlaylistDto>>> GetMyPlaylists(CancellationToken ct)
    {
        var userId = GetUserId();
        var playlists = await _playlistService.GetPlaylistsByUserIdAsync(userId, ct);
        return Ok(playlists);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<PlaylistDto>> GetById(int id, CancellationToken ct)
    {
        var userId = GetUserId();
        if (!await UserOwnsPlaylistAsync(id, userId, ct))
            return Forbid();

        var playlist = await _playlistService.GetPlaylistByIdAsync(id, ct);
        if (playlist is null) return NotFound();
        return Ok(playlist);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var userId = GetUserId();
        if (!await UserOwnsPlaylistAsync(id, userId, ct))
            return Forbid();

        await _playlistService.DeletePlaylistAsync(id, ct);
        return NoContent();
    }

    [HttpPut("{id:int}/rename")]
    public async Task<IActionResult> Rename(int id, [FromBody] string newName, CancellationToken ct)
    {
        var userId = GetUserId();
        if (!await UserOwnsPlaylistAsync(id, userId, ct))
            return Forbid();

        await _playlistService.RenamePlaylistAsync(id, newName, ct);
        return NoContent();
    }

    [HttpPost("{playlistId:int}/episodes/{episodeId:int}")]
    public async Task<IActionResult> AddEpisode(int playlistId, int episodeId, CancellationToken ct)
    {
        var userId = GetUserId();
        if (!await UserOwnsPlaylistAsync(playlistId, userId, ct))
            return Forbid();

        await _playlistService.AddEpisodeToPlaylistAsync(playlistId, episodeId, ct);
        return NoContent();
    }

    [HttpDelete("{playlistId:int}/episodes/{episodeId:int}")]
    public async Task<IActionResult> RemoveEpisode(int playlistId, int episodeId, CancellationToken ct)
    {
        var userId = GetUserId();
        if (!await UserOwnsPlaylistAsync(playlistId, userId, ct))
            return Forbid();

        await _playlistService.RemoveEpisodeFromPlaylistAsync(playlistId, episodeId, ct);
        return NoContent();
    }

    [HttpGet("{id:int}/episodes")]
    public async Task<ActionResult<IEnumerable<EpisodeDto>>> GetEpisodes(int id, CancellationToken ct)
    {
        var userId = GetUserId();
        if (!await UserOwnsPlaylistAsync(id, userId, ct))
            return Forbid();

        var episodes = await _playlistService.GetEpisodesInPlaylistAsync(id, ct);
        return Ok(episodes);
    }
}
