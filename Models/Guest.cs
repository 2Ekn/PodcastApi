using System.ComponentModel.DataAnnotations;

namespace TfkApi.Models;

public class Guest
{
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;
    [MaxLength(500)]
    public string Bio { get; set; } = string.Empty;
    public string? PhotoUrl { get; set; } = string.Empty;

    // Navigation
    //public virtual ICollection<EpisodeGuest> EpisodeGuests { get; set; } = new List<EpisodeGuest>();
    //public virtual ICollection<SocialMediaLink> SocialMediaLinks { get; set; } = new List<SocialMediaLink>();
}
