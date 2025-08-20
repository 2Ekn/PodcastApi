using PodcastApi.DTOs.Episodes;

namespace PodcastApi.DTOs.Comments;
public sealed class CommentDto
{
    public int Id { get; init; }
    public int EpisodeId { get; init; }
    public int UserId { get; init; }
    public string Content { get; init; } = null!;
    public DateTime CreatedAt { get; init; }
    public string UserDisplayName { get; set; } = string.Empty;
}

