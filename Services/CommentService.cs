using Microsoft.EntityFrameworkCore;
using PodcastApi.Data;
using PodcastApi.DTOs.Comments;
using PodcastApi.Interfaces;
using PodcastApi.Models;

public sealed class CommentService : ICommentService
{
    private readonly PodcastDbContext _context;

    public CommentService(PodcastDbContext context)
    {
        _context = context;
    }

    public async Task<CommentDto> AddCommentAsync(int userId, AddCommentRequest request, CancellationToken cancellationToken = default)
    {
        // Ensure episode exists
        var episodeExists = await _context.Episodes
            .AnyAsync(e => e.Id == request.EpisodeId, cancellationToken);

        if (!episodeExists)
        {
            throw new KeyNotFoundException("Episode not found");
        }

        // Ensure user exists
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
        
        if (user is null)
        {
            throw new KeyNotFoundException("User not found");
        }

        var comment = new Comment
        {
            EpisodeId = request.EpisodeId,
            UserId = userId,
            Content = request.Content,
            CreatedAt = DateTime.UtcNow
        };

        _context.Comments.Add(comment);
        await _context.SaveChangesAsync(cancellationToken);

        return new CommentDto
        {
            Id = comment.Id,
            EpisodeId = comment.EpisodeId,
            UserId = comment.UserId,
            Content = comment.Content,
            CreatedAt = comment.CreatedAt,
            UserDisplayName = user.DisplayName
        };
    }

    public async Task DeleteCommentAsync(int commentId, int userId, CancellationToken cancellationToken = default)
    {
        var comment = await _context.Comments
            .FirstOrDefaultAsync(c => c.Id == commentId, cancellationToken);

        if (comment is null)
        {
            throw new KeyNotFoundException("Comment not found");
        }

        if (comment.UserId != userId)
        {
            throw new UnauthorizedAccessException("You can only delete your own comments");
        }

        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<CommentDto>> GetCommentsByEpisodeIdAsync(int episodeId, CancellationToken cancellationToken = default)
    {
        return await _context.Comments
            .AsNoTracking()
            .Where(c => c.EpisodeId == episodeId)
            .Include(c => c.User) // to get DisplayName
            .OrderByDescending(c => c.CreatedAt)
            .Select(c => new CommentDto
            {
                Id = c.Id,
                EpisodeId = c.EpisodeId,
                UserId = c.UserId,
                Content = c.Content,
                CreatedAt = c.CreatedAt,
                UserDisplayName = c.User.DisplayName
            })
            .ToListAsync(cancellationToken);
    }
}