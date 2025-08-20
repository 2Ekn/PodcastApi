using PodcastApi.Models;

namespace PodcastApi.Interfaces;

public interface IUserService
{
    Task<User?> GetUserByIdAsync(int id);
    Task<User?> GetUserByEmailAsync(string email);
    Task<User> CreateUserAsync(User user, string password);
    Task<bool> ValidateCredentialsAsync(string email, string password);
}