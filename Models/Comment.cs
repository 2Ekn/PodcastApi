namespace PodcastApi.Models;

public class Comment
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public int EpisodeId { get; set; }
    public Episode Episode { get; set; } = null!;
    public string Content { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}
