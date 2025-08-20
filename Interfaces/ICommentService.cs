using PodcastApi.Models;

namespace PodcastApi.Interfaces;

public interface ICommentService
{
    Task<IEnumerable<Comment>> GetCommentsByEpisodeIdAsync(int episodeId);
    Task<Comment> AddCommentAsync(int userId, int episodeId, string content);
    Task DeleteCommentAsync(int commentId, int userId);
}