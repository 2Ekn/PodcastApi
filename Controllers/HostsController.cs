using Microsoft.AspNetCore.Mvc;
using PodcastApi.DTOs.Hosts;
using PodcastApi.Interfaces;

namespace PodcastApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class HostsController : ControllerBase
{
    private readonly IHostService _hostService;

    public HostsController(IHostService hostService)
    {
        _hostService = hostService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<HostDto>>> GetAll(CancellationToken cancellationToken)
    {
        var hosts = await _hostService.GetAllHostsAsync(cancellationToken);
        return Ok(hosts);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<HostDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var host = await _hostService.GetHostByIdAsync(id, cancellationToken);
        if (host is null) return NotFound();
        return Ok(host);
    }

    [HttpPost]
    public async Task<ActionResult<HostDto>> Create([FromBody] CreateHostRequest request, CancellationToken cancellationToken)
    {
        var host = await _hostService.CreateHostAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = host.Id }, host);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] CreateHostRequest request, CancellationToken cancellationToken)
    {
        try
        {
            await _hostService.UpdateHostAsync(id, request, cancellationToken);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        await _hostService.DeleteHostAsync(id, cancellationToken);
        return NoContent();
    }
}