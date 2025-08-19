using PodcastApi.Models;

namespace PodcastApi.Models;

public class FavoritedEpisode
{
    public int EpisodeId { get; set; }
    public virtual Episode Episode { get; set; } = null!;

    public int UserId { get; set; }
    public virtual User User { get; set; } = null!;

    public DateTime FavoritedAt { get; set; } = DateTime.UtcNow;
}
