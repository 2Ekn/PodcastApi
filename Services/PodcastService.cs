using Microsoft.EntityFrameworkCore;
using PodcastApi.Data;
using PodcastApi.DTOs.Podcast;
using PodcastApi.Interfaces;
using PodcastApi.Models;

namespace PodcastApi.Services;

public sealed class PodcastService : IPodcastService
{
    private readonly PodcastDbContext _context;

    public PodcastService(PodcastDbContext context)
    {
        _context = context;
    }

    public async Task<PodcastDto> CreatePodcastAsync(CreatePodcastRequest request, CancellationToken cancellationToken = default)
    {
        var podcast = new Podcast
        {
            Title = request.Title,
            Description = request.Description!,
           LogoUrl = request.LogoUrl!,
            CreatedAt = DateTime.UtcNow
        };

        _context.Podcasts.Add(podcast);
        await _context.SaveChangesAsync(cancellationToken);

        return new PodcastDto
        {
            Id = podcast.Id,
            Title = podcast.Title,
            Description = podcast.Description,
            LogoUrl = podcast.LogoUrl,
        };
    }

    public async Task<IEnumerable<PodcastDto>> GetAllPodcastsAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Podcasts
            .AsNoTracking()
            .OrderBy(p => p.Title)
            .Select(p => new PodcastDto
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
                LogoUrl = p.LogoUrl,
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<PodcastDto?> GetPodcastByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var podcast = await _context.Podcasts
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

        if (podcast is null) return null;

        return new PodcastDto
        {
            Id = podcast.Id,
            Title = podcast.Title,
            Description = podcast.Description,
            LogoUrl = podcast.LogoUrl,
        };
    }

    public async Task UpdatePodcastAsync(int id, UpdatePodcastRequest request, CancellationToken cancellationToken = default)
    {
        var podcast = await _context.Podcasts.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

        if (podcast is null)
            throw new KeyNotFoundException($"Podcast with id {id} not found");

        podcast.Title = request.Title;
        podcast.Description = request.Description!;
        podcast.LogoUrl = request.LogoUrl!;

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeletePodcastAsync(int id, CancellationToken cancellationToken = default)
    {
        var podcast = await _context.Podcasts.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

        if (podcast is null)
            throw new KeyNotFoundException($"Podcast with id {id} not found");

        _context.Podcasts.Remove(podcast);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
