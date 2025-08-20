using PodcastApi.DTOs.Users;

namespace PodcastApi.DTOs.Auth;

public sealed class AuthResponse { public string Token { get; init; } = null!; public UserDto User { get; init; } = null!; }