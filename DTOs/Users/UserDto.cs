namespace PodcastApi.DTOs.Users;

public sealed class UserDto
{
    public int Id { get; init; }
    public string Email { get; init; } = null!;
    public string DisplayName { get; init; } = null!;
    public string SubLevel { get; init; } = null!;
    public string Role { get; init; } = string.Empty;
}
