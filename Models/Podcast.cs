using System.ComponentModel.DataAnnotations;

namespace PodcastApi.Models;

public class Podcast
{
    public int Id { get; set; }
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(2000)]    
    public string Description { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
    public string Website { get; set; } = string.Empty;
    public string LogoUrl { get; set; } = string.Empty;

    public List<Episode> Episodes { get; set; } = new List<Episode>();
    public ICollection<Podcast2Host> PodcastHosts { get; set; } = new List<Podcast2Host>();

}
