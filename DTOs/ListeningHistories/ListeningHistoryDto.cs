namespace PodcastApi.DTOs.ListeningHistories;

public sealed class ListeningHistoryDto
{
    public int EpisodeId { get; init; }
    public string EpisodeTitle { get; init; } = null!;
    public int ProgressSeconds { get; init; }
    public DateTime LastListenedAt { get; init; }
}