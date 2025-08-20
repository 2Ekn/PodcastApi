namespace PodcastApi.DTOs.Hosts;

public sealed class HostDto
{
    public int Id { get; init; }
    public string Name { get; init; } = null!;
    public string? Bio { get; init; }
    public string? ImageUrl { get; init; }
}
