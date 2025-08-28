using System.Text.Json.Serialization;

namespace PodcastApi.DTOs.Auth;

public sealed class LoginRequest 
{
    [JsonPropertyName("email")]
    public string Email { get; init; } = null!; 
    [JsonPropertyName("password")]
    public string Password { get; init; } = null!; 
}
