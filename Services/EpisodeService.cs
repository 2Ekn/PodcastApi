using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PodcastApi.Data;
using PodcastApi.DTOs;
using PodcastApi.DTOs.Episodes;
using PodcastApi.Interfaces;
using PodcastApi.Models;

namespace PodcastApi.Services;

public class EpisodeService : IEpisodeService
{
    private readonly PodcastApiDbContext _context;

    public EpisodeService(PodcastApiDbContext context)
    {
        _context = context;
    }

    public Task<EpisodeDto> CreateAsync(CreateEpisodeRequest request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<EpisodeDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<(IEnumerable<EpisodeDto> Items, int TotalCount)> GetPagedAsync(int page, int pageSize, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(int id, UpdateEpisodeRequest request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}



