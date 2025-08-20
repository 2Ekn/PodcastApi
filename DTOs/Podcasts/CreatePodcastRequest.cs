namespace PodcastApi.DTOs.Podcast;

public sealed class CreatePodcastRequest
{
    public string Title { get; init; } = null!;
    public string? Description { get; init; }
    public string? Website { get; init; }
    public string? LogoUrl { get; init; }
}
