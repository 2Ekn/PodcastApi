namespace PodcastApi.Models;

public class Playlist2Episode
{
    public int PlaylistId { get; set; }
    public Playlist Playlist { get; set; } = null!;
    public int EpisodeId { get; set; }
    public Episode Episode { get; set; } = null!;
    public int Position { get; set; }
}
