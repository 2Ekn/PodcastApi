using PodcastApi.DTOs.Auth;
using PodcastApi.DTOs.Users;
using PodcastApi.Models;

namespace PodcastApi.Interfaces;

public interface IAuthService
{
    Task<AuthResponse> LoginAsync(DTOs.Auth.LoginRequest request, CancellationToken cancellationToken = default);
    Task<UserDto> RegisterAsync(DTOs.Auth.RegisterRequest request, CancellationToken cancellationToken = default);
}
