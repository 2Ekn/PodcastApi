namespace PodcastApi.Interfaces;

public interface IHostService
{
    Task<IEnumerable<Host>> GetAllHostsAsync();
    Task<Host?> GetHostByIdAsync(int id);
    Task<Host> CreateHostAsync(Host host);
    Task UpdateHostAsync(Host host);
    Task DeleteHostAsync(int id);
}
