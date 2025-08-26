using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PodcastApi.Data;
using PodcastApi.DTOs;
using PodcastApi.DTOs.Episodes;
using PodcastApi.DTOs.Guests;
using PodcastApi.DTOs.Hosts;
using PodcastApi.Interfaces;
using PodcastApi.Models;

namespace PodcastApi.Services;

public sealed class EpisodeService : IEpisodeService
{
    private readonly PodcastDbContext _context;

    public EpisodeService(PodcastDbContext context) => _context = context;

    //Pagination method to get episodes with optional filtering
    public async Task<(IEnumerable<EpisodeDto> Items, int TotalCount)> GetPagedAsync(int page, int pageSize, CancellationToken ct = default)
    {
        page = page <= 0 ? 1 : page;
        pageSize = pageSize <= 0 ? 20 : pageSize;

        var query = _context.Episodes
            .AsNoTracking()
            .Include(e => e.Podcast).ThenInclude(p => p.PodcastHosts).ThenInclude(ph => ph.Host)
            .Include(e => e.EpisodeTags).ThenInclude(et => et.Tag)
            .Include(e => e.EpisodeGuests).ThenInclude(eg => eg.Guest);

        var total = await query.CountAsync(ct);

        var items = await query
            .OrderByDescending(e => e.PublishDate)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(e => ToDto(e))
            .ToListAsync(ct);

        return (items, total);
    }

    public async Task<EpisodeDto?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var e = await _context.Episodes
            .AsNoTracking()
            .Include(x => x.Podcast).ThenInclude(p => p.PodcastHosts).ThenInclude(ph => ph.Host)
            .Include(x => x.EpisodeTags).ThenInclude(et => et.Tag)
            .Include(x => x.EpisodeGuests).ThenInclude(eg => eg.Guest)
            .FirstOrDefaultAsync(x => x.Id == id, ct);

        return e is null ? null : ToDto(e);
    }

    public async Task<EpisodeDto> CreateAsync(CreateEpisodeRequest req, CancellationToken ct = default)
    {
        var entity = new Episode
        {
            PodcastId = req.PodcastId,
            Title = req.Title,
            Description = req.Description!,
            PublishDate = req.PublishDate,
            DurationSeconds = req.DurationSeconds,
            EpisodeNumber = req.EpisodeNumber,
            AudioUrl = req.AudioUrl,
            IsPublished = true
        };

        // attach join rows
        entity.EpisodeTags = req.TagIds.Select(id => new Episode2Tag { TagId = id }).ToList();
        entity.EpisodeGuests = req.GuestIds.Select(id => new Episode2Guest { GuestId = id }).ToList();

        _context.Episodes.Add(entity);
        await _context.SaveChangesAsync(ct);

        // reload with navs for DTO
        return (await GetByIdAsync(entity.Id, ct))!;
    }

    public async Task UpdateAsync(int id, UpdateEpisodeRequest req, CancellationToken ct = default)
    {
        var entity = await _context.Episodes
            .Include(e => e.EpisodeTags)
            .Include(e => e.EpisodeGuests)
            .FirstOrDefaultAsync(e => e.Id == id, ct);

        if (entity is null)
        {
            throw new KeyNotFoundException("Episode not found");
        }

        entity.Title = req.Title;
        entity.Description = req.Description!;
        entity.PublishDate = req.PublishDate;
        entity.DurationSeconds = req.DurationSeconds;
        entity.EpisodeNumber = req.EpisodeNumber;
        entity.AudioUrl = req.AudioUrl;

        // sync join tables
        SyncEpisodeTags(entity, req.TagIds);
        SyncEpisodeGuests(entity, req.GuestIds);

        await _context.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(int id, CancellationToken ct = default)
    {
        var entity = await _context.Episodes.FindAsync(new object[] { id }, ct);

        if (entity is null)
        {
            return;
        }

        _context.Episodes.Remove(entity);
        await _context.SaveChangesAsync(ct);
    }

    private static EpisodeDto ToDto(Episode e) => new()
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
    };

    private static void SyncEpisodeTags(Episode e, IEnumerable<int> newTagIds)
    {
        var wanted = new HashSet<int>(newTagIds);
        e.EpisodeTags.RemoveWhere(et => !wanted.Contains(et.TagId));
        var existing = e.EpisodeTags.Select(x => x.TagId).ToHashSet();

        foreach (var id in wanted.Except(existing))
        {
            e.EpisodeTags.Add(new Episode2Tag { EpisodeId = e.Id, TagId = id });
        }
    }

    private static void SyncEpisodeGuests(Episode e, IEnumerable<int> newGuestIds)
    {
        var wanted = new HashSet<int>(newGuestIds);
        e.EpisodeGuests.RemoveWhere(eg => !wanted.Contains(eg.GuestId));
        var existing = e.EpisodeGuests.Select(x => x.GuestId).ToHashSet();

        foreach (var id in wanted.Except(existing))
        {
            e.EpisodeGuests.Add(new Episode2Guest { EpisodeId = e.Id, GuestId = id });
        }
    }
}

// Helper to remove items from ICollection safely
public static class CollectionExtensions
{
    public static void RemoveWhere<T>(this ICollection<T> source, Func<T, bool> predicate)
    {
        var toRemove = source.Where(predicate).ToList();
        foreach (var item in toRemove)
        {
            source.Remove(item);
        }
    }
}


