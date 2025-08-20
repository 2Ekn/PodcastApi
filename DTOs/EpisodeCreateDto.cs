using System.ComponentModel.DataAnnotations;

namespace PodcastApi.DTOs;
public class EpisodeCreateDto
{
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(2000)]
    public string Description { get; set; } = string.Empty;

    public DateTime PublishDate { get; set; } = DateTime.UtcNow;
    public int DurationSeconds { get; set; }
    public int EpisodeNumber { get; set; }

    [Required]
    public string AudioUrl { get; set; } = string.Empty;

    public string ShowNotesHtml { get; set; } = string.Empty;
    public bool IsPublished { get; set; } = true;

    [Required]
    public int PodcastId { get; set; }

    public IEnumerable<string> GuestIds { get; set; } = new List<string>();
    public IEnumerable<string> TagIds { get; set; } = new List<string>();
}

