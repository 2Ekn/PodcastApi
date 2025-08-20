namespace PodcastApi.DTOs.Tags;

public sealed class CreateTagRequest { public string Name { get; init; } = null!; public string? Color { get; init; } }