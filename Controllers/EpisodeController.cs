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

    [HttpGet("{id:int}")]
    [AllowAnonymous]
    public async Task<ActionResult<EpisodeDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var episode = await _episodeService.GetByIdAsync(id, cancellationToken);
        if (episode is null) return NotFound();
        return Ok(episode);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")] 
    public async Task<ActionResult<EpisodeDto>> Create([FromBody] CreateEpisodeRequest request, CancellationToken cancellationToken)
    {
        var episode = await _episodeService.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = episode.Id }, episode);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin")]
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

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        await _episodeService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}
