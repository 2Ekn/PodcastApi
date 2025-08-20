using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PodcastApi.DTOs.Comments;
using PodcastApi.Interfaces;
using System.Security.Claims;

namespace PodcastApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] // Require JWT
public sealed class CommentsController : ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentsController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    // Helper to extract UserId from JWT claims
    private int GetUserId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdClaim))
        {
            throw new UnauthorizedAccessException("User ID claim missing in token");
        }

        return int.Parse(userIdClaim);
    }

    [HttpGet("episode/{episodeId:int}")]
    [AllowAnonymous] // anyone can read comments
    public async Task<ActionResult<IEnumerable<CommentDto>>> GetByEpisodeId(int episodeId, CancellationToken cancellationToken)
    {
        var comments = await _commentService.GetCommentsByEpisodeIdAsync(episodeId, cancellationToken);
        return Ok(comments);
    }

    // POST: api/comments
    [HttpPost]
    public async Task<ActionResult<CommentDto>> Add([FromBody] AddCommentRequest request, CancellationToken cancellationToken)
    {
        var userId = GetUserId();

        var comment = await _commentService.AddCommentAsync(userId, request, cancellationToken);
        return CreatedAtAction(nameof(GetByEpisodeId), new { episodeId = request.EpisodeId }, comment);
    }

    // DELETE: api/comments/5
    [HttpDelete("{commentId:int}")]
    public async Task<IActionResult> Delete(int commentId, CancellationToken cancellationToken)
    {
        var userId = GetUserId();

        try
        {
            await _commentService.DeleteCommentAsync(commentId, userId, cancellationToken);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (UnauthorizedAccessException)
        {
            return Forbid();
        }

        return NoContent();
    }
}
