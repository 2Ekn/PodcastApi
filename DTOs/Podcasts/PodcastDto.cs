namespace PodcastApi.DTOs.Podcast;

public sealed class PodcastDto
{
    public int Id { get; init; }
    public string Title { get; init; } = null!;
    public string? Description { get; init; }
    public string? Website { get; init; }
    public string? LogoUrl { get; init; }
}
