using PodcastApi.Models;

namespace PodcastApi.Interfaces;

public interface IPlaylistService
{
    Task<IEnumerable<Playlist>> GetPlaylistsByUserIdAsync(int userId);
    Task<Playlist?> GetPlaylistByIdAsync(int id);
    Task<Playlist> CreatePlaylistAsync(int userId, string name);
    Task RenamePlaylistAsync(int playlistId, string newName);
    Task DeletePlaylistAsync(int playlistId);

    Task AddEpisodeToPlaylistAsync(int playlistId, int episodeId, int position);
    Task RemoveEpisodeFromPlaylistAsync(int playlistId, int episodeId);
    Task<IEnumerable<Episode>> GetEpisodesInPlaylistAsync(int playlistId);
}
