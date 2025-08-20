using PodcastApi.DTOs.Episodes;

namespace PodcastApi.DTOs.Playlists;

public sealed class PlaylistDto
{
    public int Id { get; init; }
    public string Name { get; init; } = null!;
    public DateTime CreatedAt { get; init; }
    public IEnumerable<EpisodeDto> Episodes { get; init; } = Array.Empty<EpisodeDto>();
}
