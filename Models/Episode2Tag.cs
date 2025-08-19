namespace PodcastApi.Models;

public class Episode2Tag
{
    public int EpisodeId { get; set; }
    public virtual Episode Episode { get; set; } = null!;

    public int TagId { get; set; }
    public virtual Tag Tag { get; set; } = null!;
}
