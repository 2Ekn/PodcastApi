using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PodcastApi.DTOs.Guests;
using PodcastApi.Interfaces;

namespace PodcastApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public sealed class GuestsController : ControllerBase
{
    private readonly IGuestService _guestService;

    public GuestsController(IGuestService guestService)
    {
        _guestService = guestService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GuestDto>>> GetAll(CancellationToken cancellationToken)
    {
        var guests = await _guestService.GetAllGuestsAsync(cancellationToken);
        return Ok(guests);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<GuestDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var guest = await _guestService.GetGuestByIdAsync(id, cancellationToken);
        if (guest is null) return NotFound();
        return Ok(guest);
    }

    [HttpPost]
    public async Task<ActionResult<GuestDto>> Create([FromBody] CreateGuestRequest request, CancellationToken cancellationToken)
    {
        var guest = await _guestService.CreateGuestAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = guest.Id }, guest);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateGuestRequest request, CancellationToken cancellationToken)
    {
        try
        {
            await _guestService.UpdateGuestAsync(id, request, cancellationToken);
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
        await _guestService.DeleteGuestAsync(id, cancellationToken);
        return NoContent();
    }
}
