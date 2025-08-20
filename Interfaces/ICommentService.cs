using PodcastApi.DTOs.Comments;
using PodcastApi.Models;

namespace PodcastApi.Interfaces;

public interface ICommentService
{
    Task<IEnumerable<CommentDto>> GetCommentsByEpisodeIdAsync(int episodeId, CancellationToken cancellationToken = default);
    Task<CommentDto> AddCommentAsync(int userId, AddCommentRequest request, CancellationToken cancellationToken = default);
    Task DeleteCommentAsync(int commentId, int userId, CancellationToken cancellationToken = default);
}