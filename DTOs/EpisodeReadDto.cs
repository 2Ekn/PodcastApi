namespace PodcastApi.DTOs;

public class EpisodeReadDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime PublishDate { get; set; }
    public int DurationSeconds { get; set; }
    public int EpisodeNumber { get; set; }
    public string AudioUrl { get; set; } = string.Empty;
    public string ShowNotesHtml { get; set; } = string.Empty;
    public bool IsPublished { get; set; }

    public int PodcastId { get; set; }
    public string PodcastTitle { get; set; } = string.Empty; // Flattened from Podcast for convenience

    public IEnumerable<string> Guests { get; set; } = new List<string>(); // from EpisodeGuests
    public IEnumerable<string> Tags { get; set; } = new List<string>();   // from EpisodeTags
}