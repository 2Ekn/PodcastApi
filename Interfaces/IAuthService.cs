using PodcastApi.Models;

namespace PodcastApi.Interfaces;

public interface IAuthService
{
    Task<string> LoginAsync(string email, string password);
    Task<User> RegisterAsync(string email, string password, string displayName);
}
