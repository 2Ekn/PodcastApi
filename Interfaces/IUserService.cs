using PodcastApi.Models;

namespace PodcastApi.Interfaces;

public interface IUserService
{
    Task<UserDto?> GetUserByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<UserDto?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default);
}