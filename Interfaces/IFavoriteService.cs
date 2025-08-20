using PodcastApi.Models;

namespace PodcastApi.Interfaces;

public interface IFavoriteService
{
    Task<IEnumerable<Episode>> GetFavoritesByUserIdAsync(int userId);
    Task AddFavoriteAsync(int userId, int episodeId);
    Task RemoveFavoriteAsync(int userId, int episodeId);
}
