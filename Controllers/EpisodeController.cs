using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PodcastApi.DTOs.Common;
using PodcastApi.DTOs.Episodes;
using PodcastApi.Interfaces;

namespace PodcastApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class EpisodesController : ControllerBase
{
    private readonly IEpisodeService _episodeService;

    public EpisodesController(IEpisodeService episodeService)
    {
        _episodeService = episodeService;
    }

    // GET: api/episodes?page=1&pageSize=20
    [HttpGet]
    [AllowAnonymous] // anyone can browse episodes
    public async Task<ActionResult<PagedResult<EpisodeDto>>> GetPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 20, CancellationToken cancellationToken = default)
    {
        var (items, total) = await _episodeService.GetPagedAsync(page, pageSize, cancellationToken);

        var result = new PagedResult<EpisodeDto>
        {
            Items = items,
            TotalCount = total,
            Page = page,
            PageSize = pageSize
        };

        return Ok(result);
    }

    // GET: api/episodes/5
    [HttpGet("{id:int}")]
    [AllowAnonymous]
    public async Task<ActionResult<EpisodeDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var episode = await _episodeService.GetByIdAsync(id, cancellationToken);
        if (episode is null) return NotFound();
        return Ok(episode);
    }

    // POST: api/episodes
    [HttpPost]
    [Authorize] // restrict to authenticated users, you can later limit to "admins"
    public async Task<ActionResult<EpisodeDto>> Create([FromBody] CreateEpisodeRequest request, CancellationToken cancellationToken)
    {
        var episode = await _episodeService.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = episode.Id }, episode);
    }

    // PUT: api/episodes/5
    [HttpPut("{id:int}")]
    [Authorize]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateEpisodeRequest request, CancellationToken cancellationToken)
    {
        try
        {
            await _episodeService.UpdateAsync(id, request, cancellationToken);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    // DELETE: api/episodes/5
    [HttpDelete("{id:int}")]
    [Authorize]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        await _episodeService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}
