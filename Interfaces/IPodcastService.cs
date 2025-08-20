using PodcastApi.DTOs.Podcast;
using PodcastApi.Models;

namespace PodcastApi.Interfaces;

public interface IPodcastService
{
    Task<IEnumerable<PodcastDto>> GetAllPodcastsAsync(CancellationToken cancellationToken = default);
    Task<PodcastDto?> GetPodcastByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<PodcastDto> CreatePodcastAsync(CreatePodcastRequest request, CancellationToken cancellationToken = default);
    Task UpdatePodcastAsync(int id, UpdatePodcastRequest request, CancellationToken cancellationToken = default);
    Task DeletePodcastAsync(int id, CancellationToken cancellationToken = default);
}
