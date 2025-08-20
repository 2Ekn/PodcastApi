using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PodcastApi.DTOs.Tags;
using PodcastApi.Interfaces;

namespace PodcastApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public sealed class TagsController : ControllerBase
{
    private readonly ITagService _tagService;

    public TagsController(ITagService tagService)
    {
        _tagService = tagService;
    }

    [HttpGet]
    [AllowAnonymous] 
    public async Task<ActionResult<IEnumerable<TagDto>>> GetAll(CancellationToken ct)
    {
        var tags = await _tagService.GetAllTagsAsync(ct);
        return Ok(tags);
    }

    [HttpGet("{id:int}")]
    [AllowAnonymous]
    public async Task<ActionResult<TagDto>> GetById(int id, CancellationToken ct)
    {
        var tag = await _tagService.GetTagByIdAsync(id, ct);
        if (tag is null)
            return NotFound();

        return Ok(tag);
    }

    [HttpPost]
    public async Task<ActionResult<TagDto>> Create([FromBody] CreateTagRequest request, CancellationToken ct)
    {
        var tag = await _tagService.CreateTagAsync(request, ct);
        return CreatedAtAction(nameof(GetById), new { id = tag.Id }, tag);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] CreateTagRequest request, CancellationToken ct)
    {
        await _tagService.UpdateTagAsync(id, request, ct);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        await _tagService.DeleteTagAsync(id, ct);
        return NoContent();
    }
}
