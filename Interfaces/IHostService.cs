using PodcastApi.DTOs.Hosts;

namespace PodcastApi.Interfaces;

public interface IHostService
{
    Task<IEnumerable<HostDto>> GetAllHostsAsync(CancellationToken cancellationToken = default);
    Task<HostDto?> GetHostByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<HostDto> CreateHostAsync(CreateHostRequest request, CancellationToken cancellationToken = default);
    Task UpdateHostAsync(int id, CreateHostRequest request, CancellationToken cancellationToken = default);
    Task DeleteHostAsync(int id, CancellationToken cancellationToken = default);
}
