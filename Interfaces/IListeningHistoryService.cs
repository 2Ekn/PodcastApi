using PodcastApi.Models;

namespace PodcastApi.Interfaces;

public interface IListeningHistoryService
{
    Task<IEnumerable<ListeningHistory>> GetHistoryByUserIdAsync(int userId);
    Task UpdateProgressAsync(int userId, int episodeId, int progressSeconds);
}