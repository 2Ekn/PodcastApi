using PodcastApi.Models;

namespace PodcastApi.Interfaces;

public interface IPodcastService
{
    Task<IEnumerable<Podcast>> GetAllPodcastsAsync();
    Task<Podcast?> GetPodcastByIdAsync(int id);
    Task<Podcast> CreatePodcastAsync(Podcast podcast);
    Task UpdatePodcastAsync(Podcast podcast);
    Task DeletePodcastAsync(int id);
}
