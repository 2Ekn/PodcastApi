using PodcastApi.Models;
using System.ComponentModel.DataAnnotations;

namespace PodcastApi.Models;

public class User
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string DisplayName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [MaxLength(255)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MinLength(6)]
    [MaxLength(100)]
    public string Password { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public bool IsAdmin { get; set; } = false;

    // Navigation properties
    public virtual ICollection<FavoritedEpisode> FavoritedEpisodes { get; set; } = new List<FavoritedEpisode>();
}
