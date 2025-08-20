namespace PodcastApi.DTOs.Episodes;

public sealed class CreateEpisodeRequest
{
    public int PodcastId { get; init; }
    public string Title { get; init; } = null!;
    public string? Description { get; init; }
    public DateTime PublishDate { get; init; }
    public int DurationSeconds { get; init; }
    public int EpisodeNumber { get; init; }
    public string AudioUrl { get; init; } = null!;
    public IEnumerable<int> TagIds { get; init; } = Array.Empty<int>();
    public IEnumerable<int> GuestIds { get; init; } = Array.Empty<int>();
}