namespace PodcastApi.Models;

public class Podcast2Host
{
    public int PodcastId { get; set; }
    public Podcast Podcast { get; set; } = null!;
    public int HostId { get; set; }
    public Host Host { get; set; } = null!;
    public string? Role { get; set; } // e.g., Main Host, Producer
}
