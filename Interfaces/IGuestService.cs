using PodcastApi.Models;

namespace PodcastApi.Interfaces;

public interface IGuestService
{
    Task<IEnumerable<Guest>> GetAllGuestsAsync();
    Task<Guest?> GetGuestByIdAsync(int id);
    Task<Guest> CreateGuestAsync(Guest guest);
    Task UpdateGuestAsync(Guest guest);
    Task DeleteGuestAsync(int id);
}