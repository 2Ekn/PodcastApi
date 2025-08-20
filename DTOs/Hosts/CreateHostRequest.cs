namespace PodcastApi.DTOs.Hosts;

public sealed class CreateHostRequest
{
    public string Name { get; init; } = null!;
    public string? Bio { get; init; }
    public string? ImageUrl { get; init; }
}