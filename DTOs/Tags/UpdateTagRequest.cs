namespace PodcastApi.DTOs.Tags;

public sealed class UpdateTagRequest
{
    public string Name { get; init; } = null!;
    public string? Color { get; init; }
}