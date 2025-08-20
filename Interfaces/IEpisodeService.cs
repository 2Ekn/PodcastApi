using PodcastApi.DTOs;
using PodcastApi.Models;

namespace PodcastApi.Interfaces;

public interface IEpisodeService
{
    Task<IEnumerable<Episode>> GetAllEpisodesAsync();
    Task<Episode?> GetEpisodeByIdAsync(int id);
    Task<IEnumerable<Episode>> GetEpisodesByPodcastIdAsync(int podcastId);
    Task<Episode> CreateEpisodeAsync(Episode episode);
    Task UpdateEpisodeAsync(Episode episode);
    Task DeleteEpisodeAsync(int id);
}
