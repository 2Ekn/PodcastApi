using System.ComponentModel.DataAnnotations;

namespace TfkApi.Models;

public class Tag
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    public string? Color { get; set; }

    // Navigation
    public virtual ICollection<Episode2Tag> EpisodeTags { get; set; } = new List<Episode2Tag>();
}
