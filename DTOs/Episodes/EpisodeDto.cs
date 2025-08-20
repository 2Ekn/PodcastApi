using PodcastApi.DTOs.Guests;
using PodcastApi.DTOs.Hosts;

namespace PodcastApi.DTOs.Episodes;

public sealed class EpisodeDto
{
    public int Id { get; init; }
    public int PodcastId { get; init; }
    public string Title { get; init; } = null!;
    public string? Description { get; init; }
    public DateTime PublishDate { get; init; }
    public int DurationSeconds { get; init; }
    public int EpisodeNumber { get; init; }
    public string AudioUrl { get; init; } = null!;
    public string? ImageUrl { get; init; }
    public IEnumerable<string> Tags { get; init; } = Array.Empty<string>();
    public IEnumerable<GuestBriefDto> Guests { get; init; } = Array.Empty<GuestBriefDto>();
    public IEnumerable<HostBriefDto> Hosts { get; init; } = Array.Empty<HostBriefDto>();
}
