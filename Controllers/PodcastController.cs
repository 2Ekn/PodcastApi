using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PodcastApi.DTOs.Podcast;
using PodcastApi.Interfaces;
using PodcastApi.Models;

namespace PodcastApi.Controllers;
[Route("api/[controller]")]
[ApiController]

public class PodcastController : ControllerBase
{
    private readonly IPodcastService _podcastService;
    public PodcastController(IPodcastService podcastService)
    {
        _podcastService = podcastService;
    }

    [HttpGet]
    [AllowAnonymous] // Anyone can browse podcasts
    public async Task<ActionResult<IEnumerable<PodcastDto>>> GetPodcasts()
    {
        var podcasts = await _podcastService.GetAllPodcastsAsync();
        return Ok(podcasts);
    }

    [HttpGet("{id:int}")]
    [AllowAnonymous] // Anyone can view podcast details
    public async Task<ActionResult<PodcastDto>> GetPodcastById(int id)
    {
        var podcast = await _podcastService.GetPodcastByIdAsync(id);

        if (podcast is null)
        {
            return NotFound();
        }

        return Ok(podcast);
    }

    [HttpPost]
    public async Task<ActionResult<PodcastDto>> Create([FromBody] CreatePodcastRequest request)
    {
      
        var podcast = await _podcastService.CreatePodcastAsync(request);
        return CreatedAtAction(nameof(GetPodcastById), new { id = podcast.Id }, podcast);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdatePodcastRequest request, CancellationToken ct)
    {
        await _podcastService.UpdatePodcastAsync(id, request, ct);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        await _podcastService.DeletePodcastAsync(id, ct);
        return NoContent();
    }
}
