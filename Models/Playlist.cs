namespace PodcastApi.Models;

public class Playlist
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime CreatedAt { get; set; }

    // Foreign Key
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    // Navigation
    public ICollection<Playlist2Episode> PlaylistEpisodes { get; set; } = new List<Playlist2Episode>();
}