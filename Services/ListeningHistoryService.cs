using Microsoft.EntityFrameworkCore;
using PodcastApi.Data;
using PodcastApi.DTOs.ListeningHistories;
using PodcastApi.Interfaces;
using PodcastApi.Models;

namespace PodcastApi.Services;

public sealed class ListeningHistoryService : IListeningHistoryService
{
    private readonly PodcastDbContext _context;

    public ListeningHistoryService(PodcastDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ListeningHistoryDto>> GetHistoryByUserIdAsync(int userId, CancellationToken cancellationToken = default)
    {
        return await _context.ListeningHistories
            .AsNoTracking()
            .Include(lh => lh.Episode)
            .ThenInclude(e => e.Podcast)
            .Where(lh => lh.UserId == userId)
            .OrderByDescending(lh => lh.LastListenedAt)
            .Select(lh => new ListeningHistoryDto
            {
                EpisodeId = lh.EpisodeId,
                EpisodeTitle = lh.Episode.Title,
                ProgressSeconds = lh.ProgressSeconds,
                LastListenedAt = lh.LastListenedAt
            })
            .ToListAsync(cancellationToken);
    }

    public async Task UpdateProgressAsync(int userId, int episodeId, int progressSeconds, CancellationToken cancellationToken = default)
    {
        var history = await _context.ListeningHistories
            .FirstOrDefaultAsync(h => h.UserId == userId && h.EpisodeId == episodeId, cancellationToken);

        if (history is null)
        {
            history = new ListeningHistory
            {
                UserId = userId,
                EpisodeId = episodeId,
                ProgressSeconds = progressSeconds,
                LastListenedAt = DateTime.UtcNow
            };
            _context.ListeningHistories.Add(history);
        }
        else
        {
            history.ProgressSeconds = progressSeconds;
            history.LastListenedAt = DateTime.UtcNow;
        }

        await _context.SaveChangesAsync(cancellationToken);
    }
}