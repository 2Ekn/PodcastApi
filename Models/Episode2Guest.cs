namespace TfkApi.Models;

public class Episode2Guest
{
    public int EpisodeId { get; set; }
    public virtual Episode Episode { get; set; } = null!;

    public int GuestId { get; set; }
    public virtual Guest Guest { get; set; } = null!;

    public string? Role { get; set; } // "Guest", "Co-host", etc.
}
