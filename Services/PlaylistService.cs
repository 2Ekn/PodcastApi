using Microsoft.EntityFrameworkCore;
using PodcastApi.Data;
using PodcastApi.DTOs.Episodes;
using PodcastApi.DTOs.Playlists;
using PodcastApi.Interfaces;
using PodcastApi.Models;

namespace PodcastApi.Services;

public sealed class PlaylistService : IPlaylistService
{
    private readonly PodcastDbContext _context;

    public PlaylistService(PodcastDbContext context)
    {
        _context = context;
    }

    public async Task<PlaylistDto> CreatePlaylistAsync(int userId, CreatePlaylistRequest request, CancellationToken cancellationToken = default)
    {
        var playlist = new Playlist
        {
            UserId = userId,
            Name = request.Name,
            CreatedAt = DateTime.UtcNow
        };

        _context.Playlists.Add(playlist);
        await _context.SaveChangesAsync(cancellationToken);

        return new PlaylistDto
        {
            Id = playlist.Id,
            Name = playlist.Name,
            CreatedAt = playlist.CreatedAt
        };
    }

    public async Task<IEnumerable<PlaylistDto>> GetPlaylistsByUserIdAsync(int userId, CancellationToken cancellationToken = default)
    {
        return await _context.Playlists
            .AsNoTracking()
            .Where(p => p.UserId == userId)
            .Select(p => new PlaylistDto
            {
                Id = p.Id,
                Name = p.Name,
                CreatedAt = p.CreatedAt
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<PlaylistDto?> GetPlaylistByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var playlist = await _context.Playlists
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

        if (playlist is null)
        {
            return null;
        }

        return new PlaylistDto
        {
            Id = playlist.Id,
            Name = playlist.Name,
            CreatedAt = playlist.CreatedAt
        };
    }

    public async Task DeletePlaylistAsync(int playlistId, CancellationToken cancellationToken = default)
    {
        var playlist = await _context.Playlists.FirstOrDefaultAsync(p => p.Id == playlistId, cancellationToken);
        if (playlist is null)
        {
            throw new KeyNotFoundException($"Playlist {playlistId} not found");
        }

        _context.Playlists.Remove(playlist);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task RenamePlaylistAsync(int playlistId, string newName, CancellationToken cancellationToken = default)
    {
        var playlist = await _context.Playlists.FirstOrDefaultAsync(p => p.Id == playlistId, cancellationToken);
        if (playlist is null)
        {
            throw new KeyNotFoundException($"Playlist {playlistId} not found");
        }

        playlist.Name = newName;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task AddEpisodeToPlaylistAsync(int playlistId, int episodeId, CancellationToken cancellationToken = default)
    {
        var exists = await _context.PlaylistEpisodes.AnyAsync(
            pe => pe.PlaylistId == playlistId && pe.EpisodeId == episodeId, cancellationToken);

        if (!exists)
        {
            var entry = new Playlist2Episode
            {
                PlaylistId = playlistId,
                EpisodeId = episodeId,
            };

            _context.PlaylistEpisodes.Add(entry);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task RemoveEpisodeFromPlaylistAsync(int playlistId, int episodeId, CancellationToken cancellationToken = default)
    {
        var entry = await _context.PlaylistEpisodes
            .FirstOrDefaultAsync(pe => pe.PlaylistId == playlistId && pe.EpisodeId == episodeId, cancellationToken);

        if (entry is not null)
        {
            _context.PlaylistEpisodes.Remove(entry);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<IEnumerable<EpisodeDto>> GetEpisodesInPlaylistAsync(int playlistId, CancellationToken cancellationToken = default)
    {
        return await _context.PlaylistEpisodes
            .AsNoTracking()
            .Include(pe => pe.Episode).ThenInclude(e => e.Podcast)
            .Where(pe => pe.PlaylistId == playlistId)
            .Select(pe => new EpisodeDto
            {
                Id = pe.Episode.Id,
                Title = pe.Episode.Title,
                Description = pe.Episode.Description,
                PublishDate = pe.Episode.PublishDate,
            })
            .ToListAsync(cancellationToken);
    }
}
