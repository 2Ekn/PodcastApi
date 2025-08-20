namespace PodcastApi.DTOs.Auth;

public sealed class RegisterRequest
{
    public string Email { get; init; } = null!;
    public string Password { get; init; } = null!;
    public string DisplayName { get; init; } = null!;
}
