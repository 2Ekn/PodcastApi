namespace PodcastApi.DTOs.Guests;

public sealed class UpdateGuestRequest
{
    public string Name { get; init; } = null!;
    public string? Bio { get; init; }
    public string? PhotoUrl { get; init; }
}