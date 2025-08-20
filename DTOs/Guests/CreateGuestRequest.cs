namespace PodcastApi.DTOs.Guests;

public sealed class CreateGuestRequest { public string Name { get; init; } = null!; public string? Bio { get; init; } public string? PhotoUrl { get; init; } }
