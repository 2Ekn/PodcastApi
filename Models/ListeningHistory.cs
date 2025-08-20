namespace PodcastApi.Models;

public class ListeningHistory
{
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public int EpisodeId { get; set; }
    public Episode Episode { get; set; } = null!;
    public int ProgressSeconds { get; set; }
    public DateTime LastListenedAt { get; set; }
}
