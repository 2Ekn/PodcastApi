using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PodcastApi.DTOs.ListeningHistories;
using PodcastApi.Interfaces;
using System.Security.Claims;

namespace PodcastApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public sealed class ListeningHistoryController : ControllerBase
{
    private readonly IListeningHistoryService _listeningHistoryService;

    public ListeningHistoryController(IListeningHistoryService listeningHistoryService)
    {
        _listeningHistoryService = listeningHistoryService;
    }

    private int GetUserId() =>
        int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    // GET: api/listeninghistory
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ListeningHistoryDto>>> GetMyHistory(CancellationToken ct)
    {
        var userId = GetUserId();
        var history = await _listeningHistoryService.GetHistoryByUserIdAsync(userId, ct);
        return Ok(history);
    }

    // POST: api/listeninghistory/{episodeId}
    [HttpPost("{episodeId:int}")]
    public async Task<IActionResult> UpdateProgress(int episodeId, [FromBody] int progressSeconds, CancellationToken ct)
    {
        if (progressSeconds < 0)
            return BadRequest("Progress must be >= 0 seconds");

        var userId = GetUserId();
        await _listeningHistoryService.UpdateProgressAsync(userId, episodeId, progressSeconds, ct);

        return NoContent();
    }
}
