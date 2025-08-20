using Microsoft.EntityFrameworkCore;
using PodcastApi.Data;
using PodcastApi.DTOs.Episodes;
using PodcastApi.DTOs.Guests;
using PodcastApi.DTOs.Hosts;
using PodcastApi.Interfaces;
using PodcastApi.Models;

namespace PodcastApi.Services;

public sealed class FavoriteService : IFavoriteService
{
    private readonly PodcastDbContext _context;

    public FavoriteService(PodcastDbContext context)
    {
        _context = context;
    }

    public async Task AddFavoriteAsync(int userId, int episodeId, CancellationToken cancellationToken = default)
    {
        var exists = await _context.Episodes.AnyAsync(e => e.Id == episodeId, cancellationToken);
        if (!exists)
        {
            throw new KeyNotFoundException("Episode not found");
        }

        var userExists = await _context.Users.AnyAsync(u => u.Id == userId, cancellationToken);
        if (!userExists)
        {
            throw new KeyNotFoundException("User not found");
        }

        //Check if already favorited
        var alreadyFavorited = await _context.FavoriteEpisodes
            .AnyAsync(f => f.UserId == userId && f.EpisodeId == episodeId, cancellationToken);

        if (alreadyFavorited)
        {
            return;
        }

        _context.FavoriteEpisodes.Add(new FavoritedEpisode
        {
            UserId = userId,
            EpisodeId = episodeId,
            FavoritedAt = DateTime.UtcNow
        });

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<EpisodeDto>> GetFavoritesByUserIdAsync(int userId, CancellationToken cancellationToken = default)
    {
        var episodes = await _context.FavoriteEpisodes
            .AsNoTracking()
            .Where(f => f.UserId == userId)
            .OrderByDescending(f => f.FavoritedAt)
            .Select(f => f.Episode)
            .Include(e => e.Podcast).ThenInclude(p => p.PodcastHosts).ThenInclude(ph => ph.Host)
            .Include(e => e.EpisodeTags).ThenInclude(et => et.Tag)
            .Include(e => e.EpisodeGuests).ThenInclude(eg => eg.Guest)
            .Select(e => new EpisodeDto
            {
                Id = e.Id,
                PodcastId = e.PodcastId,
                Title = e.Title,
                Description = e.Description,
                PublishDate = e.PublishDate,
                DurationSeconds = e.DurationSeconds,
                EpisodeNumber = e.EpisodeNumber,
                AudioUrl = e.AudioUrl,
                Tags = e.EpisodeTags.Select(t => t.Tag.Name),
                Guests = e.EpisodeGuests.Select(g => new GuestBriefDto { Id = g.GuestId, Name = g.Guest.Name }),
                Hosts = e.Podcast.PodcastHosts.Select(h => new HostBriefDto { Id = h.HostId, Name = h.Host.Name })
            })
            .ToListAsync(cancellationToken);

        return episodes;
    }

    public async Task RemoveFavoriteAsync(int userId, int episodeId, CancellationToken cancellationToken = default)
    {
        var fav = await _context.FavoriteEpisodes
            .FirstOrDefaultAsync(f => f.UserId == userId && f.EpisodeId == episodeId, cancellationToken);

        if (fav is null)
        {
            return;
        }

        _context.FavoriteEpisodes.Remove(fav);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
