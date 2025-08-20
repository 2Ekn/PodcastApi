using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PodcastApi.Data;
using PodcastApi.DTOs;
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

    public async Task<EpisodeReadDto> CreateEpisodeAsync(EpisodeCreateDto episodeCreateDto)
    {
        var episode = new Episode
        {
            Title = episodeCreateDto.Title,
            Description = episodeCreateDto.Description,
            PublishDate = DateTime.UtcNow,
            DurationSeconds = episodeCreateDto.DurationSeconds,
            EpisodeNumber = episodeCreateDto.EpisodeNumber,
            PodcastId = episodeCreateDto.PodcastId,
            ShowNotesHtml = episodeCreateDto.ShowNotesHtml,
            IsPublished = episodeCreateDto.IsPublished,
            AudioUrl = episodeCreateDto.AudioUrl
        };

        // Add junction records for Guests
        foreach (var guestId in episodeCreateDto.GuestIds)
        {
            episode.EpisodeGuests.Add(new Episode2Guest
            {
                GuestId = int.Parse(guestId)
            });
        }

        // Add junction records for Tags
        foreach (var tagId in episodeCreateDto.TagIds)
        {
            episode.EpisodeTags.Add(new Episode2Tag
            {
                TagId = int.Parse(tagId)
            });
        }

        _context.Episodes.Add(episode);
        await _context.SaveChangesAsync();

        return MapToReadDto(episode);
    }

    public async Task<bool> DeleteEpisodeAsync(int episodeId)
    {
        var episode = await _context.Episodes
            .Include(e => e.EpisodeGuests)
            .Include(e => e.EpisodeTags)
            .FirstOrDefaultAsync(e => e.Id == episodeId);

        if (episode == null)
            return false;

        _context.Episodes.Remove(episode);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<EpisodeReadDto?> GetEpisodeByIdAsync(int episodeId)
    {
        var episode = await _context.Episodes
            .Include(e => e.Podcast)
            .Include(e => e.EpisodeGuests).ThenInclude(g => g.Guest)
            .Include(e => e.EpisodeTags).ThenInclude(t => t.Tag)
            .FirstOrDefaultAsync(e => e.Id == episodeId);

        return episode == null ? null : MapToReadDto(episode);
    }

    public async Task<IEnumerable<EpisodeReadDto>> GetEpisodesAsync()
    {
        var episodes = await _context.Episodes
            .Include(e => e.Podcast)
            .Include(e => e.EpisodeGuests).ThenInclude(g => g.Guest)
            .Include(e => e.EpisodeTags).ThenInclude(t => t.Tag)
            .ToListAsync();

        return episodes.Select(MapToReadDto);
    }

    public async Task<EpisodeReadDto?> UpdateEpisodeAsync(int episodeId, EpisodeUpdateDto episodeUpdateDto)
    {
        var episode = await _context.Episodes
            .Include(e => e.EpisodeGuests)
            .Include(e => e.EpisodeTags)
            .FirstOrDefaultAsync(e => e.Id == episodeId);

        if (episode == null)
            return null;

        // Update main fields
        episode.Title = episodeUpdateDto.Title;
        episode.Description = episodeUpdateDto.Description;
        episode.DurationSeconds = episodeUpdateDto.DurationSeconds;
        episode.EpisodeNumber = episodeUpdateDto.EpisodeNumber;
        episode.ShowNotesHtml = episodeUpdateDto.ShowNotesHtml;
        episode.IsPublished = episodeUpdateDto.IsPublished;
        episode.AudioUrl = episodeUpdateDto.AudioUrl;

        // Update guests (clear + re-add)
        episode.EpisodeGuests.Clear();
        foreach (var guestId in episodeUpdateDto.GuestIds)
        {
            episode.EpisodeGuests.Add(new Episode2Guest
            {
                GuestId = int.Parse(guestId)
            });
        }

        // Update tags (clear + re-add)
        episode.EpisodeTags.Clear();
        foreach (var tagId in episodeUpdateDto.TagIds)
        {
            episode.EpisodeTags.Add(new Episode2Tag
            {
                TagId = int.Parse(tagId)
            });
        }

        await _context.SaveChangesAsync();

        return MapToReadDto(episode);
    }

    // --- Private mapper helper (manual mapping) ---
    private static EpisodeReadDto MapToReadDto(Episode episode)
    {
        return new EpisodeReadDto
        {
            Id = episode.Id,
            Title = episode.Title,
            Description = episode.Description,
            PublishDate = episode.PublishDate,
            DurationSeconds = episode.DurationSeconds,
            EpisodeNumber = episode.EpisodeNumber,
            PodcastId = episode.PodcastId,
            ShowNotesHtml = episode.ShowNotesHtml,
            IsPublished = episode.IsPublished,
            AudioUrl = episode.AudioUrl,
            Guests = episode.EpisodeGuests.Select(g => g.Guest.Name).ToList(),
            Tags = episode.EpisodeTags.Select(t => t.Tag.Name).ToList(),
            PodcastTitle = episode.Podcast?.Title!
        };
    }
}



