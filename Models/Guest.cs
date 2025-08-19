using System.ComponentModel.DataAnnotations;

namespace PodcastApi.Models;

public class Guest
{
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;
    [MaxLength(500)]
    public string Bio { get; set; } = string.Empty;
    public string? PhotoUrl { get; set; } = string.Empty;

    

    public virtual ICollection<Episode2Guest> EpisodeGuests { get; set; } = new List<Episode2Guest>();
    public virtual ICollection<SocialMediaLink> SocialMediaLinks { get; set; } = new List<SocialMediaLink>();
}
