using PodcastApi.DTOs.Episodes;
using PodcastApi.DTOs.Playlists;
using PodcastApi.Models;

namespace PodcastApi.Interfaces;

public interface IPlaylistService
{
    Task<IEnumerable<PlaylistDto>> GetPlaylistsByUserIdAsync(int userId, CancellationToken cancellationToken = default);
    Task<PlaylistDto?> GetPlaylistByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<PlaylistDto> CreatePlaylistAsync(int userId, CreatePlaylistRequest request, CancellationToken cancellationToken = default);
    Task RenamePlaylistAsync(int playlistId, string newName, CancellationToken cancellationToken = default);
    Task DeletePlaylistAsync(int playlistId, CancellationToken cancellationToken = default);

    Task AddEpisodeToPlaylistAsync(int playlistId, int episodeId, CancellationToken cancellationToken = default);
    Task RemoveEpisodeFromPlaylistAsync(int playlistId, int episodeId, CancellationToken cancellationToken = default);
    Task<IEnumerable<EpisodeDto>> GetEpisodesInPlaylistAsync(int playlistId, CancellationToken cancellationToken = default);
}
