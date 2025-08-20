namespace PodcastApi.DTOs.Comments;

public sealed class AddCommentRequest { public int EpisodeId { get; init; } public string Content { get; init; } = null!; }