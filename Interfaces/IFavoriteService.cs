using PodcastApi.DTOs.Episodes;
using PodcastApi.Models;

namespace PodcastApi.Interfaces;

public interface IFavoriteService
{
    Task<IEnumerable<EpisodeDto>> GetFavoritesByUserIdAsync(int userId, CancellationToken cancellationToken = default);
    Task AddFavoriteAsync(int userId, int episodeId, CancellationToken cancellationToken = default);
    Task RemoveFavoriteAsync(int userId, int episodeId, CancellationToken cancellationToken = default);
}

