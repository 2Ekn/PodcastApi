using PodcastApi.DTOs.ListeningHistories;

namespace PodcastApi.Interfaces;

public interface IListeningHistoryService
{
    Task<IEnumerable<ListeningHistoryDto>> GetHistoryByUserIdAsync(int userId, CancellationToken cancellationToken = default);
    Task UpdateProgressAsync(int userId, int episodeId, int progressSeconds, CancellationToken cancellationToken = default);
}