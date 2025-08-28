using PodcastApi.DTOs.Users;
using PodcastApi.Models;

namespace PodcastApi.Interfaces;

public interface IUserService
{
    Task<UserDto?> GetUserByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<UserDto?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<IEnumerable<UserDto>> GetAllUsersAsync(CancellationToken cancellationToken = default);
    Task<bool> UpdateUserAsync(UpdateUserRequest updateUserRequest, int id, CancellationToken cancellationToken = default);
}