using PodcastApi.DTOs.Episodes;
using PodcastApi.Models;

namespace PodcastApi.Interfaces;

public interface IEpisodeService
{
    Task<(IEnumerable<EpisodeDto> Items, int TotalCount)> GetPagedAsync(int page, int pageSize, CancellationToken cancellationToken = default);
    Task<EpisodeDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<EpisodeDto> CreateAsync(CreateEpisodeRequest request, CancellationToken cancellationToken = default);
    Task UpdateAsync(int id, UpdateEpisodeRequest request, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}
